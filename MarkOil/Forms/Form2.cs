using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.SolverFoundation.Common;
using Microsoft.SolverFoundation.Solvers;
using Microsoft.SolverFoundation.Services;

namespace MarkOil
{
    public partial class Form2 : Form
    {
        dbMarkEntities _context;
        public Form2()
        {
            InitializeComponent();
            _context = new dbMarkEntities();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var sul = Convert.ToDouble(textBoxSUL.Text);
            var _350 = Convert.ToDouble(textBox350.Text);
            var par = Convert.ToDouble(textBoxPAR.Text);
            var spg = Convert.ToDouble(textBoxSPG.Text);
            var cst = Convert.ToDouble(textBoxCST.Text);
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Add("Название","Название нефти");
            dataGridView1.Columns[0].Width = 627;
            _context = new dbMarkEntities();
            label7.Text = "Нефти-аналоги по индексу";
            DataTable rowsTable = new DataTable();

            int targetIndex = SetIndex(sul, _350, par);  

            var indexLibraryOils = _context.URALS.Where(p => p.LIBRARY_ID == 163);
            //var c = _context.CUT_URALS.Where(p => p.LIBRARY_ID == 163);
            //var a = _context.ANLGX;
            var query = from i in indexLibraryOils
                        join c in _context.CUT_URALS.Where(p => p.TTL == "НЕФТЬ В ЦЕЛОМ")
                            on i.NEFT_ID equals c.NEFT_ID 
                        select new { cutId = c.CUT_ID, oilName = i.TTL};
            var query2 = from q in query
                        join c in _context.CUT_PROP.Where(p => p.PROP_ID == 574)
                            on q.cutId equals c.CUT_ID
                        select new { index = c.XVALUE, oilName = q.oilName };
            var query3 = query2.Where(p => p.index == targetIndex);

            foreach (var b in query3)
            {
                dataGridView1.Rows.Add(b.oilName);
            }
        }
        private int SetIndex(double sul, double _350, double par)
        {
            int indexSul = sul <= 0.5 ? 1 : (sul <= 2 ? 2 : 3);
            int index350 = _350 > 54.9 ? 1 : (_350 >= 45 ? 2 : 3);
            int indexSPG = 0;
            int indexCST = 0;
            int indexPAR = par <= 1.5 ? 1 : (sul <= 6 ? 2 : 3);
            return 10000*indexSul + 1000*index350 + 100*indexSPG + 10*indexCST + indexPAR ;
        }

