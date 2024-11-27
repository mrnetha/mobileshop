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
    public partial class CustomerDetails : Form
    {
        public CustomerDetails()
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
            string input = textBox1.Text;

            if(string.IsNullOrEmpty(input))
            {
                MessageBox.Show("enter customer name or number");
            }

            string conString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MobileShopDB;Integrated Security=True";
            string query = "";

            if(IsValidPhoneNumber(input))
            {
                query = "select * from Sales where Phone = @MobileNumber";
            }
            else
            {
                query = "SELECT * FROM Sales WHERE CustomerName like @CustomerName";
            }

            using(SqlConnection con = new SqlConnection(conString))
            {
                try
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, con);

                    if (IsValidPhoneNumber(input))
                    {
                        dataAdapter.SelectCommand.Parameters.AddWithValue("@MobileNumber", input);
                    }
                    else
                    {
                        dataAdapter.SelectCommand.Parameters.AddWithValue("@CustomerName","%" + input + "%");
                    }

                    DataTable dt = new DataTable();
                    con.Open();
                    dataAdapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("No Data Found");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An Error Occured", ex.Message);
                }
            }


        }
        private bool IsValidPhoneNumber(string input)
        {
            
            return input.Length == 10 && long.TryParse(input, out _);
        }

        private void CustomerDetails_Load(object sender, EventArgs e)
        {

        }
    }
}
