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
    public partial class MyRaceResult : UserControl
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        Form1 parent;
        public MyRaceResult()
        {
            InitializeComponent();
        }


        List<AgeCategory> listAgeCategory = new List<AgeCategory>();
        private void MyRaceResult_Load(object sender, EventArgs e)
        {
            listAgeCategory.AddRange(new List<AgeCategory>()
            {
                new AgeCategory() {Min = 0 ,Max = 18,Display = "Under 18" },
                new AgeCategory() {Min = 18 ,Max = 29,Display = "18 to 29" },
                new AgeCategory() {Min = 30 ,Max = 39,Display = "30 to 39" },
                new AgeCategory() {Min = 40 ,Max = 55,Display = "40 to 55" },
                new AgeCategory() {Min = 56 ,Max = 70,Display = "56 to 70" },
                new AgeCategory() {Min = 71 ,Max = 1000000,Display = "Over 70" },
            });
            
            
            parent = (Form1)this.ParentForm;

            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].HeaderText = "Marathon";
            dataGridView1.Columns[1].HeaderText = "Event";
            dataGridView1.Columns[2].HeaderText = "Time";
            dataGridView1.Columns[3].HeaderText = "Overall Rank";
            dataGridView1.Columns[4].HeaderText = "Category Rank";

            var q1 = db.Runners.Where(x => x.Email.Equals(core.Email)).FirstOrDefault();
            int age = DateTime.Now.Year - q1.DateOfBirth.Value.Year;

            var query = listAgeCategory.Where(x => x.Min <= age && x.Max >= age).FirstOrDefault();
            label6.Text = query.Display;
            label4.Text = q1.Gender;

            var q = db.RegistrationEvents.Where(x=>x.Registration.Runner.Email.Equals(core.Email) && (x.RaceTime != null && x.RaceTime != 0)).ToList().Select(x => new
            {
                Marathon = x.Event.Marathon.MarathonName,
                Event = x.Event.EventName,
                Time = TimeSpan.FromSeconds(x.RaceTime.Value),
                x.RaceTime,
                x.Registration
            });

            var b = q.Select(x => new
            {   
                x.Marathon,
                x.Event,
                x.Time,
                OverallRank = "#" + q.Count(y => y.RaceTime.Value > x.RaceTime.Value) + 1,
                CategoryRank = "#" + q.Count(y => y.RaceTime.Value > x.RaceTime.Value && y.Registration.Runner.Gender == x.Registration.Runner.Gender) + 1
            });

            foreach (var a in b)
            {
                dataGridView1.Rows.Add(a.Marathon, a.Event, a.Time.Hours + "h " + a.Time.Minutes + "m " + a.Time.Seconds + "s", a.OverallRank, a.CategoryRank);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            parent.LoadForm("PREVIOUSRACERESULTS");
        }
    }
    class AgeCategory
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public string Display { get; set; }
    }

}
