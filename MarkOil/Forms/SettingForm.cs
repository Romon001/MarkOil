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
    public partial class SettingForm : Form
    {
        Form2 forma { get; set; }
        public SettingForm()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var forma = Owner as Form2;

            if (radioButton1.Checked) { forma.isVolumeCalculation = false; }
            if (radioButton2.Checked) { forma.isVolumeCalculation = true; }

            this.Close();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            //TODO: Сделать таблицу с настройками

            Form2 ownerForm = Owner as Form2;
            if (ownerForm.isVolumeCalculation == false)
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }
        }
    }
}
