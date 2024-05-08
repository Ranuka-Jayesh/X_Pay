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
using System.Windows.Forms.DataVisualization.Charting;

namespace X_Pay.AdminControls
{
    public partial class Incomes : UserControl
    {
        decimal totalIncome;
        decimal Profit;
        decimal totalPayments;
        public Incomes()
        {
            InitializeComponent();
            chartloading();
            loadProjectAndEmployeeCountsByDate();
            tot.Text = totalIncome.ToString();
            eptot.Text = totalPayments.ToString();
            profits.Text = Profit.ToString();
        }

        private void Incomes_Load(object sender, EventArgs e)
        {

        }
        private void chart1_Click(object sender, EventArgs e)
        {

        }
        private void getFinancialTotals()
        {
            // SQL query to sum the Price column from Projects
            string incomeQuery = "SELECT ISNULL(SUM(Price),0) FROM Projects";
            // SQL query to sum the Amount column from Payments
            string paymentsQuery = "SELECT ISNULL(SUM(Amount),0) FROM Payments";

            SqlParameter[] parameters = new SqlParameter[] { };
            db database = new db();

            // Execute the payments query
            object paymentsResult = database.ExecuteScalar(paymentsQuery, parameters);
            if (paymentsResult == DBNull.Value || paymentsResult == null)
            {
                totalPayments = 0;
            }
            else
            {
                totalPayments = Convert.ToDecimal(paymentsResult);
            }

            // Execute the income query
            object incomeResult = database.ExecuteScalar(incomeQuery, parameters);
            if (incomeResult == DBNull.Value || incomeResult == null)
            {
                totalIncome = 0;
            }
            else
            {
                totalIncome = Convert.ToDecimal(incomeResult);
                Profit = totalIncome - totalPayments;
            }

        }
        private void chartloading()
        {
            chart1.Series.Clear();
            var series = chart1.Series.Add("FinancialData");
            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;

            // Call the method to update totals
            getFinancialTotals();

            // Add data points to the series
            series.Points.AddXY("Total Profit", Profit);
            series.Points.AddXY("Total Payments", totalPayments);

            // Displaying data labels with values
            series.IsValueShownAsLabel = true;

            // Refresh the chart to update visuals
            chart1.Invalidate();
        }
        private void chart2_Click(object sender, EventArgs e)
        {

        }
        private void loadProjectAndEmployeeCountsByDate()
        {
            string projectQuery = @"
                SELECT CONVERT(DATE, AcceptDate) AS Date, COUNT(*) AS ProjectCount
                FROM Projects
                GROUP BY CONVERT(DATE, AcceptDate)
                ORDER BY Date;";

            string employeeQuery = @"
                SELECT CONVERT(DATE, RegisterDate) AS Date, COUNT(*) AS EmployeeCount
                FROM Employee
                GROUP BY CONVERT(DATE, RegisterDate)
                ORDER BY Date;";

            db database = new db();

            // Fetch project data
            SqlDataReader projectReader = database.Select(projectQuery);
            DataTable projectDt = new DataTable();

            if (projectReader != null)
            {
                projectDt.Load(projectReader);
                projectReader.Close(); // Close the reader

                // Clear any existing series on the chart
                chart2.Series.Clear();

                // Add a new series for project data
                Series projectSeries = chart2.Series.Add("ProjectsByDate");
                projectSeries.ChartType = SeriesChartType.Spline; // Use Spline chart type

                foreach (DataRow row in projectDt.Rows)
                {
                    string date = Convert.ToDateTime(row["Date"]).ToShortDateString();
                    int projectCount = Convert.ToInt32(row["ProjectCount"]);
                    projectSeries.Points.AddXY(date, projectCount);
                }
            }
            else
            {
                MessageBox.Show("Error loading project data.");
                return; // Exit method if project data loading fails
            }

            // Fetch employee data
            SqlDataReader employeeReader = database.Select(employeeQuery);
            DataTable employeeDt = new DataTable();

            if (employeeReader != null)
            {
                employeeDt.Load(employeeReader);
                employeeReader.Close(); // Close the reader

                // Add a new series for employee data
                Series employeeSeries = chart2.Series.Add("EmployeesByDate");
                employeeSeries.ChartType = SeriesChartType.Spline; // Use Spline chart type

                foreach (DataRow row in employeeDt.Rows)
                {
                    string date = Convert.ToDateTime(row["Date"]).ToShortDateString();
                    int employeeCount = Convert.ToInt32(row["EmployeeCount"]);
                    employeeSeries.Points.AddXY(date, employeeCount);
                }
            }
            else
            {
                MessageBox.Show("Error loading employee data.");
                return; // Exit method if employee data loading fails
            }

            // Format the chart
            foreach (Series series in chart2.Series)
            {
                series.BorderWidth = 3;
            }
            chart2.ChartAreas[0].AxisX.Interval = 1;
            chart2.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chart2.ChartAreas[0].AxisX.LabelStyle.Interval = 1;
            chart2.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            chart2.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;

            chart2.Invalidate(); // Refresh the chart
        }
    

    }
}
