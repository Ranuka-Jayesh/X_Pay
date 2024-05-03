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
            totIncome();
            ongoings();
            Delivered();
        }

        private void ongoings()
        {
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

        private void totIncome()
        {
            // SQL query to sum the Price column
            string query = "SELECT SUM(Price) FROM Projects";
            SqlParameter[] parameters = new SqlParameter[] { }; // No parameters needed for a simple SUM query

            db database = new db();
            object result = database.ExecuteScalar(query, parameters);  // ExecuteScalar should return the first column of the first row in the result set

            // Check for DB null values
            if (result == DBNull.Value || result == null)
            {
                TotIncome.Text = "00";
            }
            else
            {
                decimal totalIncome = Convert.ToDecimal(result); // Convert the result to decimal
                TotIncome.Text = totalIncome.ToString("N2"); // Format the number as needed, e.g., "N2" for two decimal places
            }
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
