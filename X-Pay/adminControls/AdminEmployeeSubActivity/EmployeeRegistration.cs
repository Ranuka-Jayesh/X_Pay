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
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Collections;
using System.Xml.Linq;
using System.Windows.Markup;
using System.Text.RegularExpressions;


namespace X_Pay.AdminControls.AdminEmployeeSubActivity
{
    public partial class EmployeeRegistration : UserControl
    {
        string imagelocations = "";
        public EmployeeRegistration()
        {
            InitializeComponent();
        }

        private void dp_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = openFileDialog.FileName.ToString();
                dp.Image = Image.FromFile(selectedFileName);
                imagelocations = selectedFileName;
            }
        }

        private void clearvalues()
        {
            FName.Text = string.Empty;
            LName.Text = string.Empty;
            Email.Text = string.Empty;
            Address.Text = string.Empty;
            Cpossition.Text = string.Empty;
            Educations.Text = string.Empty;
            uname.Text = string.Empty;
            psw.Text = string.Empty;
            contact.Text = string.Empty;
            skill.Text = string.Empty;
            BOD.Value = DateTime.Now;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clearvalues();
        }

        //Reguler Expresions Check
        private bool IsValidPassword(string password)
        {
            if (password.Length < 8)
                return false;

            bool hasUpper = false, hasLower = false, hasDigit = false, hasSymbol = false;
            foreach (char c in password)
            {
                if (char.IsUpper(c)) hasUpper = true;
                else if (char.IsLower(c)) hasLower = true;
                else if (char.IsDigit(c)) hasDigit = true;
                else if (!char.IsWhiteSpace(c)) hasSymbol = true;
            }

            return hasUpper && hasLower && hasDigit && hasSymbol;
        }
        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$";
            return Regex.IsMatch(email, emailPattern);
        }
        private bool IsValidContact(string contact)
        {
            // Check if contact number starts with '0' and has exactly 10 digits
            return contact.StartsWith("0") && contact.Length == 10 && contact.All(char.IsDigit);
        }
        private bool UsernameExists(string username, db DB)
        {
            using (var reader = DB.Select($"SELECT COUNT(*) FROM Employee WHERE Username = '{username}'"))
            {
                reader.Read();
                return Convert.ToInt32(reader[0]) > 0;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // Initialize database connection
            db DB = new db();

            // Read inputs from form fields
            string firstname = FName.Text;
            string lastname = LName.Text;
            string email = Email.Text;
            string address = Address.Text;
            string position = Cpossition.Text;
            string phone = contact.Text;
            string education = Educations.Text;
            string skills = skill.Text;
            DateTime dateOfBirth = BOD.Value;
            string username = uname.Text;
            string password = psw.Text;
            DateTime registrationDate = DateTime.Now;


            // Get the next image number and prepare image path
            string basePath = @"C:\Users\Ranuka Jayesh\Desktop\Xpay\NewGit\X-Pay\Images\UserProfile\";
            int imageNumber = GetNextImageNumber(basePath);
            string userImagePath = Path.Combine(basePath, username);
            Directory.CreateDirectory(userImagePath);
            string imagelocation = imagelocations;
            string newImageName = imageNumber.ToString() + Path.GetExtension(imagelocation);
            string imagePath = Path.Combine(userImagePath, newImageName);

            // check password validation
            if (!IsValidPassword(password))
            {
                MessageBox.Show("Password must be at least 8 characters long and include uppercase, lowercase, digits, and symbols.", "Password Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //check email validation
            if (!IsValidEmail(email))
            {
                MessageBox.Show("The email address is not in a valid format.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate contact number
            if (!IsValidContact(phone))
            {
                MessageBox.Show("Invalid contact number. Check your contact number have 10 digits.", "Contact Number Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if username already exists
            if (UsernameExists(username, DB))
            {
                MessageBox.Show("Username already exists. Please choose another.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            


            // Build SQL query to insert the new employee record
            string query = $@"INSERT INTO Employee (FirstName, LastName, Email, Address, Position, Education, Username, Password, Contact, Skills, DateOfBirth, ProfilePic, Registerdate) 
                          VALUES ('{firstname}', '{lastname}', '{email}', '{address}', '{position}', '{education}', '{username}', '{password}', '{phone}', '{skills}', '{dateOfBirth}', '{imagePath}', '{registrationDate}')";

            try
            {
                File.Copy(imagelocation, imagePath, true);
                // Execute SQL query
                DB.Execute(query);
                MessageBox.Show("Data inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clearvalues();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetNextImageNumber(string basePath)
        {
            string numberFilePath = Path.Combine(basePath, "ImageNumber.txt");
            int imageNumber = 1000; // Start from 1000 if file doesn't exist

            if (File.Exists(numberFilePath))
            {
                // Read the last used number
                string lastNumber = File.ReadAllText(numberFilePath);
                if (int.TryParse(lastNumber, out int lastNum))
                {
                    imageNumber = lastNum + 1; // Increment the last number
                }
            }

            // Write the new number back to the file
            File.WriteAllText(numberFilePath, imageNumber.ToString());
            return imageNumber;
        }

    }
}
