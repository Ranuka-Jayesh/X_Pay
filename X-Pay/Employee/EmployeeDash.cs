﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using X_Pay.AdminControls;
using X_Pay.Employee.EmployeeControls;

namespace X_Pay.Employee
{
    public partial class EmployeeDash : Form
    {
        public Point mouseLocation;
        
        public EmployeeDash()
        {
            InitializeComponent();
            ApplyRoundedCorners();

            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, profilepic.Width, profilepic.Height);
            profilepic.Region = new Region(path);
        }
        private void ApplyRoundedCorners()
        {
            int radius = 10;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
            path.AddArc(this.Width - radius * 2, 0, radius * 2, radius * 2, 270, 90);
            path.AddArc(this.Width - radius * 2, this.Height - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(0, this.Height - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseFigure();
            this.Region = new Region(path);
        }
        private void EmployeeDash_Load(object sender, EventArgs e)
        {
            EmpHome EHM = new EmpHome();
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(EHM);
        }

        private void Mouse_Down(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void Mouse_Move(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
            }
        }

        private void closeico_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void minico_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void livetime_Tick(object sender, EventArgs e)
        {
            livetimes.Text = DateTime.Now.ToLongTimeString();
            livedate.Text = DateTime.Now.ToLongDateString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                EmpLogin Ed = new EmpLogin();
                Ed.Show();
                this.Hide();
            }
        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            EmpHome EHM = new EmpHome();
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(EHM);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EmployeeControls.EmpMyTask  myTask = new EmployeeControls.EmpMyTask();
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(myTask);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FreeTask Ft = new FreeTask();
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(Ft);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EmpMyWallet wt = new EmpMyWallet();
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(wt);
        }

        private void profilepic_Click(object sender, EventArgs e)
        {
            UserProfile up = new UserProfile();
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(up);
        }
    }
}
