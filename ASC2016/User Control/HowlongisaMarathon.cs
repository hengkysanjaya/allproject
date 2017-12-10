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
    public partial class HowlongisaMarathon : UserControl
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        Form1 parent;
        public HowlongisaMarathon()
        {
            InitializeComponent();
        }

        private void HowlongisaMarathon_Load(object sender, EventArgs e)
        {
            parent = (Form1)this.ParentForm;

            label2.Text = "";
            label3.Text = "";
            
            LoadSpeed();
            LoadDistance();

        }
        List<string> listSpeed = new List<string>()
       {
           "F1 Car,f1-car.jpg,345",
            "Slug,slug.jpg,0.01",
            "Horse,horse.png,15",
            "Sloth,sloth.jpg,0.12",
            "Capybara,capybara.jpg,35",
            "Jaguar,jaguar.jpg,80",
            "Worm,worm.jpg,0.03"
       };

        List<string> listDistance = new List<string>()
        {
            "Bus,bus.jpg,10",
            "Pair of Havaianas,pair-of-havaianas.jpg,0.245",
            "Airbus A380,airbus-a380.jpg,73",
            "Football Field,football-field.jpg,105",
            "Ronaldinho,ronaldinho.jpg,1.81"
        };
        private void LoadDistance()
        {
            tabPage2.Controls.Clear();

            int y = 10;
            foreach (var a in listDistance)
            {
                string[] data = a.Split(',');

                Panel pnl = new Panel();
                PictureBox pb = new PictureBox();
                Label lblItemName = new Label();

                pnl.Location = new System.Drawing.Point(6, y);
                pnl.Name = "panel1";
                pnl.Size = new System.Drawing.Size(302, 78);
                pnl.TabIndex = 0;

                pb.Dock = System.Windows.Forms.DockStyle.Left;
                pb.Location = new System.Drawing.Point(0, 0);
                pb.Name = "pictureBox2";
                pb.Size = new System.Drawing.Size(84, 78);
                pb.TabIndex = 23;
                pb.TabStop = false;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.Image = Image.FromFile(Application.StartupPath + "\\marathon-skills-2015-how-long-data\\" + data[1]);
                // 
                // label4
                // 
                lblItemName.Dock = System.Windows.Forms.DockStyle.Fill;
                lblItemName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                lblItemName.Location = new System.Drawing.Point(84, 0);
                lblItemName.Name = "label4";
                lblItemName.Size = new System.Drawing.Size(218, 78);
                lblItemName.TabIndex = 24;
                lblItemName.Text = data[0];
                lblItemName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                pnl.Click += Distance_Click;
                lblItemName.Click += Distance_Click;
                pb.Click += Distance_Click;


                pnl.Tag = data[0];
                lblItemName.Tag = data[0];
                pb.Tag = data[0];

                pnl.Controls.Add(lblItemName);
                pnl.Controls.Add(pb);
                tabPage2.Controls.Add(pnl);
                y += 90;
            }

        }

        private void Distance_Click(object sender, EventArgs e)
        {
            var obj = ((Control)sender);
            string tag = obj.Tag + "";
            var q = listDistance.Where(x => x.Contains(tag)).FirstOrDefault().Split(',');
            label2.Text = q[0];
            pictureBox1.Image =  Image.FromFile(Application.StartupPath + "\\marathon-skills-2015-how-long-data\\" + q[1]);

            double s = 42;
            double v = double.Parse(q[2]);
            double t = s / v / 3600;
            TimeSpan ts = TimeSpan.FromSeconds(t);

            label3.Text = "The top speed of a " + q[0] + " is " + q[2] + ". It would take " + ts.Hours + " Hours " + ts.Minutes + " Minures to complete a 42km marathon";
            // S = v * t
        }

        private void LoadSpeed()
        {
            tabPage1.Controls.Clear();
            int y = 10;
            foreach(var a in listSpeed)
            {
                string[] data = a.Split(',');

                Panel pnl = new Panel();
                PictureBox pb = new PictureBox();
                Label lblItemName = new Label();

                pnl.Location = new System.Drawing.Point(6, y);
                pnl.Name = "panel1";
                pnl.Size = new System.Drawing.Size(302, 78);
                pnl.TabIndex = 0;

                pb.Dock = System.Windows.Forms.DockStyle.Left;
                pb.Location = new System.Drawing.Point(0, 0);
                pb.Name = "pictureBox2";
                pb.Size = new System.Drawing.Size(84, 78);
                pb.TabIndex = 23;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.TabStop = false;
                pb.Image = Image.FromFile(Application.StartupPath + "\\marathon-skills-2015-how-long-data\\"+data[1]);
                // 
                // label4
                // 
                lblItemName.Dock = System.Windows.Forms.DockStyle.Fill;
                lblItemName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                lblItemName.Location = new System.Drawing.Point(84, 0);
                lblItemName.Name = "label4";
                lblItemName.Size = new System.Drawing.Size(218, 78);
                lblItemName.TabIndex = 24;
                lblItemName.Text = data[0];
                lblItemName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;


                pnl.Click += Speed_Click;
                lblItemName.Click += Speed_Click;
                pb.Click += Speed_Click;


                pnl.Tag = data[0];
                lblItemName.Tag = data[0];
                pb.Tag = data[0];

                pnl.Controls.Add(lblItemName);
                pnl.Controls.Add(pb);
                tabPage1.Controls.Add(pnl);

                y += 90;
            }
            
            // 
        }

        private void Speed_Click(object sender, EventArgs e)
        {
            var obj = ((Control)sender);
            string tag = obj.Tag + "";

            var q = listSpeed.Where(x => x.Contains(tag)).FirstOrDefault().Split(',');
            label2.Text = q[0];
            pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\marathon-skills-2015-how-long-data\\" + q[1]);

            decimal numberofItems = 42 / decimal.Parse(q[2].ToString());

            label3.Text = "The length of a "+q[0]+" is "+q[2]+". It would take "+numberofItems+" of them to cover the track of a 42km marathon";
            
        }
    }
}
