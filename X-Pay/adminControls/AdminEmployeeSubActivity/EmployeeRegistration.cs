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
using System.Reflection.Emit;


namespace X_Pay.AdminControls.AdminEmployeeSubActivity
{
    public partial class EmployeeRegistration : UserControl
    {
        string selectedFilePath;
        public EmployeeRegistration()
        {
            InitializeComponent();
        }

        private void dp_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file path
                selectedFilePath = openFileDialog1.FileName;

                // Extract the file name from the file path
                string fileName = Path.GetFileName(selectedFilePath);

                // Specify the fixed path to the 'User' directory
                string userPath = @"C:\Users\Ranuka Jayesh\Desktop\Xpay\NewGit\X-Pay\User";

                // Check if the 'User' directory exists, if not create it
                if (!Directory.Exists(userPath))
                {
                    Directory.CreateDirectory(userPath);
                }

                // Define the path to save the file inside the 'User' directory
                string destinationFilePath = Path.Combine(userPath, fileName);

                // Copy the file to the 'User' directory
                try
                {
                    File.Copy(selectedFilePath, destinationFilePath, true);
                    // Update the stored file path to the new path
                    selectedFilePath = destinationFilePath;
                    dp.Image = Image.FromFile(destinationFilePath);
                    // Optionally, show a message box (comment out if not needed)
                    MessageBox.Show("File selected and saved to: " + destinationFilePath, "File Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while copying the file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
        private bool IsValidString(string input)
        {
            // Regular expression to match only letters and spaces
            string pattern = @"^[a-zA-Z\s]+$";
            return Regex.IsMatch(input, pattern);
        }
        private bool IsValidAge(DateTime birthDate)
        {
            int age = DateTime.Now.Year - birthDate.Year;
            // Check if a birthday has occurred this year
            if (birthDate > DateTime.Now.AddYears(-age)) age--;
            return age >= 16;
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

            // Validate each string field 
            if (!IsValidString(firstname))
            {
                MessageBox.Show("First name must contain only letters and spaces.", "Invalid First Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!IsValidString(lastname))
            {
                MessageBox.Show("Last name must contain only letters and spaces.", "Invalid Last Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!IsValidString(position))
            {
                MessageBox.Show("Position must contain only letters and spaces.", "Invalid Position", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!IsValidString(education))
            {
                MessageBox.Show("Education must contain only letters and spaces.", "Invalid Education", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // check BOD validation
            if (!IsValidAge(dateOfBirth))
            {
                MessageBox.Show("Employee must be at least 16 years old.", "Invalid Age", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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
                          VALUES ('{firstname}', '{lastname}', '{email}', '{address}', '{position}', '{education}', '{username}', '{password}', '{phone}', '{skills}', '{dateOfBirth}', '{selectedFilePath}', '{registrationDate}')";

            try
            {
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
