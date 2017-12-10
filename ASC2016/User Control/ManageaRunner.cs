using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ASC2016.Properties;

namespace ASC2016.User_Control
{
    public partial class ManageaRunner : UserControl
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        Form1 parent;
        public ManageaRunner()
        {
            InitializeComponent();
        }

        private void ManageaRunner_Load(object sender, EventArgs e)
        {
            parent = (Form1)this.ParentForm;

            var q = db.Registrations.Where(x => x.RegistrationId.Equals(core.RegistrationId)).FirstOrDefault();
            var r = q.Runner;
            var u = q.Runner.User;

            lblEmail.Text = core.Email;
            lblFirstName.Text = u.FirstName;
            lblLastName.Text = u.LastName;
            lblGender.Text = r.Gender;
            lblDateOfBirth.Text = r.DateOfBirth.Value.ToString();
            lblCountry.Text = r.Country.CountryName;
            lblCharity.Text = q.Charity.CharityName;
            lblTargetToRaise.Text = q.SponsorshipTarget.ToString("C");
            lblRacekitoption.Text = q.RaceKitOption.RaceKitOption1;
            lblRaceEvent.Text = string.Join("\n", q.RegistrationEvents.Select(x => x.Event.EventName).ToList());

            for (int i = 1; i <= q.RegistrationStatusId; i++)
            {
                if (i >= 1)
                {
                    pictureBox1.Image = Resources.tick_icon;
                }
                 if (i >= 2)
                {
                    pictureBox2.Image = Resources.tick_icon;
                }
                 if (i >= 3)
                {
                    pictureBox3.Image = Resources.tick_icon;
                }
                 if (i >= 4)
                {
                    pictureBox4.Image = Resources.tick_icon;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parent.LoadForm("CERTIFICATEPREVIEW");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            parent.LoadForm("EDITPROFILE");
        }
    }
}
