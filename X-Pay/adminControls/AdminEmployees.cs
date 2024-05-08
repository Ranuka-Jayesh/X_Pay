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
            loadPendingPayments();
            loadCompletedPayments();
        }

        private void employeecounts()
        {
            string query = "SELECT COUNT(*) FROM Employee";
            SqlParameter[] parameters = new SqlParameter[] { }; // No parameters for a simple count

            db database = new db();
            int count = database.ExecuteScalar(query, parameters);

            AllEmp.Text = count.ToString("D2");

            string busyQuery = @"
             SELECT COUNT(*) FROM (
            SELECT EmployeeID
            FROM AssignProject
            GROUP BY EmployeeID
            HAVING COUNT(ProjectID) > 5
            ) AS BusyEmployees";

            int busyCount = database.ExecuteScalar(busyQuery, parameters);
            busy.Text = busyCount.ToString();
        }
        private void loadPendingPayments()
        {
            string query = "SELECT SUM(Amount) FROM Payments WHERE Status = 'Pending'";
            SqlParameter[] parameters = new SqlParameter[] { }; // No parameters needed for this query

            db database = new db();
            object result = database.ExecuteScalar(query, parameters);

            if (result != DBNull.Value)
            {
                upcome.Text = $"{result}"; // Formats the number as a currency
            }
            else
            {
                upcome.Text = "0.00";
            }
        }
        private void loadCompletedPayments()
        {
            string query = "SELECT SUM(Amount) FROM Payments WHERE Status = 'Paid'";
            SqlParameter[] parameters = new SqlParameter[] { };

            db database = new db();
            object result = database.ExecuteScalar(query, parameters);

            if (result != DBNull.Value)
            {
                completedPaymentsText.Text = $"{result}";
            }
            else
            {
                completedPaymentsText.Text = "0.00";
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
            
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void MainPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
