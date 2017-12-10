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
    public partial class EditProfile : UserControl
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        Form1 parent;
        
        public EditProfile()
        {
            InitializeComponent();
        }

        User currentUser;
        Runner currentRunner;
        private void EditProfile_Load(object sender, EventArgs e)
        {
            parent = (Form1)this.ParentForm;

            var q = db.Genders;
            comboBox1.DisplayMember = "Gender1";
            comboBox1.DataSource = q;

            var q2 = db.Countries;
            comboBox2.DisplayMember = "CountryName";
            comboBox2.ValueMember = "CountryCode";
            comboBox2.DataSource = q2;

            textBox1.Text = core.Email;
            currentUser = db.Users.Where(x => x.Email.Equals(textBox1.Text)).FirstOrDefault();
            currentRunner = db.Runners.Where(x => x.Email.Equals(textBox1.Text)).FirstOrDefault();
            textBox4.Text = currentUser.FirstName;
            textBox5.Text = currentUser.LastName;
            comboBox1.SelectedText = currentRunner.Gender;
            dateTimePicker1.Value = currentRunner.DateOfBirth.Value;
            comboBox2.SelectedValue = currentRunner.CountryCode;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            parent.btnBack.PerformClick();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!parent.ValidationNotNull(this))
            {
                MessageBox.Show("All Data must be filled");
                return;
            }
            if (textBox2.Text != "")
            {
                if (textBox2.Text.Length < 6)
                {
                    MessageBox.Show("Password must be At least 6 characters");
                    return;
                }
                if (!textBox2.Text.Any(char.IsUpper))
                {
                    MessageBox.Show("Password must be At least 1 uppercase letter");
                    return;
                }

                if (!textBox2.Text.Any(char.IsNumber))
                {
                    MessageBox.Show("Password must be At least 1 Number/digit");
                    return;
                }
                string[] symbol = { "!", "@", "#", "$", "%", "^" };
                if (textBox2.Text.Where(x => symbol.Contains(x.ToString())).Count() == 0)
                {
                    MessageBox.Show("Password must be at least 1 of the following symbols: !@#$%^");
                    return;
                }
                if (!textBox3.Text.Equals(textBox2.Text))
                {
                    MessageBox.Show("The value of “Password Again” must match the value of “Password”");
                    return;
                }
            }
            if ((DateTime.Now.Year - dateTimePicker1.Value.Year) < 10)
            {
                MessageBox.Show("“Date of Birth” must be a valid date and the runner must be at least 10 years old at the time of registration");
                return;
            }

           
            currentUser.FirstName = textBox4.Text;
            currentUser.LastName = textBox5.Text;
            if (textBox2.Text != "")
            {
                currentUser.Password = textBox2.Text;
            }
            currentRunner.Gender = comboBox1.Text;
            currentRunner.DateOfBirth = dateTimePicker1.Value;
            currentRunner.CountryCode = comboBox2.SelectedValue.ToString();

            db.SubmitChanges();
            parent.btnBack.PerformClick();
        }
    }
}
