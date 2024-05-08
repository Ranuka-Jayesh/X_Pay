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
            loadProjectCountsByDate();
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
            string incomeQuery = "SELECT SUM(Price) FROM Projects";
            // SQL query to sum the Amount column from Payments
            string paymentsQuery = "SELECT SUM(Amount) FROM Payments";

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
            // First, clear existing series and add a new one for the pie chart
            chart1.Series.Clear();
            var series = chart1.Series.Add("FinancialData");
            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;

            // Call the method to update totals
            getFinancialTotals();

            // Add data points to the series
            series.Points.AddXY("Total Income", Profit);
            series.Points.AddXY("Total Payments", totalPayments);

            // Displaying data labels with values
            series.IsValueShownAsLabel = true;

            // Refresh the chart to update visuals
            chart1.Invalidate();
        }

        private void chart2_Click(object sender, EventArgs e)
        {
            
        }
        private void loadProjectCountsByDate()
        {
            string query = @"
            SELECT CONVERT(DATE, AcceptDate) AS Date, COUNT(*) AS ProjectCount
            FROM Projects
            GROUP BY CONVERT(DATE, AcceptDate)
            ORDER BY Date;";

            db database = new db();
            SqlDataReader reader = database.Select(query);
            DataTable dt = new DataTable();

            if (reader != null)
            {
                dt.Load(reader);
                reader.Close(); // It's important to close the reader

                // Configure the chart for spline
                chart2.Series.Clear();
                Series series = chart2.Series.Add("ProjectsByDate");
                series.ChartType = SeriesChartType.Spline;

                foreach (DataRow row in dt.Rows)
                {
                    string date = Convert.ToDateTime(row["Date"]).ToShortDateString();
                    int count = Convert.ToInt32(row["ProjectCount"]);
                    series.Points.AddXY(date, count);
                }

                // Format the chart
                series.BorderWidth = 3;
                chart2.ChartAreas[0].AxisX.Interval = 1;
                chart2.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
                chart2.ChartAreas[0].AxisX.LabelStyle.Interval = 1;
                chart2.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
                chart2.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
                chart2.Invalidate(); // Refresh the chart
            }
            else
            {
                MessageBox.Show("Error loading data.");
            }
        }

    }
}
