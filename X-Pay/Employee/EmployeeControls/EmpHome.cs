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
    public partial class EmpHome : UserControl
    {
        int EmployeeID;
        int month, year;
        public EmpHome(int emp)
        {
            InitializeComponent();
            displayDays();
            EmployeeID = emp;
        }

        private void EmpHome_Load(object sender, EventArgs e)
        {
            ongoings();
            upcoming();
            timetracking();
            del();
        }
        private void ongoings()
        {
            string query = $"SELECT COUNT(*) FROM AssignProject WHERE EmployeeID = {EmployeeID};";
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
        private void timetracking()
        {
            string sql = $@"
            SELECT ap.ProjectID, p.ProjectType, p.DeadLine
            FROM AssignProject ap
            JOIN Projects p ON ap.ProjectID = p.ProjectID
            WHERE ap.EmployeeID = {EmployeeID};";

            var reader = new db().Select(sql);
            dataview.Rows.Clear();

            List<int> negativeTimeRows = new List<int>();  // List to store row indices with negative countdown

            while (reader.Read())
            {
                DateTime deadline = Convert.ToDateTime(reader["DeadLine"]);
                TimeSpan timeUntilDeadline = deadline - DateTime.Now;

                // Prepare the countdown string
                string countdown = $"{timeUntilDeadline.Days} Days, {timeUntilDeadline.Hours} Hrs, {timeUntilDeadline.Minutes} Min";
                dataview.Rows.Add(reader["ProjectID"], reader["ProjectType"], countdown);

                // Check if the timeUntilDeadline is negative and store the row index
                if (timeUntilDeadline < TimeSpan.Zero)
                {
                    negativeTimeRows.Add(dataview.Rows.Count - 1);
                }
            }

            // Apply the formatting after the rows are added
            foreach (int rowIndex in negativeTimeRows)
            {
                dataview.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Red;
                dataview.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.White;
            }

            // Optionally format the rest directly here or in another loop
          
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
        private void del()
        {
            string query = $"SELECT COUNT(*) FROM ProjectDelivery WHERE EmployeeID = {EmployeeID};";
            SqlParameter[] parameters = new SqlParameter[] { }; // No parameters needed since the value is hardcoded

            db database = new db();
            int count = database.ExecuteScalar(query, parameters); // Assume this method correctly executes the query and returns the result

            // Format and display the count based on its value
            if (count == 0)
            {
                delp.Text = "00";
            }
            else if (count < 10)
            {
                delp.Text = "0" + count.ToString();
            }
            else
            {
                delp.Text = count.ToString();
            }
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
                Employee.call daycal = new Employee.call();
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
                Employee.call daycal = new Employee.call();
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

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

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
                Employee.call daycal = new Employee.call();
                daycal.days(i);
                daycontainer.Controls.Add(daycal);

                if (now.Day == i && now.Month == month && now.Year == year)
                {
                    daycal.BackColor = Color.Red;
                }
            }

            // This line sets the month label to the full name of the month
            monthlable.Text = startofthemonth.ToString("MMMM");
        }
    }
}
