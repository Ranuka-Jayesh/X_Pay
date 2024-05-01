using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace X_Pay.Employee.EmployeeControls
{
    public partial class FreeTask : UserControl
    {
        public FreeTask()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                if (btn.Tag == null || btn.Tag.ToString() != "green")
                {
                    btn.BackColor = Color.Green;
                    btn.ForeColor = Color.White;
                    btn.Text = "👍 Poked";
                    btn.Tag = "green";
                }
                else
                {
                    btn.BackColor = Color.Crimson;
                    btn.ForeColor = Color.White;
                    btn.Text = "👉 Poke";
                    btn.Tag = "Crimson";
                }
            }
        }

    }
}
