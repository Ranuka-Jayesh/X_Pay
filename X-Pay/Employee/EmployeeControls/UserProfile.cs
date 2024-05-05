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


namespace X_Pay.Employee.EmployeeControls
{
    public partial class UserProfile : UserControl
    {
        private int EmployeeID;
        public UserProfile( int employeeID)
        {
            InitializeComponent();
            EmployeeID = employeeID;
        }

        private void label3_Click(object sender, EventArgs e)
        {

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

        private void button1_Click(object sender, EventArgs e)
        {
            string query = $"SELECT Password FROM Employee WHERE EmployeeID = {EmployeeID}";
            db database = new db();

            using (SqlDataReader reader = database.Select(query))
            {

                if (reader.Read())
                {

                    if (cp.Text == reader["Password"].ToString())
                    {
                        reader.Close();
                        if (!IsValidPassword(np.Text))
                        {
                            MessageBox.Show("Password must be at least 8 characters long and include uppercase, lowercase, digits, and symbols.", "Password Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        string updateQuery = $"UPDATE Employee SET Password = '{np.Text}' WHERE EmployeeID = {EmployeeID}";
                        database.Execute(updateQuery);

                        MessageBox.Show("Password updated successfully.");
                        cp.Text = "";
                        np.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Current password is incorrect.");
                    }
                }
                else
                {
                    MessageBox.Show("No data found.");
                }
            }
        }

        private void UserProfile_Load(object sender, EventArgs e)
        {
            LoadEmployeeDetails();
        }

        public void LoadEmployeeDetails()
        {
            string query = $"SELECT FirstName, LastName, Email, Address, Position, Education, Skills, DateOfBirth, RegisterDate , Contact , ProfilePic FROM Employee WHERE EmployeeID = {EmployeeID}";
            db database = new db();
            {
                using (SqlDataReader reader = database.Select(query))
                {
                    if (reader != null && reader.Read())
                    {
                        EmpName.Text = $"{reader["FirstName"]} {reader["LastName"]}";
                        email.Text = reader["Email"].ToString();
                        addrss.Text = reader["Address"].ToString();
                        position.Text = reader["Position"].ToString();
                        educations.Text = reader["Education"].ToString();
                        skills.Text = reader["Skills"].ToString();
                        RegisteredDate.Text = Convert.ToDateTime(reader["RegisterDate"]).ToString("yyyy-MM-dd");
                        contact.Text = reader["Contact"].ToString();

                        DateTime birthDate = Convert.ToDateTime(reader["DateOfBirth"]);
                        age.Text = CalculateAge(birthDate).ToString();


                        if (reader["ProfilePic"] != DBNull.Value)
                        {
                            string imagePath = reader["ProfilePic"].ToString();
                            if (File.Exists(imagePath))
                            {
                                using (var img = Image.FromFile(imagePath))
                                {
                                    profilepic.Image = new Bitmap(img, new Size(150, 150));
                                }
                            }
                        }
                    }
                    reader.Close();
                }
            }
        }

        private int CalculateAge(DateTime dateOfBirth)
        {
            int age = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth > DateTime.Today.AddYears(-age)) age--;
            return age;
        }
    }
}
