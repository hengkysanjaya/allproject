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
    public partial class RunnerRegistration : UserControl
    {
        Form1 parent;
        DataClasses1DataContext db = new DataClasses1DataContext();
        public RunnerRegistration()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!parent.ValidationNotNull(this))
            {
                MessageBox.Show("All Data must be filled");
                return;
            }
            if (!parent.CheckEmail(textBox1.Text))
            {
                MessageBox.Show("Email address must be in a valid format, e.g. x@x.x");
                return;
            }
            if(textBox2.Text.Length < 6)
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
            if (textBox2.Text.Where(x=>symbol.Contains(x.ToString())).Count() == 0)
            {
                MessageBox.Show("Password must be at least 1 of the following symbols: !@#$%^");
                return;
            }
            if (!textBox3.Text.Equals(textBox2.Text))
            {
                MessageBox.Show("The value of “Password Again” must match the value of “Password”");
                return;
            }
            if ((DateTime.Now.Year - dateTimePicker1.Value.Year) < 10)
            {
                MessageBox.Show("“Date of Birth” must be a valid date and the runner must be at least 10 years old at the time of registration");
                return;
            }

            User u = new User();
            u.Email = textBox1.Text;
            u.Password = textBox3.Text;
            u.FirstName = textBox4.Text;
            u.LastName = textBox5.Text;
            u.RoleId = 'R';
            db.Users.InsertOnSubmit(u);

            Runner r = new Runner();
            r.Email = u.Email;
            r.Gender = comboBox1.Text;
            r.DateOfBirth = dateTimePicker1.Value;
            r.CountryCode = comboBox2.SelectedValue.ToString();
            db.Runners.InsertOnSubmit(r);
            db.SubmitChanges();

            core.Email = textBox1.Text;
            parent.LoadForm("REGISTERFORANEVENT");
                    
        }

        private void RunnerRegistration_Load(object sender, EventArgs e)
        {
            parent = (Form1)this.ParentForm;

            var q = db.Genders;
            comboBox1.DisplayMember = "Gender1";
            comboBox1.DataSource = q;

            var q2 = db.Countries;
            comboBox2.DisplayMember = "CountryName";
            comboBox2.ValueMember = "CountryCode";
            comboBox2.DataSource = q2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parent.btnBack.PerformClick();
        }
    }
}
