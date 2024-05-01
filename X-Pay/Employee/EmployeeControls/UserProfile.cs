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
    public partial class UserProfile : UserControl
    {
        public UserProfile()
        {
            InitializeComponent();
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, profilepic.Width, profilepic.Height);
            profilepic.Region = new Region(path);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            cp.Text= string.Empty;
            np.Text= string.Empty;
        }
    }
}
