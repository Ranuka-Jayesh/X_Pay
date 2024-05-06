using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace X_Pay
{
    public partial class AdminDash : Form
    {
        public Point mouseLocation;
        public AdminDash()
        {
            InitializeComponent();
            ApplyRoundedCorners();
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }


        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void livetime_Tick(object sender, EventArgs e)
        {
            livetimes.Text = DateTime.Now.ToLongTimeString();
            livedate.Text = DateTime.Now.ToLongDateString();
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


        private void AdminDash_Load(object sender, EventArgs e)
        {
            AdminControls.AdminHome Home = new AdminControls.AdminHome();
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(Home);
        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            AdminControls.AdminHome Home = new AdminControls.AdminHome();
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(Home);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminControls.AdminEmployees Employee = new AdminControls.AdminEmployees();
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(Employee);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminControls.AdminProject Project = new AdminControls.AdminProject();
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(Project);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AdminControls.AdminSetting Setting = new AdminControls.AdminSetting();
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(Setting);
        }

        public void MainPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                AdminLoging AD = new AdminLoging();
                AD.Show();
                this.Hide();
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

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

        private void MouseClick(object sender, MouseEventArgs e)
        {
            label6.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminControls.Incomes Incomes = new AdminControls.Incomes();
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(Incomes);
        }
    }
}
