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

namespace X_Pay.Employee.EmployeeControls
{
    public partial class EmpMyWallet : UserControl
    {
        int EmployeeID;
        public EmpMyWallet(int emp)
        {
            InitializeComponent();
            EmployeeID = emp;
            InitializeDataGridView();
            dataview.CellFormatting += new DataGridViewCellFormattingEventHandler(dataview_CellFormatting);
        }

        private void Register_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void MainPanel_Paint(object sender, PaintEventArgs e)
        {
            loadPendingPayments();
            incomes();
            tot();
            Payment();
        }
        private void loadPendingPayments()
        {
            DateTime endDate = DateTime.Now;
            DateTime startDate = endDate.AddMonths(-1);
            MonthLB3.Text = endDate.ToString("MMMM");

            // Updated query with parameter for EmployeeID
            string query = $"SELECT ISNULL(SUM(Amount), 0) FROM Payments WHERE Status = 'Pending' AND EmployeeID = {EmployeeID}";

            db database = new db();
            // Execute the scalar query
            int totalAmount = Convert.ToInt32(database.ExecuteScalar(query, new SqlParameter[] { }));

            // Update the label with the total amount formatted as currency
            Upcomings.Text = totalAmount.ToString();
        }
        private void incomes()
        {
            // Calculate start and end dates for the current month
            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            ml.Text = endDate.ToString("MMMM");

            // Construct the query to select payments for the current month
            string query = $"SELECT ISNULL(SUM(Amount), 0) FROM Payments WHERE PaymentDate >= '{startDate.ToString("yyyy-MM-dd")}' AND PaymentDate <= '{endDate.ToString("yyyy-MM-dd")}' AND Status = 'Paid' AND EmployeeID = {EmployeeID}";

            db database = new db();
            // Execute the scalar query
            int totalAmount = Convert.ToInt32(database.ExecuteScalar(query, new SqlParameter[] { }));

            // Update the label with the total amount formatted as currency
            inco.Text = totalAmount.ToString();
        }
        private void tot()
        {

            // Updated query with parameter for EmployeeID
            string query = $"SELECT ISNULL(SUM(Amount), 0) FROM Payments WHERE Status = 'Paid' AND EmployeeID = {EmployeeID}";

            db database = new db();
            // Execute the scalar query
            int totalAmount = Convert.ToInt32(database.ExecuteScalar(query, new SqlParameter[] { }));

            // Update the label with the total amount formatted as currency
            totinco.Text = totalAmount.ToString();
        }
        private void Payment()
        {
            string fl = filter.Text; // Get the selected filter from the dropdown
            string query;

            // Construct the query based on the selected filter
            if (fl == "All")
            {
                query = $"SELECT * FROM Payments WHERE EmployeeID = {EmployeeID}";
            }
            else
            {
                // Note: It's safer to use parameterized queries to prevent SQL injection
                query = $"SELECT * FROM Payments WHERE Status = '{fl}' AND EmployeeID = {EmployeeID}";
            }

            // Clear existing rows in the DataGridView
            dataview.Rows.Clear();

            // Execute the query and populate the DataGridView with the results
            var reader = new db().Select(query);
            while (reader.Read())
            {
                dataview.Rows.Add(reader["PaymentId"], reader["ProjectId"], reader["Amount"], reader["Status"], reader["PaymentDate"], reader["Receipt"]);
            }

        }
        private void InitializeDataGridView()
        {
            // Create a new button column
            DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
            btnColumn.HeaderText = "Download Slip";
            btnColumn.Text = "Download";
            btnColumn.UseColumnTextForButtonValue = true;  // This ensures the button shows "Download" text
            btnColumn.Name = "btnDownloadSlip";

            // Add the button column to the DataGridView
            dataview.Columns.Add(btnColumn);
            dataview.CellFormatting += new DataGridViewCellFormattingEventHandler(dataview_CellFormatting);
            dataview.CellContentClick += new DataGridViewCellEventHandler(dataview_CellContentClick);
        }
        private void filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            Payment();
        }
        private void dataview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is within valid rows and columns bounds
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataview.Columns["btnDownloadSlip"] != null)
            {
                // Ensure the click is on the 'Download Slip' button column
                if (e.ColumnIndex == dataview.Columns["btnDownloadSlip"].Index && e.RowIndex != -1)
                {
                    DataGridViewButtonCell buttonCell = dataview.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;

                    if (buttonCell != null && dataview.IsCurrentCellDirty)
                    {
                        // Commit the click only once
                        dataview.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }

                    var statusCell = dataview.Rows[e.RowIndex].Cells["Status"];
                    if (statusCell != null && statusCell.Value != null && statusCell.Value.ToString() == "Paid")
                    {
                        var paymentIdCell = dataview.Rows[e.RowIndex].Cells["PaymentId"];
                        if (paymentIdCell != null && paymentIdCell.Value != null)
                        {
                            string paymentId = paymentIdCell.Value.ToString();
                            DownloadPaymentSlip(paymentId);
                        }                      
                    }
                    else
                    {
                        MessageBox.Show("This slip is not available for download.");
                    }
                }
            }
        }
        private void dataview_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // First, check if the DataGridView and its necessary columns are properly initialized
            if (dataview.Columns["btnDownloadSlip"] == null || dataview.Rows == null)
                return;

            // Ensure the event is for the correct column index
            if (e.ColumnIndex == dataview.Columns["btnDownloadSlip"].Index && e.RowIndex >= 0 && e.RowIndex < dataview.Rows.Count)
            {
                // Safely access the Status cell value
                var statusCell = dataview.Rows[e.RowIndex].Cells["Status"];
                if (statusCell != null && statusCell.Value != null)
                {
                    var status = statusCell.Value.ToString();
                    if (status == "Paid")
                    {
                        e.CellStyle.BackColor = Color.LightGreen;
                        e.CellStyle.ForeColor = Color.Black;
                        e.Value = "Download";
                    }
                }
            }
        }
        private void DownloadPaymentSlip(string paymentId)
        {
            // First, locate the correct row based on the payment ID
            DataGridViewRow row = dataview.Rows
                .Cast<DataGridViewRow>()
                .FirstOrDefault(r => r.Cells["PaymentId"].Value.ToString() == paymentId);

            if (row != null)
            {
                // Check if the receipt path exists and is not null or empty
                var receiptPath = row.Cells["Receipt"].Value;
                if (receiptPath != null && !string.IsNullOrEmpty(receiptPath.ToString()))
                {
                    try
                    {
                        // Open the file with the default application
                        System.Diagnostics.Process.Start("explorer.exe", receiptPath.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error opening file: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("No slip available for this payment.");
                }
            }
            else
            {
                MessageBox.Show("Payment record not found.");
            }
        }

    }
}
