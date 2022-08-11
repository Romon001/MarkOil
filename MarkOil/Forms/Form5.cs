using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarkOil.Forms
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
        }
        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = (e.Row.Index + 1).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double sulfur = 0;
            double yield = 0;
            double parafins=0;
            double viscosity=0;

            dataGridView2.Rows.Clear();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                sulfur += Convert.ToDouble(row.Cells["Сера"].Value);
                yield += Convert.ToDouble(row.Cells["Выход"].Value);
                parafins += Convert.ToDouble(row.Cells["Парафины"].Value);
                viscosity += Convert.ToDouble(row.Cells["Вязкость"].Value);
            }
            sulfur /= dataGridView1.Rows.Count-1;
            yield /= dataGridView1.Rows.Count-1;
            parafins /= dataGridView1.Rows.Count-1;
            viscosity /= dataGridView1.Rows.Count-1;
            dataGridView2.Rows.Add(sulfur, yield, parafins, viscosity);
        }
    }
}
