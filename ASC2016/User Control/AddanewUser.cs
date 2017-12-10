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
    public partial class AddanewUser : UserControl
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        Form1 parent;
        public AddanewUser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parent.btnBack.PerformClick();
        }

        private void AddanewUser_Load(object sender, EventArgs e)
        {
            parent = (Form1)this.ParentForm;
            comboBox1.DisplayMember = "RoleName";
            comboBox1.ValueMember = "RoleId";
            comboBox1.DataSource = db.Roles;
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
            try
            {
                User u = new User();
                u.Email = textBox1.Text;
                u.Password = textBox3.Text;
                u.FirstName = textBox4.Text;
                u.LastName = textBox5.Text;
                u.RoleId = comboBox1.SelectedValue.ToString()[0];
                db.Users.InsertOnSubmit(u);

                db.SubmitChanges();
                MessageBox.Show("User successfully Added");
                parent.btnBack.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
