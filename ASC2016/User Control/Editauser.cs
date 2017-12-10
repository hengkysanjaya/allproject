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
    public partial class Editauser : UserControl
    {
        Form1 parent;
        DataClasses1DataContext db = new DataClasses1DataContext();
        public Editauser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parent.btnBack.PerformClick();
        }

        User currentUser;
        private void Editauser_Load(object sender, EventArgs e)
        {
            parent = (Form1)this.ParentForm;

            var role = db.Roles;
            comboBox1.DisplayMember = "RoleName";
            comboBox1.ValueMember = "RoleId";
            comboBox1.DataSource = role;

            textBox1.Text = core.Email;
            currentUser = db.Users.Where(x => x.Email.Equals(core.Email)).FirstOrDefault();
            textBox4.Text = currentUser.FirstName;
            textBox5.Text = currentUser.LastName;
            comboBox1.SelectedValue = currentUser.RoleId;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!parent.ValidationNotNull(this))
            {
                MessageBox.Show("All Data must be filled");
                return;
            }
            currentUser.FirstName = textBox4.Text;
            currentUser.LastName = textBox5.Text;
            currentUser.RoleId = comboBox1.SelectedValue.ToString()[0];

            if(textBox2.Text !="" && textBox3.Text != "")
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
                currentUser.Password = textBox3.Text;
            }
            db.SubmitChanges();
            parent.btnBack.PerformClick();

        }
    }
}
