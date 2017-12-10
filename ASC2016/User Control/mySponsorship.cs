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
    public partial class mySponsorship : UserControl
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        public mySponsorship()
        {
            InitializeComponent();
        }

        private void mySponsorship_Load(object sender, EventArgs e)
        {
            try
            {
                //dataGridView1.Columns.Clear();

                var q = db.Registrations.Where(x => x.Runner.Email.Equals(core.Email)).FirstOrDefault();
                label3.Text = q.Charity.CharityName;
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\marathon-skills-2015-charity-data\\" + q.Charity.CharityLogo);
                label4.Text = q.Charity.CharityDescription;

                var q2 = db.Sponsorships.Where(x => x.Registration.Runner.Email.Equals(core.Email)).Select(x => new
                {
                    x.SponsorName,
                    x.Amount
                });
                if(q2.Count() == 0)
                {
                    MessageBox.Show("You dont have any sponsors");
                    return;
                }

                foreach(var a in q2)
                {
                    dataGridView1.Rows.Add(a.SponsorName, a.Amount.ToString("C"));
                }
                label5.Text = "Total : " + q2.Sum(x => x.Amount).ToString("C");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
