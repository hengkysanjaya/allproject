using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASC2016.User_Control
{
    public partial class EditRunnerProfile : UserControl
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        Form1 parent;
        public EditRunnerProfile()
        {
            InitializeComponent();
        }

        Runner run;
        private void EditRunnerProfile_Load(object sender, EventArgs e)
        {
            textBox1.Text = core.Email;
            parent = (Form1)this.ParentForm;

            var q = db.RegistrationStatus;
            comboStatus.DisplayMember = "RegistrationStatus1";
            comboStatus.ValueMember = "RegistrationStatusId";
            comboStatus.DataSource = q;

            comboGender.DisplayMember = "Gender1";
            comboGender.DataSource = db.Genders.ToList();

            comboCOuntry.ValueMember = "CountryCode";
            comboCOuntry.DisplayMember = "CountryName";
            comboCOuntry.DataSource = db.Countries;

            run = db.Runners.Where(x => x.Email.Equals(core.Email)).FirstOrDefault();
            var u = run.User;
            txtFirstName.Text = u.FirstName;
            txtLastName.Text = u.LastName;
            comboGender.SelectedText = run.Gender;
            dateofbirth.Value = run.DateOfBirth.Value;
            comboCOuntry.SelectedValue = run.CountryCode;
            comboStatus.SelectedValue = db.Registrations.Where(x => x.RegistrationId.ToString() == core.RegistrationId).FirstOrDefault().RegistrationStatusId;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var u = run.User;
            u.FirstName = txtFirstName.Text;
            u.LastName = txtLastName.Text;
            run.Gender = comboGender.Text;
            run.DateOfBirth = dateofbirth.Value;
            run.CountryCode = comboCOuntry.SelectedValue.ToString();

            db.Registrations.Where(x => x.RegistrationId.ToString() == core.RegistrationId).FirstOrDefault().RegistrationStatusId = byte.Parse(comboStatus.SelectedValue.ToString());

            if (!parent.ValidationNotNull(this))
            {
                MessageBox.Show("All Data must be filled");
                return;
            }
            if ((DateTime.Now.Year - dateofbirth.Value.Year) < 10)
            {
                MessageBox.Show("“Date of Birth” must be a valid date and the runner must be at least 10 years old at the time of registration");
                return;
            }

            if (txtPassword.Text != "")
            {
                if (txtPassword.Text.Length < 6)
                {
                    MessageBox.Show("Password must be At least 6 characters");
                    return;
                }
                if (!txtPassword.Text.Any(char.IsUpper))
                {
                    MessageBox.Show("Password must be At least 1 uppercase letter");
                    return;
                }

                if (!txtPassword.Text.Any(char.IsNumber))
                {
                    MessageBox.Show("Password must be At least 1 Number/digit");
                    return;
                }
                string[] symbol = { "!", "@", "#", "$", "%", "^" };
                if (txtPassword.Text.Where(x => symbol.Contains(x.ToString())).Count() == 0)
                {
                    MessageBox.Show("Password must be at least 1 of the following symbols: !@#$%^");
                    return;
                }
                if (!txtPasswordAgain.Text.Equals(txtPassword.Text))
                {
                    MessageBox.Show("The value of “Password Again” must match the value of “Password”");
                    return;
                }
               
            }

            if(txtPassword.Text!="" && txtPasswordAgain.Text != "")
            {
                u.Password = txtPasswordAgain.Text;
            }

            db.SubmitChanges();
            MessageBox.Show("Update Successfull");


            parent.LoadForm("MANAGEARUNNER");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            parent.btnBack.PerformClick();
        }
    }
}
