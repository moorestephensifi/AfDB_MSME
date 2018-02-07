using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SBFA
{
    public partial class Tests : DevExpress.XtraEditors.XtraForm
    {
        public Tests()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int index = 204454, digits = 10;
            string name = string.Format("{0}",index.ToString().PadLeft(digits, '0'));

            MessageBox.Show(name);
        }
    }
}