        private void closenessSelectionButton_Click(object sender, EventArgs e)
        {
            var sul = Convert.ToDouble(textBoxSUL.Text);
            var _350 = Convert.ToDouble(textBox350.Text);
            var par = Convert.ToDouble(textBoxPAR.Text);
            var spg = Convert.ToDouble(textBoxSPG.Text);
            var cst = Convert.ToDouble(textBoxCST.Text);

            label7.Text = "Нефти аналоги по близости";

            var coefSul = Convert.ToDouble(textBox11.Text);
            var coef350 = Convert.ToDouble(textBox7.Text);
            var coefPar = Convert.ToDouble(textBox8.Text);
            var ceofSpg = Convert.ToDouble(textBox9.Text);
            var coefCst = Convert.ToDouble(textBox10.Text);

            var coefSum = coefSul + coef350 + coefPar + ceofSpg + coefCst;
            coefSul /= coefSum; 
            coef350 /= coefSum;
            coefPar /= coefSum;
            ceofSpg /= coefSum;
            coefCst /= coefSum;

            var answer = MessageBox.Show("Решить с помощью линейного программирования?",
                                         "Сообщение",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Information,
                                         MessageBoxDefaultButton.Button1,
                                         MessageBoxOptions.DefaultDesktopOnly);
            
            CreateClosenessSelectionTable();

            DataTable propertiesTable = GetFullPropertiesTable();
            
            
            int numberOfAnalogs = Convert.ToInt32(textBox16.Text);
            List<DataRow> analogs = new List<DataRow>(numberOfAnalogs);
            List<double> analogsDistance = new List<double>(numberOfAnalogs);
            if (!propertiesTable.Columns.Contains("Расстояние"))
            {
                propertiesTable.Columns.Add("Расстояние", System.Type.GetType("System.Double"));
                propertiesTable.Columns.Add("Рецепт", System.Type.GetType("System.Double"));

            }
            int count = 0;
            int indexOfDistance;
            foreach (DataRow b in propertiesTable.Rows)
            {
                count++;
                b["Расстояние"] = CalculateDistance(b);

                double distance = Convert.ToDouble(b["Расстояние"]);
                b["Рецепт"] = distance;
                if (count <= numberOfAnalogs)
                {
                    analogs.Add(b);
                    analogsDistance.Add(distance);
                }
                else
                {
                    if (distance < analogsDistance.Max())
                    {
                        indexOfDistance = analogsDistance.IndexOf(analogsDistance.Max());
                        analogs[indexOfDistance] = b;
                        analogsDistance[indexOfDistance] = distance;
                    }
                }


            }

            DataTable closenessAnalogs = new DataTable();
            closenessAnalogs.Columns.Add("Название", System.Type.GetType("System.String"));
            closenessAnalogs.Columns.Add("Сера", System.Type.GetType("System.Double"));
            closenessAnalogs.Columns.Add("V350", System.Type.GetType("System.Double"));
            closenessAnalogs.Columns.Add("Парафины", System.Type.GetType("System.Double"));
            closenessAnalogs.Columns.Add("Плотность", System.Type.GetType("System.Double"));
            closenessAnalogs.Columns.Add("Вязкость");
            closenessAnalogs.Columns.Add("Расстояние", System.Type.GetType("System.Double"));
            closenessAnalogs.Columns.Add("Рецепт", System.Type.GetType("System.Double"));

            
            DataRow finalAnalog = closenessAnalogs.NewRow();
            finalAnalog["Название"] = "Смесь нефтей";
            finalAnalog["Сера"] = 0;
            finalAnalog["V350"] = 0;
            finalAnalog["Парафины"] = 0;
            finalAnalog["Плотность"] = 0;
            finalAnalog["Вязкость"] = 0;
            finalAnalog["Расстояние"] = 0;
            finalAnalog["Рецепт"] = 0;

            //Рассчет рецепта
            if (answer.ToString() == "Yes")
            {
                CalculateRecipeLP(analogs, finalAnalog, closenessAnalogs);
            }
            else
            {
                CalculateRecipe(analogs, finalAnalog, closenessAnalogs);

            }
            //Добавление конечной смеси в список
            closenessAnalogs.Rows.Add(finalAnalog);
            closenessAnalogs.Rows.Add("Исходная нефть",sul,_350,par, spg, cst, "0,0000","1,000");
            dataGridView1.DataSource = closenessAnalogs;
            dataGridView1.Refresh();

            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[1].Width = 50;
            dataGridView1.Columns[2].Width = 50;
            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[4].Width = 80;
            dataGridView1.Columns[5].Width = 70;
            dataGridView1.Columns[6].Width = 75;
            dataGridView1.Columns[7].Width = 60;

            dataGridView1.Refresh();


        }
        private void CreateClosenessSelectionTable()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.ColumnHeadersVisible = true;
        }
        private DataTable GetFullPropertiesTable()
        {
            if (!(fullPropertiesTable  is null)) 
            {
                return fullPropertiesTable;
            }
            DataTable propTable = new DataTable();
            propTable.Columns.Add("Название", System.Type.GetType("System.String"));
            propTable.Columns.Add("Сера", System.Type.GetType("System.Double"));
            propTable.Columns.Add("V350", System.Type.GetType("System.Double"));
            propTable.Columns.Add("Парафины", System.Type.GetType("System.Double"));
            propTable.Columns.Add("Плотность", System.Type.GetType("System.Double"));
            propTable.Columns.Add("Вязкость");
            var indexLibraryOils = _context.URALS.Where(p => p.LIBRARY_ID == 163);
            foreach(var oil in indexLibraryOils)
            {
                var fractions = _context.CUT_URALS.Where(p => p.NEFT_ID == oil.NEFT_ID);
                var oilCutId = fractions.Where(p => p.YLD == 100).Select(p=>p.CUT_ID).FirstOrDefault();
                var oilProperties = _context.CUT_PROP.Where(p => p.CUT_ID == oilCutId);

                DataRow workRow = propTable.NewRow();

                workRow["Название"] = oil.TTL;
                workRow["Сера"] = oilProperties.Where(p => p.PROP_ID == 392 )
                                                                    .Select(p =>p.XVALUE).FirstOrDefault();
                workRow["Парафины"] = oilProperties.Where(p => p.PROP_ID == 404)
                                                                    .Select(p => p.XVALUE).FirstOrDefault();
                workRow["Плотность"] = oilProperties.Where(p => p.PROP_ID == 380)
                                                                    .Select(p => p.XVALUE).FirstOrDefault();
                workRow["Вязкость"] = oilProperties.Where(p => p.PROP_ID ==384 || p.PROP_ID == 387)
                                                                    .Select(p => p.XVALUE).FirstOrDefault();

                if (workRow["Вязкость"].ToString() =="")
                {
                    workRow["Вязкость"] = 0;
                }

                var frac350CutId = fractions.Where(p => p.TTL == "320...350")
                                                                    .Select(p => p.CUT_ID).FirstOrDefault();
                workRow["V350"] = fractions.Where(p => p.CUT_ID> oilCutId && p.CUT_ID <= frac350CutId)
                                                                    .Sum(p => p.YLD);
                propTable.Rows.Add(workRow);

            }
            fullPropertiesTable = propTable;
            return fullPropertiesTable;
        }
        public void CalculateRecipe(List<DataRow> analogs, DataRow finalAnalog, DataTable closenessAnalogs)
        {
                        double reverseDistancesSum = 0;
            foreach (var a in analogs)
            {
                reverseDistancesSum += 1 / Convert.ToDouble(a["Расстояние"]);

            }
            foreach (var a in analogs)
            {
                a["Рецепт"] = (1 / Convert.ToDouble(a["Расстояние"]) / reverseDistancesSum);
                finalAnalog["Сера"] = Convert.ToDouble(finalAnalog["Сера"]) + Convert.ToDouble(a["Рецепт"]) * Convert.ToDouble(a["Сера"]);
                finalAnalog["V350"] = Convert.ToDouble(finalAnalog["V350"]) + Convert.ToDouble(a["Рецепт"]) * Convert.ToDouble(a["V350"]);
                finalAnalog["Парафины"] = Convert.ToDouble(finalAnalog["Парафины"]) + Convert.ToDouble(a["Рецепт"]) * Convert.ToDouble(a["Парафины"]);
                finalAnalog["Плотность"] = Convert.ToDouble(finalAnalog["Плотность"]) + Convert.ToDouble(a["Рецепт"]) * Convert.ToDouble(a["Плотность"]);
                finalAnalog["Вязкость"] = Convert.ToDouble(finalAnalog["Вязкость"]) + Convert.ToDouble(a["Рецепт"]) * Convert.ToDouble(a["Вязкость"]);
                finalAnalog["Расстояние"] =CalculateDistance(finalAnalog);
                finalAnalog["Рецепт"] = Convert.ToDouble(finalAnalog["Рецепт"]) + Convert.ToDouble(a["Рецепт"]);

                closenessAnalogs.ImportRow(a);
                
            }
        }
        public void CalculateRecipeLP(List<DataRow> analogs, DataRow finalAnalog, DataTable closenessAnalogs)
        {
            var solver = SolverContext.GetContext();
            solver.ClearModel();
            var model = solver.CreateModel();
            Term generalError = 0; //функция которую минимизируем 
            Term errorSul = Convert.ToDouble(textBoxSUL.Text);
            Term errorV350 = Convert.ToDouble(textBox350.Text);
            Term errorPar = Convert.ToDouble(textBoxPAR.Text);
            Term errorSPG = Convert.ToDouble(textBoxSPG.Text);
            Term errorCST = Convert.ToDouble(textBoxCST.Text);
            Term generalConstraint = 0;// сумма рецепта = 1
            for (int i = 0; i < Convert.ToInt32(textBox16.Text); i++)
            {
                Decision alpha = new Decision(Domain.RealRange(0,1), $"alpha{i}") ;
                model.AddDecision(alpha);
                model.AddConstraint($"constraintAlpha{i}", 0 <= alpha <= 1);

                errorSul -= alpha * Convert.ToDouble(analogs[i]["Сера"]);
                errorV350 -= alpha * Convert.ToDouble(analogs[i]["V350"]);
                errorPar -= alpha * Convert.ToDouble(analogs[i]["Парафины"]);
                errorSPG -= alpha * Convert.ToDouble(analogs[i]["Плотность"]);
                errorCST -= alpha * Convert.ToDouble(analogs[i]["Вязкость"]);
                
                generalConstraint += alpha;
                
            }

            model.AddConstraint($"sumAlpha", generalConstraint==1);

            generalError = Convert.ToDouble(textBox11.Text)*Model.Abs(errorSul) +
                            Convert.ToDouble(textBox7.Text)*Model.Abs(errorV350) +
                            Convert.ToDouble(textBox8.Text)*Model.Abs(errorPar) + 
                            Convert.ToDouble(textBox9.Text)*Model.Abs(errorSPG) +
                            Convert.ToDouble(textBox10.Text)*Model.Abs(errorCST);
            model.AddGoal("ComplicatedGoal", GoalKind.Minimize, generalError);

            var solution = solver.Solve();
                
            var solutions = solution.Decisions.ToList();
            for (int i=0;i< solutions.Count();i++)
            {
                analogs[i]["Рецепт"] = solutions[i].ToDouble();
                var a = solutions[i].ToDouble();
            }
            foreach (var a in analogs)
            {
                finalAnalog["Сера"] = Convert.ToDouble(finalAnalog["Сера"]) + Convert.ToDouble(a["Рецепт"]) * Convert.ToDouble(a["Сера"]);
                finalAnalog["V350"] = Convert.ToDouble(finalAnalog["V350"]) + Convert.ToDouble(a["Рецепт"]) * Convert.ToDouble(a["V350"]);
                finalAnalog["Парафины"] = Convert.ToDouble(finalAnalog["Парафины"]) + Convert.ToDouble(a["Рецепт"]) * Convert.ToDouble(a["Парафины"]);
                finalAnalog["Плотность"] = Convert.ToDouble(finalAnalog["Плотность"]) + Convert.ToDouble(a["Рецепт"]) * Convert.ToDouble(a["Плотность"]);
                finalAnalog["Вязкость"] = Convert.ToDouble(finalAnalog["Вязкость"]) + Convert.ToDouble(a["Рецепт"]) * Convert.ToDouble(a["Вязкость"]);
                
                finalAnalog["Рецепт"] = Convert.ToDouble(finalAnalog["Рецепт"]) + Convert.ToDouble(a["Рецепт"]);
                finalAnalog["Расстояние"] = CalculateDistance(finalAnalog);

                closenessAnalogs.ImportRow(a);

            }

        }
        public double CalculateDistance(DataRow analog)
        {
            var sulfur = analog["Сера"];
            var v350 = analog["V350"];
            var parafin = analog["Парафины"];
            var density = analog["Плотность"];
            var viscosity = analog["Вязкость"];
            var coefSul = Convert.ToDouble(textBox11.Text);
            var coef350 = Convert.ToDouble(textBox7.Text);
            var coefPar = Convert.ToDouble(textBox8.Text);
            var ceofSpg = Convert.ToDouble(textBox9.Text);
            var coefCst = Convert.ToDouble(textBox10.Text);
            var sul = Convert.ToDouble(textBoxSUL.Text);
            var _350 = Convert.ToDouble(textBox350.Text);
            var par = Convert.ToDouble(textBoxPAR.Text);
            var spg = Convert.ToDouble(textBoxSPG.Text);
            var cst = Convert.ToDouble(textBoxCST.Text);
            return Math.Sqrt(coefSul * (sul - Convert.ToDouble(sulfur)) * (sul - Convert.ToDouble(sulfur)) / sul / sul +
                                        coef350 * (_350 - Convert.ToDouble(v350)) * (_350 - Convert.ToDouble(v350)) / _350 / _350 +
                                        coefPar * (par - Convert.ToDouble(parafin)) * (par - Convert.ToDouble(parafin)) / coefPar / coefPar +
                                        ceofSpg * (spg - Convert.ToDouble(density)) * (spg - Convert.ToDouble(density)) / ceofSpg / ceofSpg +
                                        coefCst * (cst - Convert.ToDouble(viscosity)) * (cst - Convert.ToDouble(viscosity)) / coefCst / coefCst);

        }
        public DataTable fullPropertiesTable { get; set;}

    }
}
