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
using X_Pay.AdminControls.AdminProjectsSubActivity;

namespace X_Pay.Employee.EmployeeControls
{
    public partial class EmpMyTask : UserControl
    {
        private int EmployeeID;
        public EmpMyTask(int emp)
        {
            InitializeComponent();
            EmployeeID = emp;
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
            MyTaskSubActivities.OngoingProjects ongoingProjects = new MyTaskSubActivities.OngoingProjects(EmployeeID);
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(ongoingProjects);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MyTaskSubActivities.DeliveredProjects DeliveredProjects = new MyTaskSubActivities.DeliveredProjects(EmployeeID);
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

        private void MainPanel_Paint(object sender, PaintEventArgs e)
        {
            ongoings();
            timetracking();
            del();
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
        private void ongoings()
        {
            // SQL query to count only the employees with a status of 'Running'
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

    }
}
