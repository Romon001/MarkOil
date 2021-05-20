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
    public partial class LogForm : Form
    {
        public LogForm( List<string> logMessage)
        {
            InitializeComponent();
            listBox1.DataSource = logMessage;
            
        }
    }
}
