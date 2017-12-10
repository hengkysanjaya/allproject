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
    public partial class LoginForm : UserControl
    {
        Form1 parent;
        DataClasses1DataContext db = new DataClasses1DataContext();
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!parent.ValidationNotNull(this))
            {
                MessageBox.Show("Please input email and password");
                return;
            }
            var q = db.Users.Where(x => x.Email.Equals(textBox1.Text)).FirstOrDefault();
            if (q == null)
            {
                MessageBox.Show("Email not found");
            }
            else
            {
                var q2 = db.Users.Where(x => x.Email.Equals(textBox1.Text) && x.Password.Equals(textBox2.Text)).FirstOrDefault();
                if (q2 == null)
                {
                    MessageBox.Show("Password not found");
                }
                else
                {
                    string role = q2.RoleId.ToString();
                    core.Email = q2.Email;
                    if (role == "R")
                    {
                        parent.LoadForm("RUNNERMENU");
                    }
                    else if (role == "A")
                    {
                        parent.LoadForm("ADMINISTRATORMENU");
                    }
                    else if (role == "C")
                    {
                        parent.LoadForm("COORDINATORMENU");
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parent.btnBack.PerformClick();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            parent = (Form1)this.ParentForm;
        }
    }
}
