using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
            employeecounts();
            upcoming();
        }

        private void employeecounts()
        {
            string query = "SELECT COUNT(*) FROM Employee";
            SqlParameter[] parameters = new SqlParameter[] { }; // No parameters for a simple count

            db database = new db();
            int count = database.ExecuteScalar(query, parameters);

            if (count == 0)
            {
                AllEmp.Text = "00";
            }
            else if (count < 10)
            {
                AllEmp.Text = "0" + count.ToString();
            }
            else
            {
                AllEmp.Text = count.ToString();
            }
        }
        private void upcoming()
        {
            // Define the date range for the last month
            DateTime endDate = DateTime.Now;
            DateTime startDate = endDate.AddMonths(-1);
            MonthLB3.Text = endDate.ToString("MMMM");

            // Query to get the total of Epayments for the given EmployeeID
            string query = $"SELECT SUM(Amount) FROM Payments";
            SqlParameter[] parameters = new SqlParameter[] { }; // No parameters in this query

            db database = new db();
            decimal? totalEpayments = database.ExecuteScalar(query, parameters);

            // Formatting and displaying the total Epayments in a label
            if (totalEpayments == null || totalEpayments == 0)
            {
                TotIncome.Text = "No payments";
            }
            else
            {
                TotIncome.Text = $"{totalEpayments}"; // Formats the number as currency
            }
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

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void MainPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
