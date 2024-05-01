using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace X_Pay.Employee.EmployeeControls
{
    public partial class EmpMyWallet : UserControl
    {
        public EmpMyWallet()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, EventArgs e)
        {
            MyWalletSubActivities.UpcomingPayments upcomingPayments = new MyWalletSubActivities.UpcomingPayments();
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(upcomingPayments);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyWalletSubActivities.Income Income = new MyWalletSubActivities.Income();
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(Income);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MyWalletSubActivities.TotalIncome TotalIncome = new MyWalletSubActivities.TotalIncome();
            MainPanel.Controls.Clear();
            MainPanel.BringToFront();
            MainPanel.Focus();
            MainPanel.Controls.Add(TotalIncome);
        }
    }
}
