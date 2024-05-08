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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using System.Diagnostics.Contracts;

namespace X_Pay.Employee.EmployeeControls.MyTaskSubActivities
{
    public partial class DeliveredProjects : UserControl
    {
        private string selectedFilePath;
        private int EmployeeID;
        public DeliveredProjects(int emp)
        {
            InitializeComponent();
            EmployeeID = emp;
        }

        private void DeliveredProjects_Load(object sender, EventArgs e)
        {
            LoadProjectID();

            var reader = new db().Select($"SELECT * FROM ProjectDelivery WHERE EmployeeID = {EmployeeID}");
            dataviwe.Rows.Clear();
            while (reader.Read())
            {
                dataviwe.Rows.Add(reader["Id"], reader["ProjectID"], reader["EmployeeID"], reader["FilePath"], reader["Status"], reader["DeliveredDate"]);
            }
        }

        private void LoadProjectID()
        {
            try
            {
                db db = new db();
                string query = $"SELECT ProjectID FROM AssignProject WHERE EmployeeID = {EmployeeID};";

                using (SqlDataReader reader = db.Select(query))
                {
                    DataTable dt = new DataTable();
                    projectID.DisplayMember = "ProjectID";
                    dt.Load(reader);
                    projectID.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load Project data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProjectFiles_Click(object sender, EventArgs e)
        {
            // Show the OpenFileDialog
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file path
                selectedFilePath = openFileDialog1.FileName;

                // Extract the file name from the file path
                string fileName = Path.GetFileName(selectedFilePath);
                label2.Text = fileName;

                // Specify the fixed path to the 'Documents' directory
                string documentsPath = @"C:\Users\Ranuka Jayesh\Desktop\Xpay\NewGit\X-Pay\Delivered";

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
                    // Update the stored file path to the new path
                    selectedFilePath = destinationFilePath;

                    // Optionally, show a message box (comment out if not needed)
                    MessageBox.Show("File selected and saved to: " + destinationFilePath, "File Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while copying the file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Projectid = projectID.Text;
            string spnote = SpecialNote.Text;
            DateTime DeliveredDate = DateTime.Now;
            string Status = "Delivered";

            if (string.IsNullOrEmpty(selectedFilePath))
            {
                MessageBox.Show("Please select a file before submitting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Stop further execution
            }

            string query = "INSERT INTO ProjectDelivery  VALUES ('" + Projectid + "', '" +EmployeeID+ "' ,'" + selectedFilePath+ "' , '" + spnote + "', '" + DeliveredDate + "', '" + Status + "')";
            string Query = $"DELETE FROM AssignProject WHERE ProjectID =" + projectID.Text;

            db DB = new db();
            try
            {
                DB.Execute(query);
                DB.Execute(Query);
                MessageBox.Show("Delivered successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SpecialNote.Text = "";
                selectedFilePath = "";
                label2.Text = "No file selected";

                LoadProjectID();

                var reader = new db().Select($"SELECT * FROM ProjectDelivery WHERE EmployeeID = {EmployeeID}");
                dataviwe.Rows.Clear();
                while (reader.Read())
                {
                    dataviwe.Rows.Add(reader["Id"], reader["ProjectID"], reader["EmployeeID"], reader["FilePath"], reader["Status"], reader["DeliveredDate"]);
                }

            }
            catch
            {

            }

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void Epayment_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
