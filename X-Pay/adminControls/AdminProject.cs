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
    }
}
