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
    public partial class AboutMarathonSkills : UserControl
    {
        Form1 parent;
        public AboutMarathonSkills()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            parent.LoadForm("INTERACTIVEMAP");   
        }

        private void AboutMarathonSkills_Load(object sender, EventArgs e)
        {
            parent = (Form1)this.ParentForm;
        }
    }
}
