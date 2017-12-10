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
    public partial class BMRCalculator : UserControl
    {
        Form1 parent;
        public BMRCalculator()
        {
            InitializeComponent();
        }

        private void KeyPress(object sender, KeyPressEventArgs e)
        {
            parent.NumberOnly(e);
        }

        private void BMRCalculator_Load(object sender, EventArgs e)
        {
            parent = (Form1)this.ParentForm;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!parent.ValidationNotNull(this))
            {
                MessageBox.Show("All Data must be filled");
                return;
            }
            double Height = double.Parse(textBox1.Text);
            double Weight = double.Parse(textBox2.Text);
            double Age = double.Parse(textBox3.Text);
            double BMR = 0;
            if (Gender == "Male")
            {
                //BMR = 66 + (13.7 x weight) +(5 x height) -(6.8 x age)
                BMR = 66 + (13.7 * Weight) + (5 * Height) - (6.8 * Age);
            }
            else if (Gender == "Female")
            {
                //BMR = 65 + (9.6 x weight) + (1.8 x height) - (4.7 x age)
                BMR = 65 + (9.6 * Weight) + (1.8 * Height) - (4.7 * Age);
            }
            
            label13.Text = BMR.ToString("N0");
            double Sedentary = BMR * 1.2;
            double Lightly = BMR * 1.375;
            double Moderately = BMR * 1.55;
            double VeryActive = BMR * 1.725;
            double Extremely = BMR * 1.9;

            label16.Text = Sedentary.ToString("N0");
            label18.Text = Lightly.ToString("N0");
            label20.Text = Moderately.ToString("N0");
            label22.Text = VeryActive.ToString("N0");
            label24.Text = Extremely.ToString("N0");
        }

        string Gender = "Male";
        private void Male_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.LightGray;
            panel2.BackColor = SystemColors.Control;
            Gender = "Male";
        }

        private void FemaleClick(object sender, EventArgs e)
        {
            panel2.BackColor = Color.LightGray;
            panel1.BackColor = SystemColors.Control;
            Gender = "Female";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }

        private void label25_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parent.btnBack.PerformClick();
        }
    }
}
