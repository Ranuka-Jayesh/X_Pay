namespace X_Pay.AdminControls
{
    partial class Incomes
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label15 = new System.Windows.Forms.Label();
            this.IncomeChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.IncomeChart)).BeginInit();
            this.SuspendLayout();
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label15.Location = new System.Drawing.Point(23, 32);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(331, 32);
            this.label15.TabIndex = 38;
            this.label15.Text = "DASHBOARD / INCOMES";
            // 
            // IncomeChart
            // 
            chartArea1.Name = "ChartArea1";
            this.IncomeChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.IncomeChart.Legends.Add(legend1);
            this.IncomeChart.Location = new System.Drawing.Point(29, 128);
            this.IncomeChart.Name = "IncomeChart";
            this.IncomeChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Excel;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.IncomeChart.Series.Add(series1);
            this.IncomeChart.Size = new System.Drawing.Size(509, 300);
            this.IncomeChart.TabIndex = 39;
            this.IncomeChart.Text = "chart1";
            // 
            // Incomes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(18)))), ((int)(((byte)(60)))));
            this.Controls.Add(this.IncomeChart);
            this.Controls.Add(this.label15);
            this.Name = "Incomes";
            this.Size = new System.Drawing.Size(978, 487);
            this.Load += new System.EventHandler(this.Incomes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.IncomeChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DataVisualization.Charting.Chart IncomeChart;
    }
}
