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
    public partial class FindoutmoreInformation : UserControl
    {
        Form1 parent;
        public FindoutmoreInformation()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            parent.LoadForm("LISTOFCHARITIES");
        }

        private void FindoutmoreInformation_Load(object sender, EventArgs e)
        {
            parent = (Form1)this.ParentForm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parent.LoadForm("HOWLONGISAMARATHON");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            parent.LoadForm("ABOUTMARATHON");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            parent.LoadForm("PREVIOUSRACERESULTS");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            parent.LoadForm("BMICALCULATOR");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            parent.LoadForm("BMRCALCULATOR");
        }
    }
}
