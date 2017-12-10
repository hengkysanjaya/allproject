using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ASC2016.User_Control
{
    public partial class AddEditCharity : UserControl
    {
        Form1 parent;
        DataClasses1DataContext db = new DataClasses1DataContext();
        public AddEditCharity()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parent.btnBack.PerformClick();
        }

        Charity currentCharity;
        private void AddEditCharity_Load(object sender, EventArgs e)
        {
            parent = (Form1)this.ParentForm;

            if (core.CharityId != "")
            {
                currentCharity = db.Charities.Where(x => x.CharityId.Equals(core.CharityId)).FirstOrDefault();
                textBox1.Text = currentCharity.CharityName;
                richTextBox1.Text = currentCharity.CharityDescription;

                string strPath = Application.StartupPath + "\\marathon-skills-2015-charity-data\\" + currentCharity.CharityLogo;
                //using (FileStream fs = new FileStream(strPath, FileMode.Open))
                //{
                //    pictureBox1.Image = Image.FromStream(fs);
                //}
                

                //pictureBox1.Image = Image.FromFile(Path);
                textBox2.Text = strPath;
                //pictureBox1.ImageLocation = textBox2.Text;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!parent.ValidationNotNull(this))
                {
                    MessageBox.Show("All data must be filled");
                    return;
                }
                string ext = Path.GetExtension(textBox2.Text);
                if (core.CharityId != "")
                {
                    currentCharity.CharityName = textBox1.Text;
                    currentCharity.CharityDescription = richTextBox1.Text;
                    
                    currentCharity.CharityLogo = currentCharity.CharityId + ext;
                    
                    File.Copy(textBox2.Text, Application.StartupPath + "\\marathon-skills-2015-charity-data\\" + currentCharity.CharityId + ext, true);
                }
                else
                {
                    Charity c = new Charity();
                    c.CharityName = textBox1.Text;
                    c.CharityDescription = richTextBox1.Text;
                    c.CharityLogo = currentCharity.CharityId + ext;
                    db.Charities.InsertOnSubmit(c);
                    File.Copy(textBox2.Text, Application.StartupPath + "\\marathon-skills-2015-charity-data\\" + c.CharityId +ext, true);
                }

                db.SubmitChanges();
                core.CharityId = "";
                parent.btnBack.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog op = new OpenFileDialog())
            {
                op.Filter = "Image files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                if(op.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        textBox2.Text = op.FileName;
                        /*using (FileStream fs = new FileStream(op.FileName, FileMode.Open))
                        {
                            pictureBox1.Image = Image.FromStream(fs);
                        }*/
                        pictureBox1.ImageLocation = textBox2.Text;
                    }
                    catch (Exception ex) 
                    {
                        MessageBox.Show(ex.Message);
                    }
                    

                }
            }
        }
    }
}
