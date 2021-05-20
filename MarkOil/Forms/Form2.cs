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
using MarkOil.Models;
namespace MarkOil
{
    public partial class Form2 : Form
    {
        dbMarkEntities _context;
        public DataTable fullPropertiesTable { get; set; }
        //public bool isOilMixed { get; set; } = false;
        public DataRow mixedOil { get; set; }
        DataRow initialOil { get; set; }
        Forms.LogForm logForm { get; set; }
        Forms.SettingForm settingForm { get; set; }
        List<string> logMessage = new List<string>();

        public bool isVolumeCalculation = false;

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
            if(logForm != null)
            {
                logForm.Close();
            }
            logForm = new Forms.LogForm(logMessage);
            logForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            toggleSwitch1.Enabled = false;
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

        //Функция от удаленной кнопки, прототип метода button1_Click_1
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
            closenessAnalogs.Columns.Add("Выход 350С", System.Type.GetType("System.Double"));
            closenessAnalogs.Columns.Add("Парафины", System.Type.GetType("System.Double"));
            closenessAnalogs.Columns.Add("Плотность", System.Type.GetType("System.Double"));
            closenessAnalogs.Columns.Add("Вязкость");
            closenessAnalogs.Columns.Add("Расстояние", System.Type.GetType("System.Double"));
            closenessAnalogs.Columns.Add("Рецепт", System.Type.GetType("System.Double"));

            
            DataRow finalAnalog = closenessAnalogs.NewRow();
            finalAnalog["Название"] = "Смесь нефтей";
            finalAnalog["Сера"] = 0;
            finalAnalog["Выход 350С"] = 0;
            finalAnalog["Парафины"] = 0;
            finalAnalog["Плотность"] = 0;
            finalAnalog["Вязкость"] = 0;
            finalAnalog["Расстояние"] = 0;
            finalAnalog["Рецепт"] = 0;

            //Рассчет рецепта
            if (answer.ToString() == "Yes")
            {
                CalculateRecipeLP(finalAnalog, closenessAnalogs);
            }
            else
            {
                //TODO: Поменять функцию как ЛП с 2мя аргументами
                //TODO: отключить возможность менять свойства в таблице(кроме галочки)
                CalculateRecipe(analogs, finalAnalog, closenessAnalogs);

            }
            //Добавление конечной смеси в список
            closenessAnalogs.Rows.Add(finalAnalog);
            closenessAnalogs.Rows.Add("Исходная нефть",sul,_350,par, spg, cst, "0,0000","1,000");

            dataGridView1.DataSource = closenessAnalogs;
            dataGridView1.Refresh();

            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[5].Width = 100;
            dataGridView1.Columns[6].Width = 100;
            dataGridView1.Columns[7].Width = 100;

            dataGridView1.Refresh();
            calculateButton.Enabled = true;

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
            propTable.Columns.Add("Выход 350С", System.Type.GetType("System.Double"));
            propTable.Columns.Add("Парафины", System.Type.GetType("System.Double"));
            propTable.Columns.Add("Плотность", System.Type.GetType("System.Double"));
            propTable.Columns.Add("Вязкость");
            propTable.Columns.Add("Расстояние");
            propTable.Columns.Add("Рецепт");
            propTable.Columns["Рецепт"].DefaultValue = 0;
            propTable.Columns["Расстояние"].DefaultValue = Double.PositiveInfinity;

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
                workRow["Выход 350С"] = fractions.Where(p => p.CUT_ID> oilCutId && p.CUT_ID <= frac350CutId)
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
                finalAnalog["Выход 350С"] = Convert.ToDouble(finalAnalog["Выход 350С"]) + Convert.ToDouble(a["Рецепт"]) * Convert.ToDouble(a["Выход 350С"]);
                finalAnalog["Парафины"] = Convert.ToDouble(finalAnalog["Парафины"]) + Convert.ToDouble(a["Рецепт"]) * Convert.ToDouble(a["Парафины"]);
                finalAnalog["Плотность"] = Convert.ToDouble(finalAnalog["Плотность"]) + Convert.ToDouble(a["Рецепт"]) * Convert.ToDouble(a["Плотность"]);
                finalAnalog["Вязкость"] = Convert.ToDouble(finalAnalog["Вязкость"]) + Convert.ToDouble(a["Рецепт"]) * Convert.ToDouble(a["Вязкость"]);
                finalAnalog["Расстояние"] =CalculateDistance(finalAnalog);
                finalAnalog["Рецепт"] = Convert.ToDouble(finalAnalog["Рецепт"]) + Convert.ToDouble(a["Рецепт"]);
                a["Рецепт"] = Math.Round(Convert.ToDouble(a["Рецепт"]), 4);
                finalAnalog["Рецепт"] = Math.Round(Convert.ToDouble(finalAnalog["Рецепт"]), 4);

