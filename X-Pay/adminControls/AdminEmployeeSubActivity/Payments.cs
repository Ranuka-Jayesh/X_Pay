using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using X_Pay.Employee.EmployeeControls.MyWalletSubActivities;

namespace X_Pay.AdminControls.AdminEmployeeSubActivity
{
    public partial class Payments : UserControl
    {
        private string selectedFilePath;
        public Payments()
        {
            InitializeComponent();
        }

        private void Payments_Load(object sender, EventArgs e)
        {
            LoadEmployeeID();
            LoadPayIDS();
        }
        private void LoadEmployeeID()
        {
            try
            {
                db db = new db();
                {
                    // Using DISTINCT to ensure only unique EmployeeIDs are selected
                    using (SqlDataReader reader = db.Select("SELECT DISTINCT EmployeeID FROM Payments WHERE Status = 'Pending' "))
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);  // Load data from reader into DataTable

                        // Setting up the ComboBox
                        employeeID.DisplayMember = "EmployeeID";  // Set the field to display
                        employeeID.DataSource = dt;  // Set the data source to the DataTable
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load Employee data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPayIDS()
        {
            db db = new db();
            using (SqlDataReader reader = db.Select($"SELECT PaymentId FROM Payments WHERE EmployeeID = '{employeeID.Text}'"))
            {
                Console.WriteLine($"Query: SELECT PaymentId FROM Payments WHERE EmployeeID = '{employeeID.Text}'"); // Log the query

                DataTable dt = new DataTable();
                payid.DisplayMember = "PaymentId";
                dt.Load(reader);
                payid.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No data found for Employee ID: " + employeeID.Text);  // Alert if no data
                }
            }
        }

        private void employeeID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = $"SELECT SUM(Amount) FROM Payments WHERE EmployeeId = {employeeID.Text}";
            string query2 = $"SELECT FirstName , LastName FROM Employee WHERE EmployeeId = {employeeID.Text}";
            SqlParameter[] parameters = new SqlParameter[] { }; // No parameters needed for a simple SUM query

            db database = new db();
            object result = database.ExecuteScalar(query, parameters);  // ExecuteScalar should return the first column of the first row in the result set

            // Check for DB null values
            if (result == DBNull.Value || result == null)
            {
                tot.Text = "00";
            }
            else
            {
                decimal totalIncome = Convert.ToDecimal(result); // Convert the result to decimal
                tot.Text = totalIncome.ToString("N2"); // Format the number as needed, e.g., "N2" for two decimal places
            }
            // E
            // xecute the second query to get the employee's name
            using (SqlDataReader reader = database.Select(query2)) // Assuming Select method is adapted to handle parameters
            {
                if (reader != null && reader.Read()) // Ensure there is data and read the first record
                {
                    string firstName = reader["FirstName"].ToString();
                    string lastName = reader["LastName"].ToString();
                    ENme.Text = $"{firstName} {lastName}";
                }
                else
                {
                    ENme.Text = "Name not found"; // Handle case where no data is returned
                }
            }

            LoadPayIDS();
        }

        private void payid_SelectedIndexChanged(object sender, EventArgs e)
        {
            db myDb = new db();
            string query = $"SELECT ProjectId, Amount, Status FROM Payments WHERE PaymentId = {payid.Text}";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@PaymentId", payid.Text)
            };

            try
            {
                using (SqlDataReader reader = myDb.Select(query))
                {
                    if (reader != null && reader.Read())
                    {
                        PID.Text = reader["ProjectId"].ToString();
                        AMT.Text = reader["Amount"].ToString();
                        ST.Text = reader["Status"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Create and configure an OpenFileDialog
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select an Image";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

                // Show the OpenFileDialog
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected file path
                    selectedFilePath = openFileDialog.FileName;

                    // Extract the file name from the file path
                    string fileName = Path.GetFileName(selectedFilePath);
                    label3.Text = fileName;  // Assuming 'label' is the Label control where you want to display the file name

                    // Specify the fixed path to the 'Documents' directory on your Desktop
                    string documentsPath = @"C:\Users\Ranuka Jayesh\Desktop\Xpay\NewGit\X-Pay\slip";

                    // Check if the 'Documents' directory exists, if not create it
                    if (!Directory.Exists(documentsPath))
                    {
                        Directory.CreateDirectory(documentsPath);
                    }

                    // Define the path to save the file inside the 'Documents' directory
                    string destinationFilePath = Path.Combine(documentsPath, fileName);

                    // Copy the file to the 'Documents' directory
                    try
                    {
                        File.Copy(selectedFilePath, destinationFilePath, true);
                        // Optionally, update the PictureBox's image
                        pictureBox1.Image = Image.FromFile(destinationFilePath);

                        // Show a confirmation message box
                        MessageBox.Show("Image selected and saved to: " + destinationFilePath, "Image Uploaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while copying the file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Ensure a file path is selected
                if (string.IsNullOrEmpty(selectedFilePath))
                {
                    MessageBox.Show("Please select a file first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string paymentId = PID.Text;
                DateTime today = DateTime.Now; 
                                               
                string query = "UPDATE Payments SET Receipt = '" + selectedFilePath + "', Status = 'Paid', PaymentDate = '" + today + "' WHERE PaymentId = '" + paymentId + "'"; 

                db DB = new db();
                DB.Execute(query);

                MessageBox.Show("Payment record updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
