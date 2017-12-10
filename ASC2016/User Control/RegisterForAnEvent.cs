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
    public partial class RegisterForAnEvent : UserControl
    {
        Form1 parent;
        DataClasses1DataContext db = new DataClasses1DataContext();
        public RegisterForAnEvent()
        {
            InitializeComponent();
        }

        int total;
        List<string> EventSelected = new List<string>();
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                total += 145;
                EventSelected.Add(checkBox2.Tag.ToString());
            }else
            {
                total -= 145;
                EventSelected.Remove(checkBox2.Tag.ToString());
            }
            label13.Text = total.ToString();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                total += 75;
                EventSelected.Add(checkBox3.Tag.ToString());
            }else
            {
                total -= 75;
                EventSelected.Remove(checkBox3.Tag.ToString());
            }
            label13.Text = total.ToString();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                total += 20;
                EventSelected.Add(checkBox4.Tag.ToString());
            }
            else
            {
                total -= 20;
                EventSelected.Remove(checkBox4.Tag.ToString());
            }
            label13.Text = total.ToString();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            total = radioButton2.Checked ? total += 20 : total -= 20;
            label13.Text = total.ToString();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            total = radioButton3.Checked ? total += 45 : total -= 45;
            label13.Text = total.ToString();
        }

        private void RegisterForAnEvent_Load(object sender, EventArgs e)
        {
            parent = (Form1)this.ParentForm;

            var q = db.Charities;
            comboBox1.DisplayMember = "CharityName";
            comboBox1.ValueMember = "CharityId";
            comboBox1.DataSource = q;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Charity c = (Charity)comboBox1.SelectedItem;
                label15.Text = c.CharityName;
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\marathon-skills-2015-charity-data\\" + c.CharityLogo);
                label16.Text = c.CharityDescription;
            }
            catch (Exception)
            {
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parent.btnBack.PerformClick();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!parent.ValidationNotNull(this))
            {
                MessageBox.Show("All Data must be filled");
                return;
            }
            if (checkBox2.Checked == false && checkBox3.Checked == false && checkBox4.Checked == false)
            {
                MessageBox.Show("At least 1 event must be chosen");
                return;
            }
            var q = db.Runners.Where(x => x.Email.Equals(core.Email)).FirstOrDefault();

            Registration r = new Registration();
            r.RunnerId = q.RunnerId;
            r.RegistrationDateTime = DateTime.Now;
            r.RaceKitOptionId = radioButton1.Checked ? 'A' : radioButton2.Checked ? 'B' : 'C';
            r.RegistrationStatusId = 1;
            r.Cost = total;
            r.CharityId = int.Parse(comboBox1.SelectedValue.ToString());
            r.SponsorshipTarget = decimal.Parse(textBox1.Text);
            db.Registrations.InsertOnSubmit(r);
            db.SubmitChanges();

            for (int i = 0; i < EventSelected.Count; i++)
            {
                RegistrationEvent re = new RegistrationEvent();
                re.RegistrationId = r.RegistrationId;
                re.EventId = EventSelected[i];
                re.BibNumber = 0;
                re.RaceTime = 0;
                db.RegistrationEvents.InsertOnSubmit(re);
                db.SubmitChanges();
            }
            parent.LoadForm("REGISTRATIONCONFIRMATION");
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            parent.NumberOnly(e);
        }
    }
}
