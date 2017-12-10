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
    public partial class InteractiveMap : UserControl
    {
        Form1 parent;
        public InteractiveMap()
        {
            InitializeComponent();
        }

        private void InteractiveMap_Load(object sender, EventArgs e)
        {
            parent = (Form1)this.ParentForm;
        }
        List<string> data = new List<string>()
        {
            "Checkpoint 1,Avenida Rudge,Yes,Yes,No,No,No",
            "Checkpoint 2,Theatro Municipal,Yes,Yes,Yes,Yes,Yes",
            "Checkpoint 3,Parque do Ibirapuera,Yes,Yes,Yes,No,No",
            "Checkpoint 4,Jardim Luzitania,Yes,Yes,Yes,No,Yes",
            "Checkpoint 5,Iguatemi,Yes,Yes,Yes,Yes,No",
            "Checkpoint 6,Rua Lisboa,Yes,Yes,Yes,No,No",
            "Checkpoint 7,Cemitério da Consolação,Yes,Yes,Yes,Yes,Yes",
            "Checkpoint 8,Cemitério da Consolação,Yes,Yes,Yes,Yes,Yes",
            "Start of the 42km Full Marathon,Race Start,no,no,no,no,no",
            "Start of the 21km Half Marathon,Race Start,no,no,no,no,no",
            "Start of the 5km Fun Run,Race Start,no,no,no,no,no",
        };

        private void Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Controls.Clear();
            var obj = sender as Control;
            string Index = obj.Tag + "";

            var q = data[int.Parse(Index)].Split(',');
            label3.Text = q[1];
            label15.Text = q[0];
            if (int.Parse(Index) > 7)
            {
                label1.Visible = false;
                label2.Visible = false;
            }
            else
            {
                label1.Visible = true;
                label2.Visible = true;
            }

            int y = 12;
            for (int i = 2; i < q.Length; i++)
            {
                if(q[i] == "Yes")
                {
                    PictureBox pb = new PictureBox();
                    Label lbl = new Label();
                    pb.Location = new System.Drawing.Point(12, y);
                    pb.Name = "pictureBox5";
                    pb.Size = new System.Drawing.Size(79, 53);
                    pb.TabIndex = 0;
                    pb.TabStop = false;
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;

                    lbl.Location = new System.Drawing.Point(107, y);
                    lbl.Name = "label4";
                    lbl.Size = new System.Drawing.Size(172, 53);
                    lbl.TabIndex = 1;
                    lbl.Text = "label4";
                    lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

                    if (i == 2)
                    {  
                        pb.Image = (Bitmap)Resources.map_icon_drinks;
                        lbl.Text = "Drinks";
                    }
                    else if (i == 3)
                    {
                        pb.Image = Resources.map_icon_energy_bars;
                        lbl.Text = "Energy";
                    }
                    else if (i == 4)
                    {
                        pb.Image = Resources.map_icon_toilets;
                        lbl.Text = "Toilets";
                    }
                    else if (i == 5)
                    {
                        pb.Image = Resources.map_icon_information;
                        lbl.Text = "Information";
                    }
                    else if (i == 6)
                    {
                        pb.Image = Resources.map_icon_medical;
                        lbl.Text = "Medical";
                    }
                    y += 60;
                    
                    panel2.Controls.Add(pb);
                    panel2.Controls.Add(lbl);
                }
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }
    }
}