                closenessAnalogs.ImportRow(a);
                
            }
        }
        public void CalculateRecipeLP(DataRow finalAnalog, DataTable closenessAnalogs)
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
            for (int i = 0; i < Convert.ToInt32(closenessAnalogs.Rows.Count); i++)
            {
                Decision alpha = new Decision(Domain.RealRange(0,1), $"alpha{i}") ;
                model.AddDecision(alpha);
                model.AddConstraint($"constraintAlpha{i}", 0 <= alpha <= 1);

                errorSul -= alpha * Convert.ToDouble(closenessAnalogs.Rows[i]["Сера"]);
                errorV350 -= alpha * Convert.ToDouble(closenessAnalogs.Rows[i]["Выход 350С"]);
                errorPar -= alpha * Convert.ToDouble(closenessAnalogs.Rows[i]["Парафины"]);
                errorSPG -= alpha * Convert.ToDouble(closenessAnalogs.Rows[i]["Плотность"]);
                errorCST -= alpha * Convert.ToDouble(closenessAnalogs.Rows[i]["Вязкость"]);
                
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
            label13.Text = $"Целевая функция = {Math.Round(solution.Goals.First().ToDouble(),4)}";    
            var solutions = solution.Decisions.ToList();
            for (int i=0;i< solutions.Count();i++)
            {
                closenessAnalogs.Rows[i]["Рецепт"] = solutions[i].ToDouble();
                 var a = solutions[i].ToDouble();
            }
            foreach (DataRow a in closenessAnalogs.Rows)
            {
                finalAnalog["Сера"] = Convert.ToDouble(finalAnalog["Сера"]) + Convert.ToDouble(a["Рецепт"]) * Convert.ToDouble(a["Сера"]);
                finalAnalog["Выход 350С"] = Convert.ToDouble(finalAnalog["Выход 350С"]) + Convert.ToDouble(a["Рецепт"]) * Convert.ToDouble(a["Выход 350С"]);
                finalAnalog["Парафины"] = Convert.ToDouble(finalAnalog["Парафины"]) + Convert.ToDouble(a["Рецепт"]) * Convert.ToDouble(a["Парафины"]);
                finalAnalog["Плотность"] = Convert.ToDouble(finalAnalog["Плотность"]) + Convert.ToDouble(a["Рецепт"]) * Convert.ToDouble(a["Плотность"]);
                finalAnalog["Вязкость"] = Convert.ToDouble(finalAnalog["Вязкость"]) + Convert.ToDouble(a["Рецепт"]) * Convert.ToDouble(a["Вязкость"]);
                
                finalAnalog["Рецепт"] = Convert.ToDouble(finalAnalog["Рецепт"]) + Convert.ToDouble(a["Рецепт"]);
                finalAnalog["Расстояние"] = CalculateDistance(finalAnalog);
                a["Рецепт"] = Math.Round(Convert.ToDouble(a["Рецепт"]),4);
                finalAnalog["Рецепт"] = Math.Round(Convert.ToDouble(finalAnalog["Рецепт"]), 4);

            }

        }
        public double CalculateDistance(DataRow analog)
        {
            var sulfur = analog["Сера"];
            var v350 = analog["Выход 350С"];
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


        private void button1_Click_1(object sender, EventArgs e)
        {
            toggleSwitch1.Enabled = false;
            label13.Text = "";

            var sul = Convert.ToDouble(textBoxSUL.Text);
            var _350 = Convert.ToDouble(textBox350.Text);
            var par = Convert.ToDouble(textBoxPAR.Text);
            var spg = Convert.ToDouble(textBoxSPG.Text);
            var cst = Convert.ToDouble(textBoxCST.Text);

            label7.Text = "Нефти аналоги по близости  , кг";

            List<string> properties = new List<string> { "Сера", "Выход 350С", "Парафины","Плотность","Вязкость" };

            DataTable closenessAnalogs = new DataTable();
            closenessAnalogs.Columns.Add("Название", System.Type.GetType("System.String"));
            closenessAnalogs.Columns.Add("Сера", System.Type.GetType("System.Double"));
            closenessAnalogs.Columns.Add("Выход 350С", System.Type.GetType("System.Double"));
            closenessAnalogs.Columns.Add("Парафины", System.Type.GetType("System.Double"));
            closenessAnalogs.Columns.Add("Плотность", System.Type.GetType("System.Double"));
            closenessAnalogs.Columns.Add("Вязкость");
            closenessAnalogs.Columns.Add("Расстояние", System.Type.GetType("System.Double"));
            closenessAnalogs.Columns.Add("Рецепт", System.Type.GetType("System.Double"));

            initialOil = closenessAnalogs.NewRow();
            initialOil["Название"] = "Исходная нефть";
            initialOil["сера"] = Convert.ToDouble(textBoxSUL.Text);
            initialOil["Выход 350С"] = Convert.ToDouble(textBox350.Text);
            initialOil["Парафины"] = Convert.ToDouble(textBoxPAR.Text);
            initialOil["Плотность"] = Convert.ToDouble(textBoxSPG.Text);
            initialOil["Вязкость"] = Convert.ToDouble(textBoxCST.Text);
            initialOil["Расстояние"] = "0,0000";
            initialOil["Рецепт"] = "1,000";

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

            //var answer = MessageBox.Show("Решить с помощью линейного программирования?",
            //                             "Сообщение",
            //                             MessageBoxButtons.YesNo,
            //                             MessageBoxIcon.Information,
            //                             MessageBoxDefaultButton.Button1,
            //                             MessageBoxOptions.DefaultDesktopOnly);

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
                b["Рецепт"] = 0;
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


            foreach (var a in analogs)
            {
                closenessAnalogs.ImportRow(a);
            }



            //Поиск минимальных и максимальных значений свойств аналогов
            DataRow maxAnalog = closenessAnalogs.NewRow();
            DataRow minAnalog = closenessAnalogs.NewRow();
            maxAnalog.ItemArray = closenessAnalogs.Rows[0].ItemArray.Clone() as object[];
            minAnalog.ItemArray = closenessAnalogs.Rows[0].ItemArray.Clone() as object[];

            foreach(string property in properties)
            {
                foreach (DataRow a in closenessAnalogs.Rows)
                {
                    minAnalog[property] = (Convert.ToDouble(a[property]) < Convert.ToDouble(minAnalog[property])) ? a[property]: minAnalog[property];
                    maxAnalog[property] = (Convert.ToDouble(a[property]) > Convert.ToDouble(maxAnalog[property])) ? a[property] : maxAnalog[property];
                }
            }



            //Сообщение о том что наша нефть не в рамках свойств аналогов
                Dictionary<string, string> checkProperties = new Dictionary<string, string>{
                                                                              {"Сера", "Норма"},
                                                                              {"Выход 350С", "Норма"},
                                                                              {"Парафины", "Норма"},
                                                                              {"Плотность", "Норма"},
                                                                              {"Вязкость", "Норма"}
                                                                              };
            if (sul > Convert.ToDouble(maxAnalog["Сера"])) { checkProperties["Сера"] = "Больше"; }
            if (sul < Convert.ToDouble(minAnalog["Сера"])) { checkProperties["Сера"] = "Меньше"; }
            if (_350 > Convert.ToDouble(maxAnalog["Выход 350С"])) { checkProperties["Выход 350С"] = "Больше"; }
            if (_350 < Convert.ToDouble(minAnalog["Выход 350С"])) { checkProperties["Выход 350С"] = "Меньше"; }
            if (par > Convert.ToDouble(maxAnalog["Парафины"])) { checkProperties["Парафины"] = "Больше"; }
            if (par < Convert.ToDouble(minAnalog["Парафины"])) { checkProperties["Парафины"] = "Меньше"; }
            if (spg > Convert.ToDouble(maxAnalog["Плотность"])) { checkProperties["Плотность"] = "Больше"; }
            if (spg < Convert.ToDouble(minAnalog["Плотность"])) { checkProperties["Плотность"] = "Меньше"; }
            if (cst > Convert.ToDouble(maxAnalog["Вязкость"])) { checkProperties["Вязкость"] = "Больше"; }
            if (cst < Convert.ToDouble(minAnalog["Вязкость"])) { checkProperties["Вязкость"] = "Меньше"; }
            

            //Валидация на то, что наша нефть в рамках свойств аналогов
            DataTable supportingAnalogs = CreateRangeException(checkProperties, propertiesTable);

            closenessAnalogs.Merge(supportingAnalogs);

            //Колонка галочки
            DataGridViewCheckBoxColumn col = new DataGridViewCheckBoxColumn();
            col.Name = "Checked";
            col.HeaderText = " ";
            col.CellTemplate = new TrueCheckBoxTemplate();
            dataGridView1.Columns.Add(col);

            dataGridView1.DataSource = closenessAnalogs;
            for (int i = 0; i < Convert.ToInt32(textBox16.Text); i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = 1;
            }
            dataGridView1.Refresh();

            dataGridView1.Columns[0].Width = 20;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[4].Width = 80;
            dataGridView1.Columns[5].Width = 80;
            dataGridView1.Columns[6].Width = 80;
            dataGridView1.Columns[7].Width = 80;
            dataGridView1.Columns[8].Width = 80;
            

            dataGridView1.Refresh();
            calculateButton.Enabled = true;

            // конец подбора аналогов

 

        }

        private void calculateButtonClick(object sender, EventArgs e)
        {
            
            DataTable closenessAnalogs = new DataTable();
            closenessAnalogs.Columns.Add("Название", System.Type.GetType("System.String"));
            closenessAnalogs.Columns.Add("Сера", System.Type.GetType("System.Double"));
            closenessAnalogs.Columns.Add("Выход 350С", System.Type.GetType("System.Double"));
            closenessAnalogs.Columns.Add("Парафины", System.Type.GetType("System.Double"));
            closenessAnalogs.Columns.Add("Плотность", System.Type.GetType("System.Double"));
            closenessAnalogs.Columns.Add("Вязкость");
            closenessAnalogs.Columns.Add("Расстояние", System.Type.GetType("System.Double"));
            closenessAnalogs.Columns.Add("Рецепт", System.Type.GetType("System.Double"));
            //DataTable gridTable = (DataTable)dataGridView1.DataSource;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if ((int)row.Cells[0].Value == 1)
                {
                    DataRow a = ((DataRowView)row.DataBoundItem).Row;
                    closenessAnalogs.ImportRow(a);

                }
            }

            DataRow finalAnalog = closenessAnalogs.NewRow();
            finalAnalog["Название"] = "Смесь нефтей";
            finalAnalog["Сера"] = 0;
            finalAnalog["Выход 350С"] = 0;
            finalAnalog["Парафины"] = 0;
            finalAnalog["Плотность"] = 0;
            finalAnalog["Вязкость"] = 0;
            finalAnalog["Расстояние"] = 0;
            finalAnalog["Рецепт"] = 0;

            CalculateRecipeLP(finalAnalog, closenessAnalogs);


            closenessAnalogs.Rows.Add(finalAnalog);
            closenessAnalogs.Rows.Add("Исходная нефть",
                                      Convert.ToDouble(textBoxSUL.Text),
                                      Convert.ToDouble(textBox350.Text),
                                      Convert.ToDouble(textBoxPAR.Text),
                                      Convert.ToDouble(textBoxSPG.Text),
                                      Convert.ToDouble(textBoxCST.Text),
                                      "0,0000",
                                      "1,000");
            dataGridView1.DataSource = closenessAnalogs;

            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 95;
            dataGridView1.Columns[4].Width = 95;
            dataGridView1.Columns[5].Width = 95;
            dataGridView1.Columns[6].Width = 95;
            dataGridView1.Columns[7].Width = 95;

            dataGridView1.Refresh();
            dataGridView1.Columns[0].Visible = false;
            calculateButton.Enabled = false;
            toggleSwitch1.Enabled = true;
            toggleSwitch1.IsOn = false;
            mixedOil = finalAnalog;
        }

        private void toggleSwitch1_Toggled(object sender, EventArgs e)
        {

            if (toggleSwitch1.IsOn)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if  (!((row.Cells["Название"].Value.ToString()=="Исходная нефть")||( row.Cells["Название"].Value.ToString()=="Смесь нефтей")))
                    {
                        double value = Convert.ToDouble(row.Cells["Рецепт"].Value) / Convert.ToDouble(row.Cells["Плотность"].Value) * Convert.ToDouble(mixedOil["Плотность"]);
                        row.Cells["Рецепт"].Value = Math.Round(value, 4);
                    }
                }
                label7.Text = "Нефти аналоги по близости  , м^3";
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!((row.Cells["Название"].Value.ToString() == "Исходная нефть") || (row.Cells["Название"].Value.ToString() == "Смесь нефтей")))
                    {
                        double value = Convert.ToDouble(row.Cells["Рецепт"].Value) * Convert.ToDouble(row.Cells["Плотность"].Value) / Convert.ToDouble(mixedOil["Плотность"]);
                        row.Cells["Рецепт"].Value = Math.Round(value, 4);
                    }
                }
                label7.Text = "Нефти аналоги по близости  , кг";
            }
        }
        public DataTable CreateRangeException(Dictionary<string,string> properties, DataTable propertiesTable)
        {
            DataTable analogs = new DataTable();
            analogs.Columns.Add("Название", System.Type.GetType("System.String"));
            analogs.Columns.Add("Сера", System.Type.GetType("System.Double"));
            analogs.Columns.Add("Выход 350С", System.Type.GetType("System.Double"));
            analogs.Columns.Add("Парафины", System.Type.GetType("System.Double"));
            analogs.Columns.Add("Плотность", System.Type.GetType("System.Double"));
            analogs.Columns.Add("Вязкость");
            analogs.Columns.Add("Расстояние", System.Type.GetType("System.Double"));
            analogs.Columns.Add("Рецепт", System.Type.GetType("System.Double"));
            //TODO: Сделать класс с шаблоном таблицы;

            //Словарь нефтей, ближайших по отдельным свойствам
            Dictionary<string, DataRow> closestAnalogsDictionary = new Dictionary<string, DataRow>();
            DataRow analog = propertiesTable.NewRow();
            analog["Название"] = "начальная нефть";
            analog["Сера"] = 999999;
            analog["Выход 350С"] = 9999999;
            analog["Парафины"] = 999999999;
            analog["Плотность"] = 999999999;
            analog["Вязкость"] = 999999999;
            analog["Расстояние"] = 999999999;
            analog["Рецепт"] = 0;
            foreach (var property in properties)
            {
                if (property.Value != "Норма") 
                {

                    closestAnalogsDictionary.Add(property.Key, analog);
                }

            }
            
            foreach (DataRow row in propertiesTable.Rows)
            {
                
                foreach (KeyValuePair<string,string> property in properties)
                {
                    double distance = Convert.ToDouble(row[property.Key]) - Convert.ToDouble(initialOil[property.Key]);
                    if (properties[property.Key] == "Больше")
                    {
                        if((distance < Convert.ToDouble(closestAnalogsDictionary[property.Key][property.Key]) - Convert.ToDouble(initialOil[property.Key]))
                            &&(distance>0))
                        {
                            closestAnalogsDictionary[property.Key] = row;
                        }
                    }
                    if (properties[property.Key] == "Меньше")
                    {
                        if ((distance > Convert.ToDouble(closestAnalogsDictionary[property.Key][property.Key]) - Convert.ToDouble(initialOil[property.Key]))
                            && (distance < 0))
                        {
                            closestAnalogsDictionary[property.Key] = row;
                        }
                    }
                }
            }
            var distinctList = closestAnalogsDictionary.Values.Distinct().ToList();

                logMessage.Clear();
            foreach (DataRow row in distinctList)
            {
                if (row["Название"] == "начальная нефть")
                {
                    logMessage.Add($"Нет подходящей нефти для смешения по параметру(aм) {string.Join(",", closestAnalogsDictionary.Where(x => x.Value == row).Select(x => x.Key))}");
                }
                else
                {
                    logMessage.Add($"{row["Название"]} {string.Join(",", closestAnalogsDictionary.Where(x => x.Value == row).Select(x => x.Key))}");
                }
                    //TODO: клонировать нормально без буфера

                analogs.ImportRow(row);
            }
            return analogs;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (settingForm != null)
            {
                settingForm.Close();
            }
            settingForm = new Forms.SettingForm() { Owner = this };
            settingForm.Show();
        }
    }
}
