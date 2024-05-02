using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace X_Pay.AdminControls.AdminProjectsSubActivity
{
    public partial class AddProjects : UserControl
    {
        private string selectedFilePath;
        public AddProjects()
        {
            InitializeComponent();
            openFileDialog.Filter = "PDF files (*.pdf)|*.pdf|Word files (*.doc;*.docx)|*.doc;*.docx";
            openFileDialog.Title = "Select a PDF or Word Document";
            openFileDialog.Multiselect = false; // Set true if you want to allow multiple selections
        }

        // Define an OpenFileDialog field at the class level
        private OpenFileDialog openFileDialog = new OpenFileDialog();


        private void Form1_Load(object sender, EventArgs e)
        {
            
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
                        projectType.DisplayMember = "CATName"; // Column name for display in the ComboBox
                        projectType.DataSource = dt;          // Set the DataTable as the data source for the ComboBox
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

        private void ProjectType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddProjects_Load(object sender, EventArgs e)
        {
            LoadCategoriesIntoComboBox();
            LoadSubjectsIntoComboBox();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Show the OpenFileDialog
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file path
                selectedFilePath = openFileDialog.FileName;

                // Extract the file name from the file path
                string fileName = Path.GetFileName(selectedFilePath);
                label.Text = fileName;

                // Optionally, show a message box (comment out if not needed)
                MessageBox.Show("File selected: " + fileName, "File Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        
        //reguler expresiona
        private bool IsValidContact(string contact)
        {
            // Check if contact number starts with '0' and has exactly 10 digits
            return contact.StartsWith("0") && contact.Length == 10 && contact.All(char.IsDigit);
        }
        private bool IsValidString(string input)
        {
            return input.All(char.IsLetter);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            db DB = new db();
            try
            {
                string Client = ClientName.Text;
                string organization = orgname.Text;
                string project = projectType.Text;
                string subject = subjectType.Text;
                int prices;
                if (!int.TryParse(Price.Text, out prices))
                {
                    MessageBox.Show("Please enter a valid price.", "Input Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                DateTime Deadline = deadline.Value;
                string notes = additionalnote.Text;
                string contact = phone.Text;
                DateTime acceptionDate = DateTime.Now;

                // Validate data
                if (!IsValidString(Client) || Client.Length <= 4)
                {
                    MessageBox.Show("Client name must be only letters and more than 4 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!IsValidString(organization) || organization.Length <= 4)
                {
                    MessageBox.Show("Organization name must be only letters and more than 4 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (prices <= 100)
                {
                    MessageBox.Show("Price must be more than 100.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validate contact number
                if (!IsValidContact(contact))
                {
                    MessageBox.Show("Invalid contact number. Please ensure the contact number has 10 digits.", "Contact Number Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // SQL Query
                string query = "INSERT INTO Projects  VALUES ('" + Client + "', '" + organization + "', '" + project + "', '" + subject + "', '" + prices + "', '" + Deadline + "', '" + notes + "', '" + selectedFilePath + "', '" + contact + "', '" + acceptionDate + "')";
                DB.Execute(query);
                MessageBox.Show("Data inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();

                var result = MessageBox.Show("Do you want to download a receipt?", "Receipt", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                    saveFileDialog.Title = "Save receipt";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        CreateAndSaveReceipt(saveFileDialog.FileName, Client, organization, project, prices, Deadline, acceptionDate);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void CreateAndSaveReceipt(string filePath, string client, string organization, string project, int price, DateTime deadline, DateTime acceptionDate)
        {
            using (var document = new iTextSharp.text.Document())
            {
                iTextSharp.text.pdf.PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                document.Open();

                document.Add(new iTextSharp.text.Paragraph("Receipt"));
                document.Add(new iTextSharp.text.Paragraph($"Client: {client}"));
                document.Add(new iTextSharp.text.Paragraph($"Organization: {organization}"));
                document.Add(new iTextSharp.text.Paragraph($"Project: {project}"));
                document.Add(new iTextSharp.text.Paragraph($"Price: {price}"));
                document.Add(new iTextSharp.text.Paragraph($"Deadline: {deadline.ToShortDateString()}"));
                document.Add(new iTextSharp.text.Paragraph($"Date of Acceptance: {acceptionDate.ToShortDateString()}"));

                document.Close();
            }
        }


        private void ClearForm()
        {

            ClientName.Text = string.Empty;        
            orgname.Text = string.Empty;           
            projectType.SelectedIndex = -1;        
            subjectType.SelectedIndex = -1;        
            Price.Text = string.Empty;             
            deadline.Value = DateTime.Now;        
            additionalnote.Text = string.Empty;
            phone.Text = string.Empty;  

            label.Text = "No file selected"; 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
    }
}
