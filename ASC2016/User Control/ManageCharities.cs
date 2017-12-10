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
    public partial class ManageCharities : UserControl
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        Form1 parent;
        public ManageCharities()
        {
            InitializeComponent();
        }

        private void ManageCharities_Load(object sender, EventArgs e)
        {
            parent = (Form1)this.ParentForm;

            var q = db.Charities;

            dataGridView1.RowTemplate.Height = 35;

            DataGridViewImageColumn imgColumn = new DataGridViewImageColumn();
            imgColumn.HeaderText = "Logo";
            imgColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.Columns.Add(imgColumn);
            
            dataGridView1.Columns.Add("CharityId", "CharityId");
            dataGridView1.Columns.Add("Name", "Name");
            dataGridView1.Columns.Add("Description", "Description");
            
            DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
            btnColumn.HeaderText = "Edit";
            btnColumn.UseColumnTextForButtonValue = true;
            btnColumn.Text = "Edit";
            dataGridView1.Columns.Add(btnColumn);

            dataGridView1.Columns[1].Visible = false;

            foreach(var a in q)
            {
                try
                {
                    using (FileStream fs = new FileStream(Application.StartupPath + "\\marathon-skills-2015-charity-data\\" + a.CharityLogo, FileMode.Open))
                    {
                        Image img = Image.FromStream(fs);
                        dataGridView1.Rows.Add(img, a.CharityId, a.CharityName, a.CharityDescription);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            core.CharityId = "";
            parent.LoadForm("ADDEDITCHARITY");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.Columns[e.ColumnIndex]. HeaderText == "Edit")
            {
                core.CharityId = dataGridView1.CurrentRow.Cells["CharityId"].Value.ToString();
                parent.LoadForm("ADDEDITCHARITY");
            }
        }
    }
}
