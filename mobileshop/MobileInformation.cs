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

namespace mobileshop
{
    public partial class MobileInformation : Form
    {
        public MobileInformation()
        {
            InitializeComponent();
        }

        private void backbutton_Click(object sender, EventArgs e)
        {
            Homepage homepage = new Homepage(LoginForm.IsAdmin);
            homepage.Show();
            this.Hide();
        }

        private void searchbutton_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text.Trim(); // Assuming you have a TextBox named txtSearch for entering ProductID or ProductName
            string conString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MobileShopDB;Integrated Security=True";
            string query;

            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Please enter Product ID or Product Name to search.");
                return;
            }

            using (SqlConnection con = new SqlConnection(conString))
            {
                try
                {
                    SqlCommand cmd;

                    // Check if input is a valid Product ID (integer)
                    if (int.TryParse(input, out int ProductId))
                    {
                        query = "SELECT * FROM Products WHERE ProductID = @ProductID";
                        cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@ProductID", ProductId);
                    }
                    else
                    {
                        query = "SELECT * FROM Products WHERE ProductName LIKE @ProductName";
                        cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@ProductName", "%" + input + "%");
                    }

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dt; // Assuming you have a DataGridView named dataGridView1
                    }
                    else
                    {
                        MessageBox.Show("No data found for the given Product ID or Product Name.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
