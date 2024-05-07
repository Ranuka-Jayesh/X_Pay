using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;


namespace X_Pay.AdminControls.AdminProjectsSubActivity
{
    public partial class AllProjects : UserControl
    {
        public AllProjects()
        {
            InitializeComponent();
        }

        private void AllProjects_Load(object sender, EventArgs e)
        {
            string fl = filter.Text;  // Get the filter text
            string query;

            // Check if the filter is set to 'All', or specific statuses like 'Pending', 'Running', or 'Delivered'
            if (fl == "All")
            {
                query = "SELECT * FROM Projects"; // Query to select all records
            }
            else
            {
                query = "SELECT * FROM Projects WHERE Status = '" + fl + "'"; // Query to select records based on the filter
            }

            var reader = new db().Select(query);  // Execute the query using your method
            dataviwe.Rows.Clear();  // Clear the DataGridView

            while (reader.Read())
            {
                // Add rows to DataGridView from the query result
                dataviwe.Rows.Add(reader["ProjectID"], reader["ClientName"], reader["Organization"], reader["ProjectType"],
                                  reader["SubjectType"], reader["Contact"], reader["Deadline"], reader["Price"],
                                  reader["Status"], reader["AcceptDate"]);
            }

            LoadCategoriesIntoComboBox();
            LoadSubjectsIntoComboBox();
        }

