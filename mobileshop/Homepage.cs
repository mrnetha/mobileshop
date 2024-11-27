using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mobileshop
{
    public partial class Homepage : Form
    {
        private bool IsAdmin;
        public Homepage(bool isAdmin)
        {
            InitializeComponent();
            this.IsAdmin = isAdmin;

            SetUpRoleBasedAccess();
        }
        private void SetUpRoleBasedAccess()
        {
           
            if (IsAdmin)
            {
                // If the user is an admin, show the "Add User" button
                adduserbutton.Enabled = true;
                adduserbutton.Visible = true;// Enable the "Add User" button
                button1.Enabled = true;
                button1.Visible = true;
                
            }
            else
            {
                // If the user is not an admin, hide or disable the "Add User" button
                adduserbutton.Enabled = false; // Disable the "Add User" button
                adduserbutton.Visible = false; // Optionally, you can hide the button completely
                button1.Enabled = false;
                button1.Visible = false;
               
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MInfoButton_Click(object sender, EventArgs e)
        {
            MobileInformation homepage = new MobileInformation();
            homepage.Show();
            this.Hide();
        }

        private void CInfoButton_Click(object sender, EventArgs e)
        {
            CustomerDetails cd = new CustomerDetails();
            cd.Show();
            this.Hide();
        }

        private void CBiillButton_Click(object sender, EventArgs e)
        {
            CustomerBilling cb = new CustomerBilling();
            cb.Show();
            this.Hide();
        }

        private void SBillButton_Click(object sender, EventArgs e)
        {
            SupplierBIlling sb = new SupplierBIlling();
            sb.Show();
            this.Hide();
        }

        private void HelpButton_Click(object sender, EventArgs e)
        {
            ContactUS cu = new ContactUS();
            cu.Show();
            this.Hide();
        }

        private void SignOutButton_Click(object sender, EventArgs e)
        {
            LoginForm lg = new LoginForm();
            lg.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InventoryManagement inventoryManagement = new InventoryManagement();
            inventoryManagement.Show();
            this.Hide();
        }

        private void Homepage_Load(object sender, EventArgs e)
        {

        }

        private void shopnamelabel_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddEmployeeUser addEmployeeUser = new AddEmployeeUser();
            addEmployeeUser.Show();
            this.Hide();
        }
    }
}
