using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MathNet.Numerics.LinearAlgebra.Double;

namespace MarkOil
{
    public partial class Form3 : Form
    {
        public List<int> constantSplit = new List<int> { 28, 62, 85, 100, 120, 150, 180,
                                                        200, 220, 240, 280, 300, 320, 340,
                                                        350, 360, 380, 400, 420, 450, 500 };
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
            //dataGridView1.Rows.Add("80", "3");
            //dataGridView1.Rows.Add("100", "7");
            //dataGridView1.Rows.Add("120", "10");
            dataGridView1.Rows.Add("140", "14");
            //dataGridView1.Rows.Add("160", "19");
            //dataGridView1.Rows.Add("180", "22");
            //dataGridView1.Rows.Add("200", "26");
            dataGridView1.Rows.Add("220", "29");
            //dataGridView1.Rows.Add("240", "33");
            //dataGridView1.Rows.Add("260", "37");
            //dataGridView1.Rows.Add("280", "40");
            //dataGridView1.Rows.Add("300", "44");
            //dataGridView1.Rows.Add("320", "48");
            //dataGridView1.Rows.Add("340", "54");
            //dataGridView1.Rows.Add("350", "56");
            dataGridView1.Rows.Add("360", "59");

            //dataGridView2.Rows.Add("380", "0");
            dataGridView2.Rows.Add("390", "10");
            //dataGridView2.Rows.Add("400", "10");
            //dataGridView2.Rows.Add("410", "16");
            dataGridView2.Rows.Add("420", "20");
            //dataGridView2.Rows.Add("430", "25");
            //dataGridView2.Rows.Add("440", "29");
            //dataGridView2.Rows.Add("450", "33");
            //dataGridView2.Rows.Add("460", "36");
            //dataGridView2.Rows.Add("470", "40");
            dataGridView2.Rows.Add("480", "44");
            //dataGridView2.Rows.Add("490", "46");
            //dataGridView2.Rows.Add("500", "48");
            //dataGridView2.Rows.Add("510", "50");
            //dataGridView2.Rows.Add("520", "52");
            //dataGridView2.Rows.Add("530", "55");
            dataGridView2.Rows.Add("540", "57");
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = 100;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            double x = 0;
            double y = 0;
            chart1.Series[0].Points.Clear();
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                x = Convert.ToDouble(dataGridView1[1, i].Value);
                y = Convert.ToDouble(dataGridView1[0, i].Value);
                chart1.Series[0].Points.AddXY(x, y);
                


            }
            chart1.Series[0].Sort(PointSortOrder.Ascending);
            chart1.Series[1].Sort(PointSortOrder.Ascending);

            double buffer = chart1.Series[0]
                                  .Points[chart1.Series[0].Points.Count - 1]
                                  .XValue;
            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                x = (100 - buffer) * (0.01) * Convert.ToDouble(dataGridView2[1, i].Value) + buffer;
                y = Convert.ToDouble(dataGridView2[0, i].Value);
                chart1.Series[0].Points.AddXY(x, y);
            }

            chart1.Series[0].Sort(PointSortOrder.Ascending);
            chart1.Series[1].Sort(PointSortOrder.Ascending);

            label3.Text = CheckMonotone() ? "Монотонна" : "Не монотонна";
            
        }
        private bool CheckMonotone() 
        {
            bool monotone = true;
            for (int i = 0; i < chart1.Series[0].Points.Count - 2; i++)
            {
                if (chart1.Series[0].Points[i].XValue > chart1.Series[0].Points[i + 1].XValue)
                {
                    monotone = false;
                }
            }
                return monotone;
        }

        private void SplitingCurve()
        {
            
            DataPointCollection points = chart1.Series[0].Points;
            int n = points.Count;
            //Матрица коэффициентов c_i сплайнов. Коэффициент a равен f[x], b,d выражаюстя через c (см.Вики Кубический_сплайн)
            SparseMatrix matrix = new SparseMatrix(n);

            double[] rightSide = new double[n];
            //Заполнение матрицы и вектора правой части
            for(int i = 0; i < n ; i++)
            {
                if ((i == 0)||(i == n - 1))
                {
                    matrix[i, i] = 1;
                    rightSide[i] = 0;
                }
                else
                {
                    matrix[i, i - 1] = points[i].XValue - points[i - 1].XValue;
                    matrix[i, i] = 2*(points[i + 1].XValue - points[i-1].XValue);
                    matrix[i, i + 1] = points[i + 1].XValue - points[i].XValue;
                    rightSide[i] = 3*((points[i+1].YValues.First()- points[i].YValues.First())/(points[i + 1].XValue - points[i].XValue)
                                       - (points[i].YValues.First() - points[i-1].YValues.First())/(points[i].XValue - points[i-1].XValue));
                }
            }    
            var coefC = matrix.Solve(DenseVector.Build.DenseOfArray(rightSide)).ToArray();

            double[] coefA = new double[n];
            double[] coefB = new double[n];
            double[] coefD = new double[n];
            //Нулевые коэффициенты нам не нужны, заполнили нулями
            (coefB[0], coefD[0]) = (0, 0);
            coefA[0] = points[0].YValues.First();
            for (int i = 1; i < n; i++)
            {
                coefA[i] = points[i].YValues.First();
                coefB[i] = (coefA[i]-coefA[i-1])/(points[i].XValue - points[i - 1].XValue) 
                           +(2*coefC[i]+ coefC[i-1])*(points[i].XValue - points[i - 1].XValue)/3 ;
                coefD[i] = (coefC[i]-coefC[i-1])/(points[i].XValue - points[i - 1].XValue) /3;
            }

            //Разбивка значений кривой по 10
            double iterator = 0;
            while (iterator < points[0].XValue)
            {
                iterator += 10;
            }

            DataTable splittingValues = new DataTable();
            splittingValues.Columns.Add("Выход, %", System.Type.GetType("System.Double"));
            splittingValues.Columns.Add("Температура, С", System.Type.GetType("System.Double"));
            for (int i = 0; i < constantSplit.Count-1; i++)
            {
                while (iterator < points[i].XValue )
                {
                    double value = coefD[i] * Math.Pow(iterator - points[i].XValue, 3)
                                + coefC[i] * Math.Pow(iterator - points[i].XValue, 2)
                                + coefB[i] * (iterator - points[i].XValue)
                                + coefA[i];
                    splittingValues.Rows.Add(iterator, value);
                    iterator += 10;
                }
            }
            chart1.Series[2].Points.Clear();

            chart1.Series[1].Points.Clear();
            foreach (DataRow row in splittingValues.Rows)
            {
                chart1.Series[2].Points.AddXY(row[0], row[1]);
                chart1.Series[1].Points.AddXY(row[0], row[1]);
            }

            dataGridView3.DataSource = splittingValues;
            dataGridView3.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (CheckMonotone())
            {
                SplitingCurve();
            }
            else 
            {
                    chart1.Series[1].Points.Clear();
                chart1.Series[2].Points.Clear();
            }

        }
    }
}