        private void LoadCategoriesIntoComboBox()
        {
            try
            {
                db db = new db();
                {
                    // Assuming 'Select' executes SQL and returns a SqlDataReader
                    using (SqlDataReader reader = db.Select("SELECT CATName FROM Cat"))
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader); // Load data into DataTable from reader

                        // Bind the DataTable to the ComboBox
                        projectTypes.DisplayMember = "CATName"; // Column name for display in the ComboBox
                        projectTypes.DataSource = dt;          // Set the DataTable as the data source for the ComboBox
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load category data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadSubjectsIntoComboBox()
        {
            try
            {
                db db = new db();
                {
                    // Assuming 'Select' executes SQL and returns a SqlDataReader
                    using (SqlDataReader reader = db.Select("SELECT SUBName FROM sub"))
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader); // Load data into DataTable from reader

                        // Bind the DataTable to the ComboBox
                        subjectType.DisplayMember = "SUBName"; // Column name for display in the ComboBox
                        subjectType.DataSource = dt;          // Set the DataTable as the data source for the ComboBox
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load category data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PID_TextChanged(object sender, EventArgs e)
        {
            string searchdata = PID.Text;

            if (searchdata == "")
            {
                ClearForm();
            }
            else
            {
                var reader = new db().Select(string.IsNullOrWhiteSpace(searchdata) ? "SELECT * FROM Projects" : "SELECT * FROM Projects WHERE ProjectID LIKE '" + searchdata + "'");
                while (reader.Read())
                {
                    PID.Text = reader["ProjectID"].ToString();
                    CName.Text = (string)reader["ClientName"];
                    Orgz.Text = (string)reader["Organization"];
                    Contacts.Text = (string)reader["Organization"];
                    projectTypes.Text = (string)reader["ProjectType"];
                    subjectType.Text = (string)reader["SubjectType"];
                    DDline.Value = Convert.ToDateTime(reader["Deadline"]);
                    Tot.Text = reader["Price"].ToString();
                    Contacts.Text = (string)reader["contact"];

                    if (reader["AdditionalNotes"] != DBNull.Value)
                    {
                        note.Text = (string)reader["AdditionalNotes"];
                    }
                    
                    if (reader["Status"] != DBNull.Value)
                    {
                        status.Text = reader["Status"].ToString(); 
                    }
                    else
                    {
                        status.Text = "No Status"; 
                    }

                }
            }

        }

        private void ClearForm()
        {
            PID.Text = string.Empty;
            CName.Text = string.Empty;
            Orgz.Text = string.Empty;
            Contacts.Text = string.Empty;
            DDline.Value=DateTime.Now;
            ln.Text = string.Empty;
            Tot.Text = string.Empty;
            Contacts.Text = string.Empty;
            status.Text = string.Empty;
            note.Text= string.Empty;
        }

        private void label14_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            // Check if the employee ID field is empty
            if (string.IsNullOrWhiteSpace(PID.Text))
            {
                MessageBox.Show("Please enter the Project ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the method without executing the update query
            }

            try
            {
                if (status.Text == "Pending")
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to update the status of this project to Pending?\nThis will remove the assigned person from the project.", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                }

                string query = "UPDATE Projects SET " +
                "ClientName = '" + (CName.Text) + "', " +
                "Organization = '" + (Orgz.Text) + "', " +
                "ProjectType = '" + (projectTypes.Text) + "', " +
                "SubjectType = '" + (subjectType.Text) + "', " +
                "Price = " + (Tot.Text) + ", " +
                "Deadline = '" + Convert.ToDateTime(DDline.Text).ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                "AdditionalNotes = '" + (note.Text) + "', " +
                "contact = '" + (Contacts.Text) + "', " +
                "Status = '" + (status.Text) + "' " +
                "WHERE ProjectID = '"+ PID.Text + "'"; 

                db DB = new db();
                if (status.Text == "Pending")
                {
                    string deleteQuery = "DELETE FROM AssignProject WHERE ProjectID = '" + PID.Text + "'";
                    DB.Execute(deleteQuery);
                }

                // Execute the update query
                DB.Execute(query);
                // Show success message box
                MessageBox.Show("Data updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                // Select data to datagridview and display
                var reader = new db().Select("SELECT * FROM Projects");
                dataviwe.Rows.Clear();
                while (reader.Read())
                {
                    dataviwe.Rows.Add(reader["ProjectID"], reader["ClientName"], reader["Organization"], reader["ProjectType"], reader["SubjectType"], reader["contact"], reader["Deadline"], reader["Price"], reader["Status"], reader["AcceptDate"]);
                }

            }
            catch (Exception ex)
            {
                // Log exception details here, e.g., to a file or a database
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void label13_Click(object sender, EventArgs e)
        {
            // Check if the Project ID field is empty
            if (string.IsNullOrWhiteSpace(PID.Text))
            {
                MessageBox.Show("Please enter the Project ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Prompt the user for confirmation before deleting the project
                DialogResult result = MessageBox.Show("Are you sure you want to delete this project?\nDeleting this project will also remove the assignment.", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    // Construct the SQL query for deleting the record
                    string Query = "DELETE FROM Projects WHERE ProjectID = " + PID.Text;
                    string deleteQuery = "DELETE FROM AssignProject WHERE ProjectID = '" + PID.Text + "'";
                    

                    // Create a new instance of the database access class
                    db DB = new db();

                    // Execute the delete query
                    DB.Execute(Query);
                    DB.Execute(deleteQuery);

                    // Show success message box
                    MessageBox.Show("Data Delete successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearForm();

                    // Select data to datagridview and display
                    var reader = new db().Select("SELECT * FROM Projects");
                    dataviwe.Rows.Clear();
                    while (reader.Read())
                    {
                        dataviwe.Rows.Add(reader["ProjectID"], reader["ClientName"], reader["Organization"], reader["ProjectType"], reader["SubjectType"], reader["contact"], reader["Deadline"], reader["Price"], reader["Status"], reader["AcceptDate"]);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception details here, e.g., to a file or a database
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PID.Text))
            {
                MessageBox.Show("Please enter a Project ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                db database = new db();
                var reader = database.Select($"SELECT FilePath FROM Projects WHERE ProjectID = {PID.Text}");
                if (reader.Read())
                {
                    string path = reader["FilePath"].ToString();
                    if (!string.IsNullOrWhiteSpace(path))
                    {
                        System.Diagnostics.Process.Start("explorer.exe", path);
                    }
                    else
                    {
                        MessageBox.Show("No file path associated with this project.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No record found for the given Project ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open file path: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void status_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string fl = filter.Text;  // Get the filter text
            string query;

            // Check if the filter is set to 'All', or specific statuses like 'Pending', 'Running', or 'Delivered'
            if (fl == "All")
            {
                query = "SELECT * FROM Projects"; // Query to select all records
            }
            else
            {
                query = "SELECT * FROM Projects WHERE Status = '" + fl + "'"; // Query to select records based on the filter
            }

            var reader = new db().Select(query);  // Execute the query using your method
            dataviwe.Rows.Clear();  // Clear the DataGridView

            while (reader.Read())
            {
                // Add rows to DataGridView from the query result
                dataviwe.Rows.Add(reader["ProjectID"], reader["ClientName"], reader["Organization"], reader["ProjectType"],
                                  reader["SubjectType"], reader["Contact"], reader["Deadline"], reader["Price"],
                                  reader["Status"], reader["AcceptDate"]);
            }
        }
    }
}
