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
    public partial class CertificatePreview : UserControl
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        Form1 parent;
        public CertificatePreview()
        {
            InitializeComponent();
        }

        private void CertificatePreview_Load(object sender, EventArgs e)
        {
            comboBox1.DisplayMember = "EventName";
            comboBox1.ValueMember = "EventId";
            comboBox1.DataSource = db.Events.Where(x => x.MarathonId == 4);

            Find();
        }

        private void Find()
        {
            var run = db.Runners.Where(x => x.Email.Equals(core.Email)).FirstOrDefault();
            var q = db.RegistrationEvents.Where(x => x.RegistrationId.Equals(core.RegistrationId) && x.EventId.Equals(comboBox1.SelectedValue.ToString())).FirstOrDefault();
            if (q != null)
            {
                panel2.Visible = false;

                TimeSpan t = TimeSpan.FromSeconds(q.RaceTime.Value);
                string RaceTime = t.Hours + "h " + t.Minutes + "m " + t.Seconds + "s ";


                var q2 = db.RegistrationEvents.Where(x => x.Registration.Runner.Email.Equals(core.Email) && x.EventId.Equals(comboBox1.SelectedValue.ToString()) && (x.RaceTime != null || x.RaceTime == 0)).ToList().Select(x => new
                {
                    x.Registration.Runner.Email,
                    Marathon = x.Event.Marathon.MarathonName,
                    Event = x.Event.EventName,
                    Time = TimeSpan.FromSeconds(x.RaceTime.Value),
                    OverallRank = "#" + db.RegistrationEvents.Count(y => y.RaceTime.Value > x.RaceTime.Value) + 1,
                    CategoryRank = "#" + db.RegistrationEvents.Count(y => y.RaceTime.Value > x.RaceTime.Value && y.Registration.Runner.Gender == x.Registration.Runner.Gender) + 1,
                    Regis = x.Registration
                }).Where(x => x.Email.Equals(core.Email)).FirstOrDefault();


                label1.Text = "Congratulations " + run.User.FirstName + " " + run.User.LastName + " for running in the 42km Full Marathon." +
                    "You ran a time of " + RaceTime + " and got a rank of " + q2.OverallRank + "!";

                parent = (Form1)this.ParentForm;

                var q3 = db.Sponsorships.Where(x => x.RegistrationId.Equals(core.RegistrationId)).Sum(x => x.Amount);

                label6.Text = "You also raised " + q3.ToString("C") + " for " + q2.Regis.Charity.CharityName;
            }
            else
            {
                MessageBox.Show("They do not have a race time recorded or they were not registered in the event");
                panel2.Visible = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex != -1)
            {
                Find();
            }
        }
    }
}
