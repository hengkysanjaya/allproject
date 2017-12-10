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
    public partial class RunnerMenu : UserControl
    {
        Form1 parent;
        public RunnerMenu()
        {
            InitializeComponent();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            parent.LoadForm("REGISTERFORANEVENT");
        }

        private void RunnerMenu_Load(object sender, EventArgs e)
        {
            parent = (Form1)this.ParentForm;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            parent.LoadForm("EDITYOURPROFILE");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parent.LoadForm("MYRACERESULTS");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            parent.LoadForm("MYSPONSORSHIP");
        }
    }
}
