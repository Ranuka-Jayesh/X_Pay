using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace X_Pay.AdminControls
{
    public partial class cal : UserControl
    {
        public cal()
        {
            InitializeComponent();
        }
        public void days(int numdays)
        {
            label1.Text = numdays + "";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cal_Load(object sender, EventArgs e)
        {

        }
    }
}
