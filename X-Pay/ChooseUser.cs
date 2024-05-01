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
    public partial class ChooseUser : Form
    {
        public Point mouseLocation;
        public ChooseUser()
        {
            InitializeComponent();

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

        private void ChooseUser_Load(object sender, EventArgs e)
        {
            int radius = 10;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
            path.AddArc(this.Width - radius * 2, 0, radius * 2, radius * 2, 270, 90);
            path.AddArc(this.Width - radius * 2, this.Height - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(0, this.Height - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseFigure();
            this.Region = new Region(path);

            GreetUser();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminLoging AD = new AdminLoging();
            AD.Show();
            this.Hide();
        }

        private void GreetUser()
        {
            int hour = DateTime.Now.Hour;
            string message;
            if (hour < 12)
            {
                message = "Good Morning";
            }
            else if (hour < 18)
            {
                message = "Good Afternoon";
            }
            else
            {
                message = "Good Evening";
            }
            greetmsg.Text = message;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EmpLogin lg = new EmpLogin();
            lg.Show();
            this.Hide();
        }

        private void Mouse_Down(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void Mouse_move(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
            }
        }
    }
}
