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
    public partial class CoordinatorMenu : UserControl
    {
        Form1 parent;
        public CoordinatorMenu()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            parent.LoadForm("RUNNERMANAGEMENT");
        }

        private void CoordinatorMenu_Load(object sender, EventArgs e)
        {
            parent = (Form1)this.ParentForm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parent.LoadForm("SPONSORSHIPOVERVIEW");
        }
    }
}
