using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace X_Pay.Employee.EmployeeControls.MyTaskSubActivities
{
    public partial class OngoingProjects : UserControl
    {
        private int EmployeeID;
        private string filePath;
        public OngoingProjects(int employeeID)
        {
            InitializeComponent();
            EmployeeID = employeeID;
        }

        private void OngoingProjects_Load(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView();
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            DataGridViewButtonColumn btnDownload = new DataGridViewButtonColumn();
            btnDownload.Name = "btnDownload";
            btnDownload.HeaderText = "Download";
            btnDownload.Text = "Download";
            btnDownload.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(btnDownload);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["btnDownload"].Index && e.RowIndex >= 0)
            {
                string Document = dataGridView1.Rows[e.RowIndex].Cells["Document"].Value.ToString();
                if (!string.IsNullOrEmpty(Document))
                {
                    System.Diagnostics.Process.Start("explorer", Document);
                }
                else
                {
                    MessageBox.Show("No file available for download.", "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        private void LoadDataIntoDataGridView()
        {
            db DB = new db();
            string query = $@"
        SELECT 
        ap.ProjectID, 
        p.ProjectType, 
        p.SubjectType as ProjectSubject,
        ap.AssignDate, 
        ap.SpecialNote, 
        p.Deadline, 
        p.Price, 
        ap.FilePath
        FROM AssignProject ap
        JOIN Projects p ON ap.ProjectID = p.ProjectID
        WHERE ap.EmployeeID = {EmployeeID};";

            var reader = DB.Select(query);
            dataGridView1.Rows.Clear();
            while (reader != null && reader.Read())
            {
                dataGridView1.Rows.Add(
                    reader["ProjectID"].ToString(), // Assuming a corresponding column for each of these exists
                    reader["ProjectType"].ToString(),
                    reader["ProjectSubject"].ToString(),
                    reader["Deadline"].ToString(),
                    reader["SpecialNote"].ToString(),                   
                    reader["AssignDate"].ToString(),
                    reader["Price"].ToString(),
                    reader["FilePath"].ToString() // This could be a path to handle differently if it's for download functionality
                );
            }
            if (reader != null)
                reader.Close(); // Important to close the reader to free resources
        }


    }
}
