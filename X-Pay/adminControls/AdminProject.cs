using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace X_Pay.AdminControls
{
    public partial class AdminProject : UserControl
    {
        public AdminProject()
        {
            InitializeComponent();
            ongoing();
            Delivered();
            Alls();
        }

        private void Alls()
        {

            string query = "SELECT COUNT(*) FROM Projects";
            SqlParameter[] parameters = new SqlParameter[] { };

            db database = new db();
            int count = database.ExecuteScalar(query, parameters); 


            if (count == 0)
            {
                allss.Text = "00";
            }
            else if (count < 10)
            {
                allss.Text = "0" + count.ToString();
            }
            else
            {
                allss.Text = count.ToString();
            }
        }

        private void ongoing()
        {
            // SQL query to count only the employees with a status of 'Running'
            string query = "SELECT COUNT(*) FROM Projects WHERE Status = 'Running'";
            SqlParameter[] parameters = new SqlParameter[] { }; // No parameters needed since the value is hardcoded

            db database = new db();
            int count = database.ExecuteScalar(query, parameters); // Assume this method correctly executes the query and returns the result

            // Format and display the count based on its value
            if (count == 0)
            {
                ongoings.Text = "00";
            }
            else if (count < 10)
            {
                ongoings.Text = "0" + count.ToString();
            }
            else
            {
                ongoings.Text = count.ToString();
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

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AddProject_Click(object sender, EventArgs e)
        {
            AdminProjectsSubActivity.AddProjects addProjects = new AdminProjectsSubActivity.AddProjects();
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(addProjects);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminProjectsSubActivity.AllProjects allProjects = new AdminProjectsSubActivity.AllProjects();
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(allProjects);
        }

        private void MainPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void seach_TextChanged(object sender, EventArgs e)
        {
            string searchdata = seach.Text.Trim(); // Trim to remove leading and trailing spaces

            if (string.IsNullOrEmpty(searchdata))
            {
                ClearFields();
            }
            else
            {
                var reader = new db().Select(string.IsNullOrWhiteSpace(searchdata) ? "SELECT * FROM Projects WHERE ProjectID LIKE '" + searchdata + "'" : "SELECT Projects.*, AssignProject.EmployeeID FROM Projects LEFT JOIN AssignProject ON Projects.ProjectID = AssignProject.ProjectID WHERE Projects.ProjectID LIKE '%" + searchdata + "%'");

                if (reader.Read())
                {
                    PID.Text = (string)reader["ProjectID"].ToString();
                    EID.Text = (string)reader["EmployeeID"].ToString();
                    status.Text = (string)reader["Status"];

                    DateTime deadline = (DateTime)reader["Deadline"];
                    TimeSpan remainingTime = deadline - DateTime.Now;

                    string remainingTimeString = "";

                    if (remainingTime.Days > 0)
                        remainingTimeString += $"{remainingTime.Days} D, ";

                    if (remainingTime.Hours > 0)
                        remainingTimeString += $"{remainingTime.Hours} hr, ";

                    if (remainingTime.Minutes > 0)
                        remainingTimeString += $"{remainingTime.Minutes} m";

                    remind.Text = remainingTimeString;
                }
                else
                {
                    ClearFields(); // Clear fields if no matching record found
                }

                reader.Close(); // Close the reader after use
            }
        }

        private void ClearFields()
        {
            PID.Text = "";
            EID.Text = "";
            status.Text = "";
            remind.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}

