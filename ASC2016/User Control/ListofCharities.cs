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
    public partial class ListofCharities : UserControl
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        public ListofCharities()
        {
            InitializeComponent();
        }

        private void ListofCharities_Load(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            var q = db.Charities;
            int y = 12;
            foreach (var a in q)
            {
                Panel pnl = new Panel();
                Label lblName = new Label();
                Label lblDescription = new Label();
                PictureBox pb = new PictureBox();
                // 
                // panel2
                // 
                pnl.Location = new System.Drawing.Point(12, y);
                pnl.Name = "panel2";
                pnl.Size = new System.Drawing.Size(746, 93);
                pnl.TabIndex = 19;
                // 
                // pictureBox1
                // 
                pb.Location = new System.Drawing.Point(3, 3);
                pb.Name = "pictureBox1";
                pb.Size = new System.Drawing.Size(115, 87);
                pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                pb.TabIndex = 0;
                pb.TabStop = false;
                pb.Image = Image.FromFile(Application.StartupPath + "\\marathon-skills-2015-charity-data\\" + a.CharityLogo);
                // 
                // label3
                // 
                lblName.AutoSize = true;
                lblName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lblName.Location = new System.Drawing.Point(124, 10);
                lblName.Name = "label3";
                lblName.Size = new System.Drawing.Size(51, 20);
                lblName.TabIndex = 1;
                lblName.Text = a.CharityName;
                // 
                // label4
                // 
                lblDescription.Location = new System.Drawing.Point(128, 30);
                lblDescription.Name = "label4";
                lblDescription.Size = new System.Drawing.Size(615, 60);
                lblDescription.TabIndex = 2;
                lblDescription.Text = a.CharityDescription;

                pnl.Controls.Add(pb);
                pnl.Controls.Add(lblName);
                pnl.Controls.Add(lblDescription);

                panel1.Controls.Add(pnl);
                y += 100;
            }
        }
    }
}
