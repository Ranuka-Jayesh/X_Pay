using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace X_Pay.Employee.EmployeeControls
{
    public partial class EmpHome : UserControl
    {
        int month, year;
        public EmpHome()
        {
            InitializeComponent();
            displayDays();
        }

        private void EmpHome_Load(object sender, EventArgs e)
        {

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
