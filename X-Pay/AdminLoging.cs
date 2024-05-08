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
    public partial class AdminLoging : Form
    {
        public Point mouseLocation;
        public AdminLoging()
        {
            InitializeComponent();
        }

        private void AdminLoging_Load(object sender, EventArgs e)
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

        private void view_Click(object sender, EventArgs e)
        {
            if(AdminPsw.UseSystemPasswordChar == true)
            {
                AdminPsw.UseSystemPasswordChar = false;
            }
            else
            {
                AdminPsw.UseSystemPasswordChar = true;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            ChooseUser ch = new ChooseUser();
            ch.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string enteredUsername = AdminUname.Text;
            string enteredPassword = AdminPsw.Text;

            if (string.IsNullOrEmpty(enteredUsername) || string.IsNullOrEmpty(enteredPassword))
            {
                if (string.IsNullOrEmpty(enteredUsername) && string.IsNullOrEmpty(enteredPassword))
                {
                    MessageBox.Show("Please enter a username and password.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrEmpty(enteredUsername))
                {
                    MessageBox.Show("Please enter a username.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Please enter a password.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
            else
            {
                if (enteredUsername == "Admin" && enteredPassword == "Admin")
                {
                    AdminDash AD = new AdminDash();
                    AD.Show();
                    this.Hide();
                }
                else
                {
                    if (enteredUsername != "Admin" && enteredPassword != "Admin")
                    {
                        MessageBox.Show("Both username and password are incorrect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (enteredUsername != "Admin")
                    {
                        MessageBox.Show("The username is incorrect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (enteredPassword != "Admin")
                    {
                        MessageBox.Show("The password is incorrect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void Mouve_Down(object sender, MouseEventArgs e)
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
    }
}
