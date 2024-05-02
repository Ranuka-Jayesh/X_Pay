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
