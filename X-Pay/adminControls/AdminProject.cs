using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace X_Pay.AdminControls
{
    public partial class AdminProject : UserControl
    {
        public AdminProject()
        {
            InitializeComponent();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AddProject_Click(object sender, EventArgs e)
        {
            AdminProjectsSubActivity.AddProjects addProjects = new AdminProjectsSubActivity.AddProjects();
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(addProjects);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminProjectsSubActivity.AllProjects allProjects = new AdminProjectsSubActivity.AllProjects();
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(allProjects);
        }
    }
}
