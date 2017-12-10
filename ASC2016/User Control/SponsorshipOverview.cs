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
    public partial class SponsorshipOverview : UserControl
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        public SponsorshipOverview()
        {
            InitializeComponent();
        }

        private void SponsorshipOverview_Load(object sender, EventArgs e)
        {
            comboSort.Items.Add("Charity Name");
            comboSort.Items.Add("Total Amount");
            comboSort.SelectedIndex = 0;
            button1.PerformClick();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            dataGridView1.RowTemplate.Height = 35;
            DataGridViewImageColumn imgColumn = new DataGridViewImageColumn();
            imgColumn.HeaderText = "Logo";
            imgColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.Columns.Add(imgColumn);

            dataGridView1.Columns.Add("Charity Name", "Charity Name");
            dataGridView1.Columns.Add("Total Amount", "Total Amount");

            var q = db.Sponsorships.GroupBy(x => x.Registration.CharityId).Select(x => new
            {
                CharityName = x.Select(y => y.Registration.Charity.CharityName).FirstOrDefault(),
                CharityId = x.Key,
                TotalAmount = x.Sum(y => y.Amount)
            });
            if (comboSort.Text == "Charity Name")
            {
                q = q.OrderBy(x => x.CharityName);
            }
            else
            {
                q = q.OrderBy(x => x.TotalAmount);
            }

            foreach (var a in q)
            {
                var charity = db.Charities.Where(x => x.CharityId.Equals(a.CharityId)).FirstOrDefault();
                Image img = Image.FromFile(Application.StartupPath + "\\marathon-skills-2015-charity-data\\" + charity.CharityLogo);
                dataGridView1.Rows.Add(img, charity.CharityName, a.TotalAmount.ToString("C"));
            }
            label4.Text = q.Count().ToString();
            label6.Text = q.Sum(x => x.TotalAmount).ToString("C");

        }
    }
}
