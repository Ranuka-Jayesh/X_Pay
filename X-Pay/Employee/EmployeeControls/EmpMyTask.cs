using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using X_Pay.AdminControls.AdminProjectsSubActivity;

namespace X_Pay.Employee.EmployeeControls
{
    public partial class EmpMyTask : UserControl
    {
        public EmpMyTask()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void Register_Click(object sender, EventArgs e)
        {
            MyTaskSubActivities.EmpAllProjects AllProjects = new MyTaskSubActivities.EmpAllProjects();
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(AllProjects);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            MyTaskSubActivities.OngoingProjects ongoingProjects = new MyTaskSubActivities.OngoingProjects();
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(ongoingProjects);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MyTaskSubActivities.DeliveredProjects DeliveredProjects = new MyTaskSubActivities.DeliveredProjects();
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(DeliveredProjects);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MyTaskSubActivities.ReturnProjects ReturnProjects = new MyTaskSubActivities.ReturnProjects();
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(ReturnProjects);
        }
    }
}
