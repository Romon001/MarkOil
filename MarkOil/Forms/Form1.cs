using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarkOil.Forms;

namespace MarkOil
{

    public partial class Form1 : Form
    {
        dbMarkEntities _context;
            
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _context = new dbMarkEntities();

            TreeNode root = new TreeNode("Банк Нефтей");
            treeView1.Nodes.Add(root);
            //Добавление библиотек
            foreach (var item in _context.LIBRARY)
            {
                TreeNode b = new TreeNode();
                b.ImageKey = item.LIBRARY_ID.ToString();
                b.Text = item.TTL;
                root.Nodes.Add(b);
                var query = _context.URALS.Where(s => s.LIBRARY_ID == item.LIBRARY_ID);
                //TODO: сортировка нефтей
                //Добавление нефтей
                foreach (var crude in query) 
                {
                    TreeNode c = new TreeNode();
                    c.ImageKey = crude.NEFT_ID.ToString();
                    c.Text = crude.TTL;
                    b.Nodes.Add(c);
                    var query2 = _context.CUT_URALS.Where(s => s.NEFT_ID == crude.NEFT_ID);
                    //Добавление фракций нефтей
                    foreach (var frac in query2) 
                    {
                        TreeNode d = new TreeNode();
                        d.ImageKey = frac.CUT_ID.ToString();
                        d.Text = frac.TTL;
                        c.Nodes.Add(d);
                    }
                }
                foreach (var a in root.Nodes)
                {
                  
                }
            }

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.ImageKey != "")
            {
                dataGridView1.Rows.Clear();
                var properties = _context.CUT_PROP.Where(s => s.CUT_ID.ToString() == e.Node.ImageKey);
                var buffer = from p in properties
                             join c in _context.PROPERTIES on p.PROP_ID equals c.PROP_ID
                             select new { Property = c.TTL, Value = p.XVALUE.ToString() };
                
                var yld = _context.CUT_URALS.Where(s => s.CUT_ID.ToString() == e.Node.ImageKey).FirstOrDefault();
                if (yld != null)
                {
                    dataGridView1.Rows.Add("Объемная доля в нефти, %", yld.YLD);
                }

                foreach (var a in buffer)
                {
                    dataGridView1.Rows.Add(a.Property, a.Value);
                }
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            Form2 newForm = new Form2();
            newForm.Show();
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            Form3 newForm = new Form3();
            newForm.Show();
        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            Form4 newForm = new Form4();
            newForm.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            TEST newForm = new TEST();
            newForm.Show();
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            Form5 newForm = new Form5();
            newForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //DataTable Crude = CreateCrudeDataTable();
            //ToCSV(Crude, "C:\\Users\\roman_000\\Desktop\\csv\\Crude.csv");

            //DataTable Cuts = CreateCutsDataTable();
            //ToCSV(Cuts, "C:\\Users\\roman_000\\Desktop\\csv\\Cuts.csv");

           // DataTable CutSets = CreateCutSetDataTable();
           // ToCSV(CutSets, "C:\\Users\\roman_000\\Desktop\\csv\\CutSets.csv");
            
            ToCSV(CreateYieldPropertyTable(), "C:\\Users\\roman_000\\Desktop\\csv\\Yield.csv");

        }
        public DataTable CreateCrudeDataTable() 
        {
            var indexLibraryOils = _context.URALS.Where(p => p.LIBRARY_ID == 163);
            DataTable Crudes = new DataTable();
            Crudes.Columns.Add("CrudeID", System.Type.GetType("System.Int32"));
            Crudes.Columns.Add("Name", System.Type.GetType("System.String"));
            Crudes.Columns.Add("LibraryID", System.Type.GetType("System.Int32"));
            foreach(var oil in indexLibraryOils)
            {
                DataRow row = Crudes.NewRow();
                row["CrudeID"] = oil.NEFT_ID;
                row["Name"] = oil.TTL;
                row["LibraryID"] = 2;
                Crudes.Rows.Add(row);
            }
            return Crudes;
        }
        public DataTable CreateCutsDataTable()
        {
            var indexLibraryOils = _context.URALS.Where(p => p.LIBRARY_ID == 163);

            var query = from f in indexLibraryOils
                        join c in _context.CUT_URALS
                            on f.NEFT_ID equals c.NEFT_ID
                        select new { Name = c.TTL, CutSetID = c.NEFT_ID, CutId = c.CUT_ID };

            DataTable Cuts = new DataTable();
            Cuts.Columns.Add("Name", System.Type.GetType("System.String"));
            Cuts.Columns.Add("CutSetID", System.Type.GetType("System.Int32"));
            Cuts.Columns.Add("CutId", System.Type.GetType("System.Int32"));
            
            foreach (var cut in query)
            {
                DataRow row = Cuts.NewRow();
                row["Name"] = cut.Name;
                row["CutSetID"] = cut.CutSetID;
                row["CutId"] = cut.CutId;
                Cuts.Rows.Add(row);
            }
            return Cuts;
        }
        public void ToCSV( DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            //headers    
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(";");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(';'))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(";");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }
        public DataTable CreateCutSetDataTable()
        {
            DataTable CutSets = new DataTable();
            CutSets.Columns.Add("CutSetID", System.Type.GetType("System.Int32"));
            CutSets.Columns.Add("CrudeID", System.Type.GetType("System.Int32"));

            var indexLibraryOils = _context.URALS.Where(p => p.LIBRARY_ID == 163);
            foreach (var oil in indexLibraryOils)
            {
                DataRow row = CutSets.NewRow();
                row["CutSetID"] = oil.NEFT_ID;
                row["CrudeID"] = oil.NEFT_ID;
                CutSets.Rows.Add(row);
            }
            return CutSets;
        }
        public DataTable CreatePropertiesDatatable()
        {
            var indexLibraryOils = _context.URALS.Where(p => p.LIBRARY_ID == 163);
            var query = from f in indexLibraryOils
                        join c in _context.CUT_URALS
                            on f.NEFT_ID equals c.NEFT_ID
                        select new { Name = c.TTL, CutSetID = c.NEFT_ID, CutId = c.CUT_ID };
            var props  = from f in query
                         join c in _context.CUT_PROP
                            on f.CutId equals c.CUT_ID
                         select new { CutId = c.CUT_ID, Value = c.XVALUE, PrpertyTypeId= c.PROP_ID };
            DataTable Properties = new DataTable();
            Properties.Columns.Add("CutId", System.Type.GetType("System.Int32"));
            Properties.Columns.Add("Value", System.Type.GetType("System.Double"));
            Properties.Columns.Add("PrpertyTypeId", System.Type.GetType("System.Int32"));
            foreach (var prop in props)
            {
                DataRow row = Properties.NewRow();
                row["CutId"] = prop.CutId;
                row["Value"] = Convert.ToDouble(prop.Value.ToString().Replace(",", "."));
                row["PrpertyTypeId"] = prop.PrpertyTypeId;
                Properties.Rows.Add(row);
            }
            return Properties;

        }
        public DataTable CreateYieldPropertyTable()
        {
            var indexLibraryOils = _context.URALS.Where(p => p.LIBRARY_ID == 163);
            var query = from f in indexLibraryOils
                        join c in _context.CUT_URALS
                            on f.NEFT_ID equals c.NEFT_ID
                        select new { Name = c.TTL, CutSetID = c.NEFT_ID, CutId = c.CUT_ID,YLD=c.YLD};
            DataTable YieldProps = new DataTable();
            YieldProps.Columns.Add("CutId", System.Type.GetType("System.Int32"));
            YieldProps.Columns.Add("Value", System.Type.GetType("System.Double"));
            YieldProps.Columns.Add("UOM", System.Type.GetType("System.Int32"));
            YieldProps.Columns.Add("PrpertyTypeId", System.Type.GetType("System.Int32"));

            foreach (var prop in query)
            {
                DataRow row = YieldProps.NewRow();
                row["CutId"] = prop.CutId;
                row["Value"] = Convert.ToDouble(prop.YLD.ToString().Replace(",", "."));
                row["PrpertyTypeId"] = 3;
                row["UOM"] = 6;
                YieldProps.Rows.Add(row);
            }
            return YieldProps;
        }

    }

}
