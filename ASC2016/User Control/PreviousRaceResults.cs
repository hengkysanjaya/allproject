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
    public partial class PreviousRaceResults : UserControl
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        Form1 parent;
        public PreviousRaceResults()
        {
            InitializeComponent();
        }

   
        List<AgeCategory> listAgeCategory = new List<AgeCategory>();
        private void PreviousRaceResults_Load(object sender, EventArgs e)
        {
            listAgeCategory.AddRange(new List<AgeCategory>()
            {
                new AgeCategory() {Min = 0 ,Max = 18,Display = "Under 18" },
                new AgeCategory() {Min = 18 ,Max = 29,Display = "18 to 29" },
                new AgeCategory() {Min = 30 ,Max = 39,Display = "30 to 39" },
                new AgeCategory() {Min = 40 ,Max = 55,Display = "40 to 55" },
                new AgeCategory() {Min = 56 ,Max = 70,Display = "56 to 70" },
                new AgeCategory() {Min = 71 ,Max = int.MaxValue,Display = "Over 70" },
            });

            parent = (Form1)this.ParentForm;
          
            comboMarathon.DisplayMember = "display";
            comboMarathon.ValueMember = "value";
            comboMarathon.DataSource = db.Marathons.Select(x => new {
                display = x.YearHeld + " - " + x.CityName,
                value = x.MarathonId
            });


            var q = db.Genders;
            comboGender.Items.Add("Any");
            foreach(var a in q)
            {
                comboGender.Items.Add(a.Gender1);
            }

            comboAgeCategory.DisplayMember = "Display";
            comboAgeCategory.DataSource = listAgeCategory;
        }

        List<Event> listEvent;
        private void comboMarathon_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                listEvent = new List<Event>(db.Events.Where(x => x.MarathonId.Equals(comboMarathon.SelectedValue.ToString())).ToList());
                comboRaceEvent.DisplayMember = "EventName";
                comboRaceEvent.ValueMember = "EventId";
                comboRaceEvent.DataSource = listEvent;
            }
            catch (Exception)
            {
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            var q = db.RegistrationEvents.Where(x => x.Event.MarathonId.Equals(comboMarathon.SelectedValue.ToString()) && x.EventId.Equals(comboRaceEvent.SelectedValue.ToString()) && x.RaceTime.HasValue && x.RaceTime != 0);

            if(comboGender.Text != "Any")
            {
                q = q.Where(x => x.Registration.Runner.Gender.Equals(comboGender.Text));
            }

            var currentAgeCategory = (AgeCategory)comboAgeCategory.SelectedItem;
            q = q.Where(x => ((DateTime.Now.Year - x.Registration.Runner.DateOfBirth.Value.Year) > currentAgeCategory.Min) && ((DateTime.Now.Year - x.Registration.Runner.DateOfBirth.Value.Year) < currentAgeCategory.Max));

            label6.Text = q.Count().ToString();

            label9.Text = q.Count().ToString();

            TimeSpan AvgTime = TimeSpan.FromSeconds(q.Average(x => x.RaceTime).Value);
            string AvgRaceTime = AvgTime.Hours + "h " + AvgTime.Minutes + "m " + AvgTime.Seconds + "s ";
            label11.Text = AvgRaceTime;


            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].HeaderText = "Rank";
            dataGridView1.Columns[1].HeaderText = "Race Time";
            dataGridView1.Columns[2].HeaderText = "Runner Name";
            dataGridView1.Columns[3].HeaderText = "Country";

            var data = q.Select(x => new
            {
                Rank = q.Count(y => y.RaceTime.Value > x.RaceTime.Value) + 1,
                RaceTime = x.RaceTime.Value,
                RunnerName = x.Registration.Runner.User.FirstName + x.Registration.Runner.User.LastName,
                CountryName = x.Registration.Runner.CountryCode
            }).OrderBy(x => x.Rank);

            if(data.Count() == 0)
            {
                MessageBox.Show("There is no result");
            }

            foreach(var a in data)
            {
                TimeSpan t = TimeSpan.FromSeconds(a.RaceTime);
                string RaceTime = t.Hours + "h " + t.Minutes + "m " + t.Seconds + "s";
                dataGridView1.Rows.Add(a.Rank, RaceTime, a.RunnerName, a.CountryName);
            }
        }
    }
    
}

