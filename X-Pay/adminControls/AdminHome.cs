using ScottPlot.TickGenerators.TimeUnits;
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
    public partial class AdminHome : UserControl
    {
        int month, year;
        public AdminHome()
        {
            InitializeComponent();
            displayDays();
            employeecounts();
            projectcount();
            ongoings();
            Delivered();
            incomes();

        }

        private void ongoings()
        {
            DateTime endDate = DateTime.Now;
            DateTime startDate = endDate.AddMonths(-1);
            bl4 .Text = endDate.ToString("MMMM");
            // SQL query to count only the employees with a status of 'Running'
            string query = "SELECT COUNT(*) FROM Projects WHERE Status = 'Running'";
            SqlParameter[] parameters = new SqlParameter[] { }; // No parameters needed since the value is hardcoded

            db database = new db();
            int count = database.ExecuteScalar(query, parameters); // Assume this method correctly executes the query and returns the result

            // Format and display the count based on its value
            if (count == 0)
            {
                ongoing.Text = "00";
            }
            else if (count < 10)
            {
                ongoing.Text = "0" + count.ToString();
            }
            else
            {
                ongoing.Text = count.ToString();
            }
        }

        private void Delivered()
        {
            DateTime endDate = DateTime.Now;
            DateTime startDate = endDate.AddMonths(-1);
            lb6.Text = endDate.ToString("MMMM");
            string query = "SELECT COUNT(*) FROM Projects WHERE Status = 'Delivered'";
            SqlParameter[] parameters = new SqlParameter[] { }; 

            db database = new db();
            int count = database.ExecuteScalar(query, parameters);

            if (count == 0)
            {
                Del.Text = "00";
            }
            else if (count < 10)
            {
                Del.Text = "0" + count.ToString();
            }
            else
            {
                Del.Text = count.ToString();
            }
        }

        private void employeecounts()
        {
            DateTime endDate = DateTime.Now;
            DateTime startDate = endDate.AddMonths(-1);
            MonthLB2.Text = endDate.ToString("MMMM");

            string query = "SELECT COUNT(*) FROM Employee";
            SqlParameter[] parameters = new SqlParameter[] { }; // No parameters for a simple count

            db database = new db();
            int count = database.ExecuteScalar(query, parameters);

            if (count == 0)
            {
                EmployeeCount.Text = "00";
            }
            else if (count < 10)
            {
                EmployeeCount.Text = "0" + count.ToString();
            }
            else
            {
                EmployeeCount.Text = count.ToString();
            }
        }

        private void projectcount()
        {
            DateTime endDate = DateTime.Now;
            DateTime startDate = endDate.AddMonths(-1);
            MonthLB1.Text = endDate.ToString("MMMM");

            string query = "SELECT COUNT(*) FROM Projects";
            SqlParameter[] parameters = new SqlParameter[] { }; // No parameters for a simple count

            db database = new db();
            int count = database.ExecuteScalar(query, parameters);

            if (count == 0)
            {
                ProjectCount.Text = "00";
            }
            else if (count < 10)
            {
                ProjectCount.Text = "0" + count.ToString();
            }
            else
            {
                ProjectCount.Text = count.ToString();
            }
        }

        private void incomes()
        {
            DateTime endDate = DateTime.Now;
            DateTime startDate = endDate.AddMonths(-1);
            MonthLB3.Text = endDate.ToString("MMMM");
            // Updated query with parameter for EmployeeID
            string query = $"SELECT ISNULL(SUM(Amount), 0) FROM Payments";

            db database = new db();
            // Execute the scalar query
            int totalAmount = Convert.ToInt32(database.ExecuteScalar(query, new SqlParameter[] { }));

            // Update the label with the total amount formatted as currency
            TotIncome.Text = totalAmount.ToString();
        }

        private void displayDays()
        {
            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;

            DateTime startofthemonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);
            int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;

            for (int i = 1; i <= days; i++)
            {
                AdminControls.cal daycal = new AdminControls.cal();
                daycal.days(i);
                daycontainer.Controls.Add(daycal);

                if (now.Day == i && now.Month == month && now.Year == year)
                {
                    daycal.BackColor = Color.Green;
                }
            }

            // This line sets the month label to the full name of the month
            monthlable.Text = startofthemonth.ToString("MMMM");
        }

        private void PreMon_Click(object sender, EventArgs e)
        {
            daycontainer.Controls.Clear();
            month--;
            DateTime now = DateTime.Now;
            DateTime startofthemonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);
            int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;

            for (int i = 1; i <= days; i++)
            {
                AdminControls.cal daycal = new AdminControls.cal();
                daycal.days(i);
                daycontainer.Controls.Add(daycal);

                if (now.Day == i && now.Month == month && now.Year == year)
                {
                    daycal.BackColor = Color.Green;
                }
            }

            // This line sets the month label to the full name of the month
            monthlable.Text = startofthemonth.ToString("MMMM");
        }

        private void NexMon_Click(object sender, EventArgs e)
        {
            daycontainer.Controls.Clear();
            DateTime now = DateTime.Now;
            month++;

            DateTime startofthemonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);
            int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;

            for (int i = 1; i <= days; i++)
            {
                AdminControls.cal daycal = new AdminControls.cal();
                daycal.days(i);
                daycontainer.Controls.Add(daycal);

                if (now.Day == i && now.Month == month && now.Year == year)
                {
                    daycal.BackColor = Color.Green;
                }
            }

            // This line sets the month label to the full name of the month
            monthlable.Text = startofthemonth.ToString("MMMM");
        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void EmployeeCount_Click(object sender, EventArgs e)
        {
            
        }

        private void AdminHome_Load(object sender, EventArgs e)
        {

        }
    }
}
