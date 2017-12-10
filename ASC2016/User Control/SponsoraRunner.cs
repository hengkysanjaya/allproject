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
    public partial class SponsoraRunner : UserControl
    {
        Form1 parent;
        DataClasses1DataContext db = new DataClasses1DataContext();
        
        public SponsoraRunner()
        {
            InitializeComponent();
        }

        private void SponsoraRunner_Load(object sender, EventArgs e)
        {
            parent = (Form1)this.ParentForm;
            LoadRunner();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!parent.ValidationNotNull(this))
            {
                MessageBox.Show("All data must be filled");
                return;
            }
            if(txtCreditCard.Text.Length < 16)
            {
                MessageBox.Show("Credit card has to be 16 digits");
                return;
            }
            int year = int.Parse(txtYear.Text);
            int month = int.Parse(txtMonth.Text);
            if (year == DateTime.Now.Year)
            {
                if (month <= DateTime.Now.Month)
                {
                    MessageBox.Show("Expiry date must be a valid month and year that is after today’s date.");
                    return;
                }
            }
            else if (year < DateTime.Now.Year)
            {
                MessageBox.Show("Expiry date must be a valid month and year that is after today’s date.");
                return;
            }
            if(txtCVC.Text.Length != 3)
            {
                MessageBox.Show("CVC is a security code that has to be 3 digits.");
                return;
            }

            Sponsorship s = new Sponsorship();
            s.SponsorName = txtName.Text;
            s.RegistrationId = int.Parse(comboBox2.SelectedValue.ToString());
            s.Amount = int.Parse(textBox6.Text);
            db.Sponsorships.InsertOnSubmit(s);
            db.SubmitChanges();

            core.sponsorshipid = s.SponsorshipId.ToString();

            parent.LoadForm("SPONSORSHIPCONFIRMATION");
        }
        private void LoadRunner()
        {
            var q = db.RegistrationEvents.Where(x => x.Event.Marathon.YearHeld.ToString() == "2015").Select(x => new
            {
                display = x.Registration.Runner.User.LastName + "," + x.Registration.Runner.User.FirstName + "-" + x.BibNumber + "(" + x.Registration.Runner.CountryCode + ")",
                value = x.RegistrationId,
            });
            comboBox2.ValueMember = "value";
            comboBox2.DisplayMember = "display";
            comboBox2.DataSource = q;
        }

        
        private void btnBack_Click(object sender, EventArgs e)
        {
            label13.Text = textBox6.Text;
            int sponsorship = int.Parse(textBox6.Text);
            sponsorship -= 10;
            if(sponsorship >= 0)
            {
                textBox6.Text = sponsorship.ToString();
                label13.Text = sponsorship.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int sponsorship = int.Parse(textBox6.Text);
            sponsorship += 10;

            textBox6.Text = sponsorship.ToString();
            label13.Text = sponsorship.ToString();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        
        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            var q = db.Registrations.Where(x => x.RegistrationId.Equals(comboBox2.SelectedValue.ToString())).FirstOrDefault();
            var charity = q.Charity;
            label15.Text = charity.CharityName;
            pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\marathon-skills-2015-charity-data\\" + charity.CharityLogo);
            label16.Text = charity.CharityDescription;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            parent.btnBack.PerformClick();
        }

        private void txtCreditCard_KeyPress(object sender, KeyPressEventArgs e)
        {
            parent.NumberOnly(e);
        }

        private void txtYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            parent.NumberOnly(e);
        }

        private void txtMonth_KeyPress(object sender, KeyPressEventArgs e)
        {
            parent.NumberOnly(e);
        }

        private void txtCVC_KeyPress(object sender, KeyPressEventArgs e)
        {
            parent.NumberOnly(e);
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            parent.NumberOnly(e);
        }
    }
}
