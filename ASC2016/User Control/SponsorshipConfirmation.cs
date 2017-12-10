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
    public partial class SponsorshipConfirmation : UserControl
    {
        Form1 parent;
        DataClasses1DataContext db = new DataClasses1DataContext();
        public SponsorshipConfirmation()
        {
            InitializeComponent();
        }

        private void SponsorshipConfirmation_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(this.Size.ToString());
            parent = (Form1)this.ParentForm;
            var q = db.Sponsorships.Where(x => x.SponsorshipId.Equals(core.sponsorshipid)).Select(x => new
            {
                display = x.Registration.Runner.User.FirstName + " " + x.Registration.Runner.User.LastName + " (" + x.Registration.Runner.CountryCode + ") from " + x.Registration.Runner.CountryCode,
                x.Amount
            }).FirstOrDefault();
            label3.Text = q.display;
            label13.Text = q.Amount.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            parent.btnBack.PerformClick();
        }
    }
}
