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
            foreach (var item in _context.LIBRARY)
            {
                TreeNode b = new TreeNode();
                b.ImageKey = item.LIBRARY_ID.ToString();
                b.Text = item.TTL;
                root.Nodes.Add(b);
                var query = _context.URALS.Where(s => s.LIBRARY_ID == item.LIBRARY_ID);
//TODO: сортировка нефтей
                foreach (var crude in query) 
                {
                    TreeNode c = new TreeNode();
                    c.ImageKey = crude.NEFT_ID.ToString();
                    c.Text = crude.TTL;
                    b.Nodes.Add(c);
                    var query2 = _context.CUT_URALS.Where(s => s.NEFT_ID == crude.NEFT_ID);
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
            dataGridView1.Rows.Clear();
            var properties = _context.CUT_PROP.Where(s => s.CUT_ID.ToString()== e.Node.ImageKey);
            var phones = from p in properties
                         join c in _context.PROPERTIES on p.PROP_ID equals c.PROP_ID
                         select new { Property = c.TTL, Value = p.XVALUE.ToString()};
            foreach (var a in phones)
            {
                dataGridView1.Rows.Add(a.Property,a.Value);
            }
            //_context.CUT_PROP
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
    }
}
