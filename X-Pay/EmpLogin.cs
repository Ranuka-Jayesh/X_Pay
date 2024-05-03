using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using X_Pay.Employee;

namespace X_Pay
{
    public partial class EmpLogin : Form
    {
        public Point mouseLocation;
        public EmpLogin()
        {
            InitializeComponent();
        }

        private void EmpLogin_Load(object sender, EventArgs e)
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

        private void label6_Click(object sender, EventArgs e)
        {
            ChooseUser ch = new ChooseUser();
            ch.Show();
            this.Hide();
        }

        private void view_Click(object sender, EventArgs e)
        {
            if (EmpPsw.UseSystemPasswordChar == true)
            {
                EmpPsw.UseSystemPasswordChar = false;
            }
            else
            {
                EmpPsw.UseSystemPasswordChar = true;
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            string username = EmpUname.Text.Trim();  // Trimming to remove any leading or trailing spaces
            string password = EmpPsw.Text;

            if (string.IsNullOrWhiteSpace(username) && string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter a username and password.");
                return; // Exit the method to avoid further processing
            }
            else if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Please enter a username.");
                return; // Exit the method to avoid further processing
            }
            else if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter a password.");
                return; // Exit the method to avoid further processing
            }

            var dbInstance = new db();
            // Properly sanitize inputs (though this is not a replacement for parameterized queries)
            username = username.Replace("'", "''");
            password = password.Replace("'", "''");

            var query = $"SELECT * FROM Employee WHERE Username LIKE '{username}' AND Password LIKE '{password}'";
            var reader = dbInstance.Select(query);

            if (reader != null && reader.HasRows)
            {
                EmployeeDash ED = new EmployeeDash();
                ED.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }




    }
}
