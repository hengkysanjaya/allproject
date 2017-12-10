using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ASC2016.User_Control;

namespace ASC2016
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public Form1(string strFormName)
        {
            InitializeComponent();
            LoadForm(strFormName);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            displayTime();
            this.Text = "Marathon Skills 2015 ";
        }

        DateTime startDate = new DateTime(2018, 09, 05, 6, 0, 0);
        private void timer1_Tick(object sender, EventArgs e)
        {
            displayTime();
        }

        private void displayTime()
        {
            TimeSpan t = startDate - DateTime.Now;
            label2.Text = t.Days + " Days " + t.Hours + " Hours " + t.Minutes + " Minutes until the race starts";
        }


        string back;
        bool logout;
        public void LoadForm(string FormName)
        {
            this.Text = "Marathon Skills 2015";

            logout = true;
            panel2.Controls.Clear();
            if (FormName == "SPONSORARUNNER")
            {
                SponsoraRunner sr = new SponsoraRunner();
                sr.Dock = DockStyle.Fill;
                panel2.Controls.Add(sr);
                back = "";
                logout = false;
                this.Text += " - Sponsor a Runner";
            }
            else if (FormName == "SPONSORSHIPCONFIRMATION")
            {
                SponsorshipConfirmation sc = new SponsorshipConfirmation();
                sc.Dock = DockStyle.Fill;
                panel2.Controls.Add(sc);
                back = "";
                logout = false;
                this.Text += " - Sponsorship Confirmation";
            }
            else if (FormName == "FINDOUTMOREINFORMATION")
            {
                FindoutmoreInformation fo = new FindoutmoreInformation();
                panel2.Controls.Add(fo);
                back = "";
                logout = false;
                this.Text += " - Find Out more Information";
            }
            else if (FormName == "LISTOFCHARITIES")
            {
                ListofCharities lc = new ListofCharities();
                panel2.Controls.Add(lc);
                back = "FINDOUTMOREINFORMATION";
                logout = false;
                this.Text += " - List of Charities";
            }
            else if (FormName == "LOGIN")
            {
                LoginForm lf = new LoginForm();
                panel2.Controls.Add(lf);
                back = "";
                logout = false;
                this.Text += " - Login";
            }
            else if (FormName == "RUNNERMENU")
            {
                RunnerMenu rm = new RunnerMenu();
                panel2.Controls.Add(rm);
                back = "";
                logout = true;
                this.Text += " - Runner Menu";
            }
            else if (FormName == "COORDINATORMENU")
            {
                CoordinatorMenu cm = new CoordinatorMenu();
                panel2.Controls.Add(cm);
                back = "";
                logout = true;
                this.Text += " - Coordinator Menu";
            }
            else if (FormName == "ADMINISTRATORMENU")
            {
                AdministratorMenu am = new AdministratorMenu();
                panel2.Controls.Add(am);
                back = "";
                logout = true;
                this.Text += " - Administrator Menu";
            }
            else if (FormName == "CHECKIFRUNNERHASCOMPETEDBEFORE")
            {
                CheckIfRunnerhasCompetedBefore ch = new CheckIfRunnerhasCompetedBefore();
                panel2.Controls.Add(ch);
                back = "";
                logout = false;
                this.Text += " - Register as a runner";
            }
            else if (FormName == "REGISTERASARUNNER")
            {
                RunnerRegistration rr = new RunnerRegistration();
                panel2.Controls.Add(rr);
                back = "";
                logout = false;
                this.Text += " - Register as a runner";
            }
            else if (FormName == "REGISTERFORANEVENT")
            {
                RegisterForAnEvent re = new RegisterForAnEvent();
                panel2.Controls.Add(re);
                back = "RUNNERMENU";
                logout = true;
                this.Text += " - Register for an event";
            }
            else if (FormName == "REGISTRATIONCONFIRMATION")
            {
                RegistrationConfirmation rc = new RegistrationConfirmation();
                panel2.Controls.Add(rc);
                back = "RUNNERMENU";
                logout = true;
                this.Text += " - Registration Confirmation";
            }
            else if (FormName == "EDITYOURPROFILE")
            {
                EditProfile ep = new EditProfile();
                panel2.Controls.Add(ep);
                back = "RUNNERMENU";
                logout = true;
                this.Text += " - Edit runner profile";
            }
            else if (FormName == "MYRACERESULTS")
            {
                MyRaceResult mr = new MyRaceResult();
                panel2.Controls.Add(mr);
                back = "RUNNERMENU";
                logout = true;
                this.Text += " - My race results";
            }
            else if (FormName == "HOWLONGISAMARATHON")
            {
                HowlongisaMarathon hl = new HowlongisaMarathon();
                panel2.Controls.Add(hl);
                back = "FINDOUTMOREINFORMATION";
                logout = false;
                this.Text += " - How long is a marathon";
            }
            else if (FormName == "ABOUTMARATHON")
            {
                AboutMarathonSkills am = new AboutMarathonSkills();
                panel2.Controls.Add(am);
                back = "FINDOUTMOREINFORMATION";
                logout = false;
                this.Text += " - About Marathon Skills 2015";
            }
            else if (FormName == "INTERACTIVEMAP")
            {
                InteractiveMap im = new InteractiveMap();
                panel2.Controls.Add(im);
                back = "ABOUTMARATHON";
                logout = false;
                this.Text += " - Interactive Map";
            }
            else if (FormName == "PREVIOUSRACERESULTS")
            {
                PreviousRaceResults pr = new PreviousRaceResults();
                panel2.Controls.Add(pr);
                back = "FINDOUTMOREINFORMATION";
                logout = false;
                this.Text += " - Previous race results";
            }
            else if (FormName == "MYSPONSORSHIP")
            {
                mySponsorship ms = new mySponsorship();
                panel2.Controls.Add(ms);
                back = "RUNNERMENU";
                logout = true;
                this.Text += " -  My sponsorship";
            }
            else if (FormName == "RUNNERMANAGEMENT")
            {
                RunnerManagement rm = new RunnerManagement();
                panel2.Controls.Add(rm);
                back = "COORDINATORMENU";
                logout = true;
                this.Text += " -  Runner management";
            }
            else if (FormName == "MANAGEARUNNER")
            {
                ManageaRunner mr = new ManageaRunner();
                panel2.Controls.Add(mr);
                back = "RUNNERMANAGEMENT";
                logout = true;
                this.Text += " - Manage a runner";
            }
            else if (FormName == "EDITPROFILE")
            {
                EditRunnerProfile er = new EditRunnerProfile();
                panel2.Controls.Add(er);
                back = "MANAGEARUNNER";
                logout = true;
                this.Text += " - Edit runner profile";
            }
            else if (FormName == "CERTIFICATEPREVIEW")
            {
                CertificatePreview cp = new CertificatePreview();
                panel2.Controls.Add(cp);
                back = "MANAGEARUNNER";
                logout = true;
                this.Text += " - Certificate Preview";
            }
            else if (FormName == "SPONSORSHIPOVERVIEW")
            {
                SponsorshipOverview so = new SponsorshipOverview();
                panel2.Controls.Add(so);
                back = "COORDINATORMENU";
                logout = true;
                this.Text += " - Sponsorship Overview";
            }
            else if (FormName == "USERMANAGEMENT")
            {
                UserManagement um = new UserManagement();
                panel2.Controls.Add(um);
                back = "ADMINISTRATORMENU";
                logout = true;
                this.Text += " - User Management";
            }
            else if (FormName == "EDITAUSER")
            {
                Editauser eu = new Editauser();
                panel2.Controls.Add(eu);
                back = "USERMANAGEMENT";
                logout = true;
                this.Text += " - Edit a User";
            }
            else if(FormName == "ADDNEWUSER")
            {
                AddanewUser au = new AddanewUser();
                panel2.Controls.Add(au);
                back = "USERMANAGEMENT";
                logout = true;
                this.Text += " - Add a new user";
            }
            else if (FormName == "MANAGECHARITIES")
            {
                ManageCharities mc = new ManageCharities();
                panel2.Controls.Add(mc);
                back = "ADMINISTRATORMENU";
                logout = false;
                this.Text += " - Manage Charities";
            }
            else if(FormName == "ADDEDITCHARITY")
            {
                AddEditCharity ae = new AddEditCharity();
                panel2.Controls.Add(ae);
                back = "MANAGECHARITIES";
                logout = true;
            }else if(FormName == "VOLUNTEERSMANAGEMENT")
            {
                VolunteerManagement vm = new VolunteerManagement();
                panel2.Controls.Add(vm);
                back = "ADMINISTRATORMENU";
                logout = true;
                this.Text += " - Volunteers Management";
            }
            else if (FormName == "IMPORTVOLUNTEERS")
            {
                ImportVolunteers iv = new ImportVolunteers();
                panel2.Controls.Add(iv);
                back = "VOLUNTEERSMANAGEMENT";
                logout = true;
                this.Text += " - Import Voluteers";
            }
            else if (FormName == "BMICALCULATOR")
            {
                BMICalculator bmi = new BMICalculator();
                panel2.Controls.Add(bmi);
                back = "FINDOUTMOREINFORMATION";
                logout = false;
                this.Text += " - BMI Calculator";
            }
            else if (FormName == "BMRCALCULATOR")
            {
                BMRCalculator bmr = new BMRCalculator();
                panel2.Controls.Add(bmr);
                back = "FINDOUTMOREINFORMATION";
                logout = false;
                this.Text += " - BMR Calculator";
            }
            //end
            btnLogout.Visible = logout;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (back == "")
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                LoadForm(back);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {   
            this.Close();
        }

        public bool ValidationNotNull(Control ctrl)
        {
            var q = ctrl.Controls.OfType<TextBox>().Where(x => x.Text == "" &&x.Tag+"" == "");
            if (q.Count() > 0)
            {
                return false;
            }
            var q2 = ctrl.Controls.OfType<ComboBox>().Where(x => x.Text == "" || x.SelectedIndex == -1).Count();
            if (q2 > 0)
            {
                return false;
            }
            return true;
        }
        public bool CheckEmail(string Email)
        {
            //x@x.x
            string[] data1 = Email.Split('@');
            if (data1.Length != 2 || data1[0] == "" || data1[1] == "")
            {
                return false;
            }
            string[] data2 = data1[1].Split('.');
            if (data2.Length != 2 || data2[0] == "" || data2[1] == "")
            {
                return false;
            }
            return true;
        }
        public void NumberOnly(KeyPressEventArgs e)
        {
            if(char.IsLetter(e.KeyChar) || char.IsWhiteSpace(e.KeyChar) ||  char.IsSymbol(e.KeyChar) || char.IsPunctuation(e.KeyChar) || char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
