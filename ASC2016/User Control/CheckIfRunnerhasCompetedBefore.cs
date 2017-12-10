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
    public partial class CheckIfRunnerhasCompetedBefore : UserControl
    {
        Form1 parent;
        public CheckIfRunnerhasCompetedBefore()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parent.LoadForm("LOGIN");
        }

        private void CheckIfRunnerhasCompetedBefore_Load(object sender, EventArgs e)
        {
            parent = (Form1)this.ParentForm;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            parent.LoadForm("REGISTERASARUNNER");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            parent.LoadForm("LOGIN");
        }
    }
}
