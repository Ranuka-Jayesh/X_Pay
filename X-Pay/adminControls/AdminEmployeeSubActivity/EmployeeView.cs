using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace X_Pay.AdminControls.AdminEmployeeSubActivity
{
    public partial class EmployeeView : UserControl
    {
        public EmployeeView()
        {
            InitializeComponent();
        }
        private void EmployeeView_Load(object sender, EventArgs e)
        {
            // Select data to datagridview and display
            var reader = new db().Select("SELECT * FROM Employee");
            dataviwe.Rows.Clear();
            while (reader.Read())
            {
                DateTime dateOfBirth = (DateTime)reader["DateOfBirth"];
                int age = CalculateAge(dateOfBirth);

                dataviwe.Rows.Add(reader["EmployeeID"], reader["Username"], reader["FirstName"], reader["LastName"], age, reader["Email"], reader["Address"], reader["Contact"], reader["Position"], reader["Registerdate"]);
            }
        }
        private int CalculateAge(DateTime dateOfBirth)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }

        private void employeeID_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void employeeID_TextChanged_1(object sender, EventArgs e)
        {
            string searchdata = employeeID.Text;

            if (searchdata == "")
            {
                FirstName.Text = "";
                LastName.Text = "";
                EmpEmail.Text = "";
                EmpAddress.Text = "";
                EmpPosition.Text = "";
                EmpEdu.Text = "";
                User.Text = "";
                Contacts.Text = "";
                skill.Text = "";
                psw.Text = "";
            }
            else
            {
                var reader = new db().Select(string.IsNullOrWhiteSpace(searchdata) ? "SELECT * FROM Employee" : "SELECT * FROM Employee WHERE EmployeeID LIKE '" + searchdata + "'");

                while (reader.Read())
                {
                    FirstName.Text = (string)reader["FirstName"];
                    LastName.Text = (string)reader["LastName"];
                    EmpEmail.Text = (string)reader["Email"];
                    EmpAddress.Text = (string)reader["Address"];
                    EmpPosition.Text = (string)reader["Position"];
                    EmpEdu.Text = (string)reader["Education"];
                    User.Text = (string)reader["Username"];
                    Contacts.Text = (string)reader["Contact"];
                    skill.Text = (string)reader["Skills"];
                    psw.Text = (string)reader["Password"];

                }
            }
            
        }

        private void FirstName_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void label14_Click(object sender, EventArgs e)
        {
            employeeID.Text = "";
            FirstName.Text = "";
            LastName.Text = "";
            EmpEmail.Text = "";
            EmpAddress.Text = "";
            EmpPosition.Text = "";
            EmpEdu.Text = "";
            User.Text = "";
            Contacts.Text = "";
            skill.Text = "";
            psw.Text = "";
        }

        private void label11_Click(object sender, EventArgs e)
        {
            // Check if the employee ID field is empty
            if (string.IsNullOrWhiteSpace(employeeID.Text))
            {
                MessageBox.Show("Please enter the Employee ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the method without executing the update query
            }

            try
            {
                // SQL Query
                string query = "UPDATE Employee SET FirstName = '" + FirstName.Text + "', LastName = '" + LastName.Text + "', Email = '" + EmpEmail.Text + "', Address = '" + EmpAddress.Text + "', Position = '" + EmpPosition.Text + "', Education = '" + EmpEdu.Text + "', Username = '" + User.Text + "', Password = '" + psw.Text + "', Contact = '" + Contacts.Text + "', Skills = '" + skill.Text + "' WHERE EmployeeID = '" + employeeID.Text + "'";

                db DB = new db();
                // Execute the update query
                DB.Execute(query);

                // Show success message box
                MessageBox.Show("Data updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Select data to datagridview and display
                var reader = new db().Select("SELECT * FROM Employee");
                dataviwe.Rows.Clear();
                while (reader.Read())
                {
                    DateTime dateOfBirth = (DateTime)reader["DateOfBirth"];
                    int age = CalculateAge(dateOfBirth);

                    dataviwe.Rows.Add(reader["EmployeeID"], reader["Username"], reader["FirstName"], reader["LastName"], age, reader["Email"], reader["Address"], reader["Contact"], reader["Position"], reader["Registerdate"]);
                }
            }
            catch (Exception ex)
            {
                // Log exception details here, e.g., to a file or a database
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            
        }

        private void label13_Click(object sender, EventArgs e)
        {
            // Check if the employee ID field is empty
            if (string.IsNullOrWhiteSpace(employeeID.Text))
            {
                MessageBox.Show("Please enter the Employee ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the method without executing the update query
            }

            // Construct the SQL query for deleting the record
            string Query = "DELETE FROM employee WHERE EmployeeID = " + employeeID.Text;

            // Create a new instance of the database access class
            db DB = new db();

            // Execute the delete query
            DB.Execute(Query);
            MessageBox.Show("Data Delete successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Select data to datagridview and display
            var reader = new db().Select("SELECT * FROM Employee");
            dataviwe.Rows.Clear();
            while (reader.Read())
            {
                DateTime dateOfBirth = (DateTime)reader["DateOfBirth"];
                int age = CalculateAge(dateOfBirth);

                dataviwe.Rows.Add(reader["EmployeeID"], reader["Username"], reader["FirstName"], reader["LastName"], age, reader["Email"], reader["Address"], reader["Contact"], reader["Position"], reader["Registerdate"]);
            }
        }
    }
}
