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

namespace X_Pay.Employee.EmployeeControls
{
    public partial class EmpMyWallet : UserControl
    {
        int EmployeeID;
        public EmpMyWallet(int emp)
        {
            InitializeComponent();
            EmployeeID = emp;   
        }

        private void Register_Click(object sender, EventArgs e)
        {
            MyWalletSubActivities.UpcomingPayments upcomingPayments = new MyWalletSubActivities.UpcomingPayments();
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(upcomingPayments);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyWalletSubActivities.Income Income = new MyWalletSubActivities.Income();
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(Income);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MyWalletSubActivities.TotalIncome TotalIncome = new MyWalletSubActivities.TotalIncome();
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(TotalIncome);
        }

        private void MainPanel_Paint(object sender, PaintEventArgs e)
        {
            upcoming();
        }

        private void upcoming()
        {
            // Query to get the total of Epayments for the given EmployeeID
            string query = $"SELECT SUM(Amount) FROM Payments WHERE EmployeeID = {EmployeeID} AND Status = 'Pending';";
            SqlParameter[] parameters = new SqlParameter[] { }; // No parameters in this query

            db database = new db();
            decimal? totalEpayments = database.ExecuteScalar(query, parameters);

            // Formatting and displaying the total Epayments in a label
            if (totalEpayments == null || totalEpayments == 0)
            {
                Upcomings.Text = "No payments";
            }
            else
            {
                Upcomings.Text = $"{totalEpayments}"; // Formats the number as currency
            }
        }

    }
}
