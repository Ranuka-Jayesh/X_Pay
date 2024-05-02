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
        string imagelocation = "";
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
                imagelocation = selectedFileName;
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


        private void button1_Click(object sender, EventArgs e)
        {
            // Create a new instance of the database access class
            db DB = new db();
            try
            {
                //store image
                byte[] dp = null;
                FileStream Streem = new FileStream(imagelocation, FileMode.Open, FileAccess.Read);
                BinaryReader binaryReader = new BinaryReader(Streem);
                dp = binaryReader.ReadBytes((int)Streem.Length);

                //ValueSerializerAttribute initialization
                string firstname = FName.Text;
                string lastname = LName.Text;
                string email = Email.Text;
                string address = Address.Text;
                string position = Cpossition.Text;
                string phone = contact.Text;
                string education = Educations.Text;
                string Skill = skill.Text;
                DateTime dateOfBirth = BOD.Value;
                string username = uname.Text;
                string password = psw.Text;
                DateTime RegistrationDate = DateTime.Now;

                //SQL Query
                string query = "INSERT INTO Employee VALUES ('" + firstname + "' , '" + lastname + "' , '" + email + "' , '" + address + "' , '" + position + "' ,'" + education + "','" + username + "','" + password + "','" + phone + "','" + Skill + "','" + dateOfBirth + "','"+dp+"' ,'"+RegistrationDate+"')";


                //check password validation
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

                //check username validation
                var idExistsReader = new db().Select("SELECT COUNT(*) FROM Employee WHERE Username = '" + username + "'");
                idExistsReader.Read();
                int idCount = Convert.ToInt32(idExistsReader[0]);
                idExistsReader.Close();
                if (idCount > 0)
                {
                    MessageBox.Show("User already exists. Please.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Exit the method without executing the insert query
                }

                DB.Execute(query);
                // Show success message box
                MessageBox.Show("Data inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clearvalues();


            }
            catch (Exception ex)
            {
                // Show error message box if insertion fails
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
