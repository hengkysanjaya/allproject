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
    public partial class ImportVolunteers : UserControl
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        Form1 parent;
        public ImportVolunteers()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string[] allLines = File.ReadAllLines(textBox1.Text);

                for (int i = 1; i < allLines.Length; i++)
                {
                    try
                    {
                        string[] lines = allLines[i].Split(',');
                        if (lines.Length != 5 || lines[0] + "" == "" || lines[1] + "" == "" || lines[2] + "" == "" || lines[3] + "" == "" || lines[4] + "" == "")
                        {
                            MessageBox.Show("CSV file doest not match required format");
                            return;
                        }

                        string id = lines[0];
                        string firstname = lines[1];
                        string lastname = lines[2];
                        string CountryCode = lines[3];
                        string Gender = lines[4].ToLower() == "m" ? "Male" : "Female";

                        var q = db.Volunteers.Where(x => x.VolunteerId.Equals(id)).FirstOrDefault();
                        if (q == null)
                        {
                            Volunteer v = new Volunteer();
                            v.FirstName = firstname;
                            v.LastName = lastname;
                            v.CountryCode = CountryCode;
                            v.Gender = Gender;
                            db.Volunteers.InsertOnSubmit(v);
                        }
                        else
                        {
                            q.FirstName = firstname;
                            q.LastName = lastname;
                            q.CountryCode = CountryCode;
                            q.Gender = Gender;
                        }
                        db.SubmitChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                MessageBox.Show("Import Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog op = new OpenFileDialog())
            {
                op.Filter = "CSV Files|*.csv";
                if(op.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = op.FileName;
                }
            }
        }

        private void ImportVolunteers_Load(object sender, EventArgs e)
        {
            parent = (Form1)this.ParentForm;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            parent.btnBack.PerformClick();
        }
    }
}
