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
                catname = "";


            }
            catch (Exception ex)
            {
                // Show error message box if insertion fails
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
