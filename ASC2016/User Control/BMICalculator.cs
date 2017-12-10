using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ASC2016.Properties;

namespace ASC2016.User_Control
{
    public partial class BMICalculator : UserControl
    {
        Form1 parent;
        public BMICalculator()
        {
            InitializeComponent();
        }

        string Gender = "Male";
        private void Male_CLick(object sender, EventArgs e)
        {
            panel1.BackColor = Color.LightGray;
            panel2.BackColor = SystemColors.Control;
            Gender = "Male";
        }

        private void Female_CLick(object sender, EventArgs e)
        {
            panel2.BackColor = Color.LightGray;
            panel1.BackColor = SystemColors.Control;
            Gender = "Female";
        }

        private void KeyPress(object sender, KeyPressEventArgs e)
        {
            parent.NumberOnly(e);   
        }

        private void BMICalculator_Load(object sender, EventArgs e)
        {
            parent = (Form1)this.ParentForm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parent.btnBack.PerformClick();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double weight = double.Parse(textBox2.Text);
            double height = double.Parse(textBox1.Text) / 100;
            double BMI = weight / (height * height);

            string status = "";
            if (BMI < 18.5)
            {
                panel8.Left = panel4.Left + (int)((panel4.Width * BMI) / 18.5) - 22;
                status = "Underweight";
                pictureBox3.Image = Resources.bmi_underweight_icon;
            }
            else if (BMI >= 18.5 && BMI < 25)
            {
                panel8.Left = panel5.Left + (int)((panel5.Width * (BMI - 18.5)) / (25 - 18.5)) - 22;
                status = "Healthy";
                pictureBox3.Image = Resources.bmi_healthy_icon;
            }
            else if (BMI >= 25 && BMI < 30)
            {
                panel8.Left = panel7.Left + (int)((panel7.Width * (BMI - 25)) / (30 - 25)) - 22;
                status = "Overweight";
                pictureBox3.Image = Resources.bmi_overweight_icon;
            }
            else if (BMI >= 30)
            {
                panel8.Left = panel6.Left + (int)((panel6.Width * (60 - (BMI - 60)) / 60)) - 22;
                status = "Obese";
                pictureBox3.Image = Resources.bmi_obese_icon;
            }
            label9.Text = status;
            label10.Text = BMI.ToString("N2");
        }
    }
}
