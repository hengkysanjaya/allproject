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
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ASC2016
{
    public partial class MainScreen : Form
    {
        public MainScreen()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            displayTime();
        }

        DateTime startDate = new DateTime(2018, 09, 05, 6, 0, 0);
        private void displayTime()
        {
            TimeSpan t = startDate - DateTime.Now;
            label2.Text = t.Days + " Days " + t.Hours + " Hours " + t.Minutes + " Minutes until the race starts";
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            displayTime();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1("SPONSORARUNNER");
            core.sponsorshipid = "1";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.Show();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1("FINDOUTMOREINFORMATION");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1("LOGIN");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.Show();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1("CHECKIFRUNNERHASCOMPETEDBEFORE");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            DataClasses1DataContext db = new DataClasses1DataContext();
            var cmd = db.Connection.CreateCommand();
            cmd.CommandText = "select * from gender";
            var q = cmd.ExecuteReader();
            MessageBox.Show(q[0].ToString());
            
            var a = (HatchStyle[])Enum.GetValues(typeof(HatchStyle));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string testing = "test.xlsx";
            MessageBox.Show(System.IO.Path.GetExtension(testing));
            int a = 10000;
            string ab = string.Format("{0:C0}", a);
            MessageBox.Show(ab);
        }
    }
}
