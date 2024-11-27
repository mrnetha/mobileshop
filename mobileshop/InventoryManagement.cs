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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace mobileshop
{
    public partial class InventoryManagement : Form
    {
        public InventoryManagement()
        {
            InitializeComponent();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["ProductName"].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["Quantity"].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["UnitPrice"].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells["Description"].Value.ToString();
            dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["WarrantyEndDate"].Value);

        }

        private void InventoryManagement_Load(object sender, EventArgs e)
        {
            pageaLoad();
            dataGridView1.ReadOnly = true;
            dataGridView1.Columns["ProductName"].ReadOnly = false;
            dataGridView1.Columns["Quantity"].ReadOnly = false;
            dataGridView1.Columns["UnitPrice"].ReadOnly = false;
            dataGridView1.Columns["Description"].ReadOnly = false;
            dataGridView1.Columns["WarrantyEndDate"].ReadOnly = false;
        }
        private void pageaLoad()
        {
            string cnctstrg = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MobileShopDB;Integrated Security=True";
            string query = "SELECT * FROM Products";
            dataGridView1.ReadOnly = true;

            using (SqlConnection con = new SqlConnection(cnctstrg))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, con);

                DataTable dt = new DataTable();
                con.Open();
                dataAdapter.Fill(dt);
                dataGridView1.DataSource = dt;

            }


        }

        private void backbutton_Click(object sender, EventArgs e)
        {
            Homepage homepage = new Homepage(LoginForm.IsAdmin);
            homepage.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddB_Click(object sender, EventArgs e)
        {


            string ProductName = textBox1.Text;
            int Quantity = int.Parse(textBox2.Text);
            decimal UnitPrice = decimal.Parse(textBox3.Text);
            string Description = textBox4.Text;
            DateTime Warranty = dateTimePicker1.Value;

            string conString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MobileShopDB;Integrated Security=True";
            string query1 = "INSERT INTO Products (ProductName, Quantity, UnitPrice, Description, WarrantyEndDate) VALUES (@ProductName,@Quantity, @UnitPrice, @Description, @Warranty)";
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand(query1, con);

                cmd1.Parameters.AddWithValue("@ProductName", ProductName);
                cmd1.Parameters.AddWithValue("@Quantity", Quantity);
                cmd1.Parameters.AddWithValue("@UnitPrice", UnitPrice);
                cmd1.Parameters.AddWithValue("@Description", Description);
                cmd1.Parameters.AddWithValue("@Warranty", Warranty);

                cmd1.ExecuteNonQuery();

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();

                pageaLoad();

            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text =="" || textBox2.Text == "")
            {
                MessageBox.Show("Enter All Fields");
            }
            else
            {
                string ProductName = textBox1.Text;
                int Quantity = int.Parse(textBox2.Text);
                decimal UnitPrice = decimal.Parse(textBox3.Text);
                string Description = textBox4.Text;
                DateTime WarrantyEndDate = dateTimePicker1.Value;

                int ProductId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ProductId"].Value);

                string conString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MobileShopDB;Integrated Security=True";
                string query1 = "UPDATE Products SET ProductName = @ProductName, Quantity = @Quantity, UnitPrice = @UnitPrice, Description = @Description, WarrantyEndDate = @WarrantyEndDate WHERE ProductId = @ProductId";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand(query1, con);

                    cmd1.Parameters.AddWithValue("@ProductName", ProductName);
                    cmd1.Parameters.AddWithValue("@Quantity", Quantity);
                    cmd1.Parameters.AddWithValue("@UnitPrice", UnitPrice);
                    cmd1.Parameters.AddWithValue("@Description", Description);
                    cmd1.Parameters.AddWithValue("@WarrantyEndDate", WarrantyEndDate);
                    cmd1.Parameters.AddWithValue("@ProductId", ProductId);

                    cmd1.ExecuteNonQuery();
                   

                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();

                    pageaLoad();
                 
                }
            }
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this product?", "Confirm Delete", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    int ProductId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ProductId"].Value);
                    DeleteProductFromDatabase(ProductId);
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);

                }
                else
                {
                    MessageBox.Show("Please select a row to delete");
                }
            }
        }
        private void DeleteProductFromDatabase(int productId)
        {
            try
            {
                string dbcon = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MobileShopDB;Integrated Security=True";

                using (SqlConnection conn = new SqlConnection(dbcon))
                {
                    conn.Open();

                    string query = "DELETE FROM Products WHERE ProductID = @ProductID"; 

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        
                        cmd.Parameters.AddWithValue("@ProductID", productId);

                        
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting product: " + ex.Message);
            }
        }

    }
}

