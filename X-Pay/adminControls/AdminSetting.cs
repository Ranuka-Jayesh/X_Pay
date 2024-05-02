using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Xml.Linq;

namespace X_Pay.AdminControls
{
    public partial class AdminSetting : UserControl
    {
        public AdminSetting()
        {
            InitializeComponent();
        }

        private void CatBT_Click(object sender, EventArgs e)
        {
            // Create a new instance of the database access class
            db DB = new db();
            try
            {
               
                string catname = CATName.Text;

                //SQL Query
                string query = "INSERT INTO Cat VALUES ('" + catname + "')";

                DB.Execute(query);
                // Show success message box
                MessageBox.Show("Data inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CATName.Text = "";
                // Select data to datagridview and display
                var reader = new db().Select("SELECT * FROM Cat");
                CatDataView.Rows.Clear();
                while (reader.Read())
                {
                    CatDataView.Rows.Add(reader["CATID"], reader["CATName"]);
                }


            }
            catch (Exception ex)
            {
                // Show error message box if insertion fails
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AdminSetting_Load(object sender, EventArgs e)
        {
            // Select data to datagridview and display
            var reader = new db().Select("SELECT * FROM Cat");
            CatDataView.Rows.Clear();
            while (reader.Read())
            {
                CatDataView.Rows.Add(reader["CATID"], reader["CATName"]);
            }

            // Select data to datagridview and display
            var readers = new db().Select("SELECT * FROM sub");
            SubDataView.Rows.Clear();
            while (readers.Read())
            {
                SubDataView.Rows.Add(readers["SUBID"], readers["SUBName"]);
            }
        }

        private void CatDataView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // Check if the employee ID field is empty
            if (string.IsNullOrWhiteSpace(CATName.Text))
            {
                MessageBox.Show("Please enter the Catagory ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the method without executing the update query
            }

            // Construct the SQL query for deleting the record
            string Query = "DELETE FROM Cat WHERE CATID = " + CATName.Text;

            // Create a new instance of the database access class
            db DB = new db();

            // Execute the delete query
            DB.Execute(Query);
            MessageBox.Show("Data Delete successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Select data to datagridview and display
            var reader = new db().Select("SELECT * FROM Cat");
            CatDataView.Rows.Clear();
            while (reader.Read())
            {
                CatDataView.Rows.Add(reader["CATID"], reader["CATName"]);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Create a new instance of the database access class
            db DB = new db();
            try
            {

                string subname = Sub.Text;

                //SQL Query
                string query = "INSERT INTO sub VALUES ('" + subname + "')";

                DB.Execute(query);
                // Show success message box
                MessageBox.Show("Data inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Sub.Text = "";
                // Select data to datagridview and display
                var reader = new db().Select("SELECT * FROM sub");
                SubDataView.Rows.Clear();
                while (reader.Read())
                {
                    SubDataView.Rows.Add(reader["SUBID"], reader["SUBName"]);
                }


            }
            catch (Exception ex)
            {
                // Show error message box if insertion fails
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            // Check if the employee ID field is empty
            if (string.IsNullOrWhiteSpace(Sub.Text))
            {
                MessageBox.Show("Please enter the Sub ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the method without executing the update query
            }

            // Construct the SQL query for deleting the record
            string Query = "DELETE FROM sub WHERE SUBID = " + Sub.Text;

            // Create a new instance of the database access class
            db DB = new db();

            // Execute the delete query
            DB.Execute(Query);
            MessageBox.Show("Data Delete successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Select data to datagridview and display
            var reader = new db().Select("SELECT * FROM sub");
            SubDataView.Rows.Clear();
            while (reader.Read())
            {
                SubDataView.Rows.Add(reader["SUBID"], reader["SUBName"]);
            }
        }
    }
}
