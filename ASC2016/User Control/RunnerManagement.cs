using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ASC2016.User_Control
{
    public partial class RunnerManagement : UserControl
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        Form1 parent;
        public RunnerManagement()
        {
            InitializeComponent();
        }

        List<string> listSortBy = new List<string>();
        private void RunnerManagement_Load(object sender, EventArgs e)
        {
            listSortBy = new List<string>()
            {
                "First Name",
                "Last Name",
                "Email",
                "Status",
            };

            parent = (Form1)this.ParentForm;

            var q = db.RegistrationStatus.ToList();
            comboStatus.DisplayMember = "RegistrationStatus1";
            comboStatus.ValueMember = "RegistrationStatusId";
            comboStatus.DataSource = q;

            var q2 = db.Events.Where(x => x.Marathon.YearHeld == 2015).ToList();
            comboRaceEvent.DisplayMember = "EventName";
            comboRaceEvent.ValueMember = "EventId";
            comboRaceEvent.DataSource = q2;

            comboSortBy.DataSource = listSortBy;

        }

        List<Registration> TampungList = new List<Registration>();
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();

            var q = db.Registrations.Where(x => x.RegistrationEvents.Where(y=>y.EventId.Equals(comboRaceEvent.SelectedValue.ToString())).Count() > 0);

            q = q.Where(x => x.RegistrationStatusId.Equals(comboStatus.SelectedValue.ToString()));

            if (comboSortBy.Text == "First Name")
            {
                q = q.OrderBy(x => x.Runner.User.FirstName);
            }
            else if (comboSortBy.Text == "Last Name")
            {
                q = q.OrderBy(x => x.Runner.User.LastName);
            }
            else if (comboSortBy.Text == "Email")
            {
                q = q.OrderBy(x => x.Runner.Email);
            }
            else if (comboSortBy.Text == "Status")
            {
                q = q.OrderBy(x => x.RegistrationStatus.RegistrationStatus1);
            }
            if(q.Count() == 0)
            {
                MessageBox.Show("There is any results");
            }

            TampungList = q.ToList();

            dataGridView1.DataSource = q.Select(x => new
            {
                x.RegistrationId,
                x.Runner.User.FirstName,
                x.Runner.User.LastName,
                x.Runner.Email,
                Status = x.RegistrationStatus.RegistrationStatus1,
            });
            label8.Text = q.Count().ToString();
            dataGridView1.Columns[0].Visible = false;

            DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
            btnColumn.UseColumnTextForButtonValue = true;
            btnColumn.Text = "Edit";
            dataGridView1.Columns.Add(btnColumn);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 5)
            {
                core.Email = dataGridView1.CurrentRow.Cells["Email"].Value.ToString();
                core.RegistrationId = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                parent.LoadForm("MANAGEARUNNER");
            }
        }

        private void btnCSV_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sf = new SaveFileDialog())
            {
                sf.Filter = "CSV Files|*.csv";
                StringBuilder sb = new StringBuilder();
                if(sf.ShowDialog() == DialogResult.OK)
                {
                    foreach(var a in TampungList)
                    {
                        string str = a.Runner.User.FirstName + ";" + a.Runner.User.LastName
                            + ";" + a.Runner.Email + ";" + a.Runner.Gender1 + ";" + a.Runner.CountryCode + ";" +
                            a.Runner.DateOfBirth + ";" + a.RegistrationStatus.RegistrationStatus1 + ";" +
                            string.Join(",", a.RegistrationEvents.Select(x => x.Event.EventName).ToList());

                        sb.AppendLine(str);
                    }
                    File.WriteAllText(sf.FileName, sb.ToString());
                    MessageBox.Show("CSV Export Successfully");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            richTextBox1.Text = string.Join(";", TampungList.Select(x =>
             "\"" + x.Runner.User.FirstName + " " + x.Runner.User.LastName + "\"" + "<" + x.Runner.Email + ">" + Environment.NewLine
            ));

            panel1.Visible = true;
        }

        private void label9_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }
    }
}
