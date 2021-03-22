using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarkOil
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add("66", "0");
            dataGridView1.Rows.Add("80", "3");
            dataGridView1.Rows.Add("100", "7");
            dataGridView1.Rows.Add("120", "10");
            dataGridView1.Rows.Add("140", "14");
            dataGridView1.Rows.Add("160", "19");
            dataGridView1.Rows.Add("180", "22");
            dataGridView1.Rows.Add("200", "26");
            dataGridView1.Rows.Add("220", "29");
            dataGridView1.Rows.Add("240", "33");
            dataGridView1.Rows.Add("260", "37");
            dataGridView1.Rows.Add("280", "40");
            dataGridView1.Rows.Add("300", "44");
            dataGridView1.Rows.Add("320", "48");
            dataGridView1.Rows.Add("340", "54");
            dataGridView1.Rows.Add("350", "56");
            dataGridView1.Rows.Add("360", "59");

            dataGridView2.Rows.Add("380", "0");
            dataGridView2.Rows.Add("390", "4");
            dataGridView2.Rows.Add("400", "10");
            dataGridView2.Rows.Add("410", "16");
            dataGridView2.Rows.Add("420", "20");
            dataGridView2.Rows.Add("430", "25");
            dataGridView2.Rows.Add("440", "29");
            dataGridView2.Rows.Add("450", "33");
            dataGridView2.Rows.Add("460", "36");
            dataGridView2.Rows.Add("470", "40");
            dataGridView2.Rows.Add("480", "44");
            dataGridView2.Rows.Add("490", "46");
            dataGridView2.Rows.Add("500", "48");
            dataGridView2.Rows.Add("510", "50");
            dataGridView2.Rows.Add("520", "52");
            dataGridView2.Rows.Add("530", "55");
            dataGridView2.Rows.Add("540", "57");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double x = 0;
            double y = 0;
            chart1.Series[0].Points.Clear();
            for(int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                x = Convert.ToDouble(dataGridView1[1, i].Value);
                y = Convert.ToDouble(dataGridView1[0, i].Value);
                chart1.Series[0].Points.AddXY(x, y);
            }
            double buffer = x;
            for (int i = 0; i < dataGridView2.Rows.Count-1; i++)
            {
                x = (100-buffer)*(0.01)*Convert.ToDouble(dataGridView2[1, i].Value) + buffer;
                y = Convert.ToDouble(dataGridView2[0, i].Value) ;
                chart1.Series[0].Points.AddXY(x, y);
            }
        }
    }
}
