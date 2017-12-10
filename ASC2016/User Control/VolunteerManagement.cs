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
    public partial class VolunteerManagement : UserControl
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        Form1 parent;
        public VolunteerManagement()
        {
            InitializeComponent();
        }

        List<string> listsort = new List<string>() {
            "Firstname",
            "Lastname",
            "Country",
            "Gender",
        };

        private void VolunteerManagement_Load(object sender, EventArgs e)
        {
            parent = (Form1)this.ParentForm;
            comboBox1.DataSource = listsort;
            comboBox1.SelectedIndex = -1;
            LoadDGV(false);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoadDGV(true);
        }
        private void LoadDGV(bool Filter)
        {
            var q = db.Volunteers.Select(x => new
            {
                x.FirstName,
                x.LastName,
                x.Country.CountryName,
                Gender = x.Gender.ToLower().Contains("f") ? "Female" : "Male"
            });

            if(Filter)
            {
                if (comboBox1.Text == "Firstname")
                {
                    q = q.OrderBy(x => x.FirstName);

                }
                else if (comboBox1.Text == "Lastname")
                {
                    q = q.OrderBy(x => x.LastName);
                }
                else if (comboBox1.Text == "Country")
                {
                    q = q.OrderBy(x => x.CountryName);
                }
                else if (comboBox1.Text == "Gender")
                {
                    q = q.OrderBy(x => x.Gender);
                }
            }
            dataGridView1.DataSource = q;
            label6.Text = q.Count().ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parent.LoadForm("IMPORTVOLUNTEERS");
        }
    }
}
