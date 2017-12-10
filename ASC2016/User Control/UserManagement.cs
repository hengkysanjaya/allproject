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
    public partial class UserManagement : UserControl
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        Form1 parent;
        public UserManagement()
        {
            InitializeComponent();
        }

        
        private void UserManagement_Load(object sender, EventArgs e)
        {
            parent = (Form1)this.ParentForm;

            comboBox1.Items.Add("All Roles");
            comboBox1.Items.Add("Coordinator");
            comboBox1.Items.Add("Runner");
            comboBox1.SelectedIndex = 0;

            comboBox2.Items.Add("FirstName");
            comboBox2.Items.Add("LastName");
            comboBox2.Items.Add("Email");
            comboBox2.Items.Add("Role");
            comboBox2.SelectedIndex = 0;

            LoadDGV(true, true, true);
        }

        private void LoadDGV(bool statusFilter = false,bool StatusWhere = false,bool StatusOrder = false)
        {
            dataGridView1.Columns.Clear();

            var q = db.Users.Select(x => new
            {
                x.FirstName,
                x.LastName,
                x.Email,
                x.Role.RoleName
            });

            if (StatusWhere && textBox1.Text != "")
            {
                q = q.Where(x => x.FirstName.Contains(textBox1.Text) || x.LastName.Contains(textBox1.Text) || x.Email.Contains(textBox1.Text));
            }

            if (statusFilter)
            {
                if (comboBox1.Text == "All Roles")
                {
                    q = q.Where(x => x.RoleName != "");

                }
                else if (comboBox1.Text == "Runner")
                {
                    q = q.Where(x => x.RoleName == "Runner");
                }
                else if (comboBox1.Text == "Coordinator")
                {
                    q = q.Where(x => x.RoleName == "Coordinator");
                }
            }

            if (StatusOrder)
            {
                if(comboBox2.Text == "FirstName")
                {
                    q = q.OrderBy(x => x.FirstName);
                }
                else if (comboBox2.Text == "LastName")
                {
                    q = q.OrderBy(x => x.LastName);
                }
                else if(comboBox2.Text == "Email")
                {
                    q = q.OrderBy(x => x.Email);
                }
                else if (comboBox2.Text == "Role")
                {
                    q = q.OrderBy(x => x.RoleName);
                }
            }

            dataGridView1.DataSource = q;
            DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
            btnColumn.UseColumnTextForButtonValue = true;
            btnColumn.Text = "Edit";
            dataGridView1.Columns.Add(btnColumn);

            if(q.Count() == 0)
            {
                MessageBox.Show("There is no any results");
            }

            label5.Text = q.Count().ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadDGV(true,true,true);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.Columns[e.ColumnIndex].HeaderText == "")
            {
                core.Email = dataGridView1.CurrentRow.Cells["Email"].Value.ToString();
                parent.LoadForm("EDITAUSER");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            parent.LoadForm("ADDNEWUSER");

        }
    }
}
