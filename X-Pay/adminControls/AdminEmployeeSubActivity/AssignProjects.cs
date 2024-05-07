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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace X_Pay.AdminControls.AdminEmployeeSubActivity
{
    public partial class AssignProjects : UserControl
    {
        public AssignProjects()
        {
            InitializeComponent();
        }

        private void AssignProjects_Load(object sender, EventArgs e)
        {
           ShowAssignProjects();
           LoadEmployeeID();
           LoadProjectID();
        }

        private void ShowAssignProjects()
        {
            try
            {
                // Clear existing rows in the DataGridView
                dataview.Rows.Clear();

                // Execute SQL query to select data from AssignProject table
                var reader = new db().Select("SELECT * FROM AssignProject");

                // Loop through the data reader and add rows to the DataGridView
                while (reader.Read())
                {
                    // Determine if FilePath is not null to display "HAVE" or "DON'T HAVE"
                    string filePathValue = reader["FilePath"] != DBNull.Value ? "Privided" : "Not Privided";
                    string specialNoteValue = reader["SpecialNote"] != DBNull.Value ? reader["SpecialNote"].ToString() : "NaN";

                    // Add a row to the DataGridView with data from the reader
                    dataview.Rows.Add(
                        reader["Id"],
                        reader["EmployeeID"],
                        reader["ProjectID"],
                        reader["Epayment"],
                        filePathValue,
                        specialNoteValue,
                        reader["AssignDate"]
                    );
                }

                // Close the data reader
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadProjectID()
        {
            try
            {
                db db = new db();
                {
                    using (SqlDataReader reader = db.Select("SELECT ProjectID FROM Projects WHERE Status = 'Pending'"))
                    {
                        DataTable dt = new DataTable();
                        projectID.DisplayMember = "ProjectID";
                        dt.Load(reader);
                        projectID.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load Project data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadEmployeeID()
        {
            try
            {
                db db = new db();
                {
                    using (SqlDataReader reader = db.Select("SELECT EmployeeID FROM Employee"))
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        employeeID.DisplayMember = "EmployeeID";
                        employeeID.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load Employee data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void employeeID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (employeeID.Text != null)
            {
                try
                {
                    string selectedEmployeeID = employeeID.Text;
                    db db = new db();
                    using (SqlDataReader reader = db.Select($"SELECT FirstName, LastName, Contact, Skills FROM Employee WHERE EmployeeID = '{selectedEmployeeID}'"))
                    {
                        if (reader.Read())
                        {
                            ENme.Text = (reader["FirstName"]?.ToString() ?? "") + " " + (reader["LastName"]?.ToString() ?? "");
                            Contact.Text = reader["Contact"]?.ToString() ?? "N/A";
                            Qulification.Text = reader["Skills"]?.ToString() ?? "N/A";
                        }
                        else
                        {
                            MessageBox.Show("No data found for this Employee.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to retrieve project details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string projectFilePath = "";
        private void projectID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (projectID.Text != null)
            {
                try
                {
                    string selectedProjectID = projectID.Text;
                    db db = new db();
                    using (SqlDataReader reader = db.Select($"SELECT ClientName, Organization, ProjectType, SubjectType, Price, Deadline , FilePath FROM Projects WHERE ProjectID = '{selectedProjectID}'"))
                    {
                        if (reader.Read())
                        {
                            CName.Text = reader["ClientName"]?.ToString() ?? "N/A";
                            org.Text = reader["Organization"]?.ToString() ?? "N/A";
                            PType.Text = reader["ProjectType"]?.ToString() ?? "N/A";
                            Subject.Text = reader["SubjectType"]?.ToString() ?? "N/A";
                            PPrice.Text = "Rs." + reader["Price"]?.ToString() ?? "N/A";
                            Dline.Text = reader["Deadline"]?.ToString() ?? "N/A";

                            projectFilePath = reader["FilePath"]?.ToString() ?? "";
                        }
                        else
                        {
                            MessageBox.Show("No data found for this project.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to retrieve project details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the current date and time
                DateTime currentDate = DateTime.Now;

                // Retrieve values from labels and controls
                string employeeIDValue = employeeID.Text;
                string projectIDValue = projectID.Text;
                string epaymentValue = Epayment.Text;
                string specialNoteValue = SpecialNote.Text;

                string filePathValue = projectFilePath;

                // Check if Epayment is not null and greater than 100
                if (string.IsNullOrWhiteSpace(epaymentValue) || !decimal.TryParse(epaymentValue, out decimal epayment) || epayment <= 100)
                {
                    MessageBox.Show("Employee Payment Should Be More than Rs.100", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                // Create SQL query for insertion into AssignProject table
                string insertQuery = $"INSERT INTO AssignProject (EmployeeID, ProjectID, Epayment, SpecialNote, FilePath, AssignDate) VALUES ('{employeeIDValue}', '{projectIDValue}', '{epaymentValue}', '{specialNoteValue}', '{filePathValue}', '{currentDate}')";

                string query = "INSERT INTO Payments (EmployeeID, Amount, Status, PaymentDate , ProjectId) VALUES ('" + employeeID.Text + "', '" + Epayment.Text + "', 'Pending', '" + currentDate + "' ,'"+ projectIDValue + "')";

                // Execute the insert query for AssignProject table
                db db = new db();
                db.Execute(insertQuery);
                db.Execute(query);

                // Update the Status column in the Project table to 'Running'
                string updateStatusQuery = $"UPDATE Projects SET Status = 'Running' WHERE ProjectID = '{projectIDValue}'";
                db.Execute(updateStatusQuery);

                // Display a success message
                MessageBox.Show("Project Assigned Successfuly !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowAssignProjects();
                LoadEmployeeID();
                LoadProjectID();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to insert data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }


}
