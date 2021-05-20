using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace MarkOil.Models
{
    class TrueCheckBoxTemplate : DataGridViewCheckBoxCell
    {
        public TrueCheckBoxTemplate() : base()
        {
            this.FalseValue = 0;
            this.TrueValue = 1;
            this.Value = FalseValue;
        }
    }
}
