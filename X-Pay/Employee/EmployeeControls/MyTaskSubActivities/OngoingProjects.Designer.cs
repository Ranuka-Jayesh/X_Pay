namespace X_Pay.Employee.EmployeeControls.MyTaskSubActivities
{
    partial class OngoingProjects
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
            this.label13 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ProjectID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProjectType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProjectSubject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeadLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SpecialNote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AssignedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Document = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label13.Location = new System.Drawing.Point(23, 32);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(556, 32);
            this.label13.TabIndex = 41;
            this.label13.Text = "DASHBOARD / MyTask / Ongoing Projects";
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(75)))), ((int)(((byte)(95)))));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProjectID,
            this.ProjectType,
            this.ProjectSubject,
            this.DeadLine,
            this.SpecialNote,
            this.AssignedDate,
            this.Price,
            this.Document});
            this.dataGridView1.Location = new System.Drawing.Point(16, 93);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(946, 373);
            this.dataGridView1.TabIndex = 43;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // ProjectID
            // 
            this.ProjectID.HeaderText = "ProjectID";
            this.ProjectID.Name = "ProjectID";
            // 
            // ProjectType
            // 
            this.ProjectType.HeaderText = "Project Type";
            this.ProjectType.Name = "ProjectType";
            // 
            // ProjectSubject
            // 
            this.ProjectSubject.HeaderText = "Project Subject";
            this.ProjectSubject.Name = "ProjectSubject";
            // 
            // DeadLine
            // 
            this.DeadLine.HeaderText = "Dead Line";
            this.DeadLine.Name = "DeadLine";
            // 
            // SpecialNote
            // 
            this.SpecialNote.HeaderText = "Special Note";
            this.SpecialNote.Name = "SpecialNote";
            // 
            // AssignedDate
            // 
            this.AssignedDate.HeaderText = "Assigned Date";
            this.AssignedDate.Name = "AssignedDate";
            // 
            // Price
            // 
            this.Price.HeaderText = "Price";
            this.Price.Name = "Price";
            // 
            // Document
            // 
            this.Document.HeaderText = "Document";
            this.Document.Name = "Document";
            // 
            // OngoingProjects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(75)))), ((int)(((byte)(95)))));
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label13);
            this.Name = "OngoingProjects";
            this.Size = new System.Drawing.Size(978, 487);
            this.Load += new System.EventHandler(this.OngoingProjects_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjectID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjectType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjectSubject;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeadLine;
        private System.Windows.Forms.DataGridViewTextBoxColumn SpecialNote;
        private System.Windows.Forms.DataGridViewTextBoxColumn AssignedDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Document;
    }
}
