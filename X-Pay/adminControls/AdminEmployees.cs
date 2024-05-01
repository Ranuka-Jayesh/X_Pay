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
    public partial class AdminEmployees : UserControl
    {
        public AdminEmployees()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, EventArgs e)
        {
           
        }

        private void Register_Click_1(object sender, EventArgs e)
        {
            
        }

        private void Register_Click_2(object sender, EventArgs e)
        {
            AdminEmployeeSubActivity.EmployeeRegistration Registration = new AdminEmployeeSubActivity.EmployeeRegistration();
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(Registration);
        }

        private void All_Click(object sender, EventArgs e)
        {
            AdminEmployeeSubActivity.EmployeeView All = new AdminEmployeeSubActivity.EmployeeView();
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(All);
        }

        private void poke_Click(object sender, EventArgs e)
        {
            AdminEmployeeSubActivity.EmployeePokes EmpPoke = new AdminEmployeeSubActivity.EmployeePokes();
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(EmpPoke);
        }

        private void Assign_Click(object sender, EventArgs e)
        {
            AdminEmployeeSubActivity.AssignProjects assignProjects = new AdminEmployeeSubActivity.AssignProjects();
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(assignProjects);
        }

        private void makepayment_Click(object sender, EventArgs e)
        {
            AdminEmployeeSubActivity.Payments payments = new AdminEmployeeSubActivity.Payments();
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(payments);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AdminEmployeeSubActivity.ProjectReturns projectReturns = new AdminEmployeeSubActivity.ProjectReturns();
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(projectReturns);
        }
    }
}
