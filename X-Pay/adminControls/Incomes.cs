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
        public Incomes()
        {
            InitializeComponent();
        }

        private void Incomes_Load(object sender, EventArgs e)
        {
            db db = new db();
            string query = "SELECT SUM(price) AS TotalIncome FROM Projects";
            SqlParameter[] parameters = new SqlParameter[0]; // No parameters for this query
            int totalIncome = db.ExecuteScalar(query, parameters); // Retrieve the total income

            // Setup the chart
            SetupIncomeChart(totalIncome);
        }

        private void SetupIncomeChart(int totalIncome)
        {
            IncomeChart.Series.Clear();
            var series = IncomeChart.Series.Add("Total Income");
            series.ChartType = SeriesChartType.Column;

            // Add the total income to the chart
            series.Points.Add(totalIncome);

            IncomeChart.ChartAreas[0].AxisX.Title = "Total";
            IncomeChart.ChartAreas[0].AxisY.Title = "Income";
        }


    }
}
