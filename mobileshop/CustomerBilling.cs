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
    public partial class CustomerBilling : Form
    {
        public CustomerBilling()
        {
            InitializeComponent();
            
            quantityBox.TextChanged += (s, e) => UpdateTotalPrice();
            textBox6.TextChanged += (s, e) => UpdateTotalPrice();

            listofproducts.SelectedIndexChanged += listofproducts_SelectedIndexChanged;
        }
        SqlConnection connection = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = MobileShopDB; Integrated Security = True");
        void FillProductcb()
        {
            try
            {
                connection.Open();
                string querie = "select * from Products";
                SqlCommand command = new SqlCommand(querie, connection);
                SqlDataReader rdr;
                rdr = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("ProductName");
                dt.Load(rdr);
                listofproducts.ValueMember = "ProductName";
                listofproducts.DataSource = dt;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message);
            }

            finally
            {
                connection.Close();
            }
            

        }
        

        private void CustomerBilling_Load(object sender, EventArgs e)
        {
            FillProductcb();
            if (listofproducts.Items.Count > 0)
            {
                listofproducts.SelectedIndex = 0;
                UpdateUnitPrice(); 
            }



        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void backbutton_Click(object sender, EventArgs e)
        {
            Homepage homepage = new Homepage(LoginForm.IsAdmin);
            homepage.Show();
            this.Hide();
        }

        

        private void label7_Click(object sender, EventArgs e)
        {

        }
        private void UpdateTotalPrice()
        {
            // Calculate total price when Quantity or UnitPrice changes
            if (int.TryParse(quantityBox.Text, out int quantity) && decimal.TryParse(textBox6.Text, out decimal unitPrice))
            {
                decimal totalPrice = quantity * unitPrice;
                textBox7.Text = totalPrice.ToString("F2"); // Format to 2 decimal places
            }
            else
            {
                textBox7.Text = "0.00"; // Reset if inputs are invalid
            }
        }

        private void listofproducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUnitPrice();
        }
        private void UpdateUnitPrice()
        {
            if (listofproducts.SelectedItem != null)
            {
                string SelectedProduct = listofproducts.Text;

                string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MobileShopDB;Integrated Security=True";
                string query = "SELECT UnitPrice FROM Products WHERE ProductName = @ProductName";

                try
                {
                    using (SqlConnection con = new SqlConnection(connection))
                    {
                        con.Open();

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@ProductName", SelectedProduct);

                            SqlDataReader Reader = cmd.ExecuteReader();
                            if (Reader.Read())
                            {
                                textBox6.Text = Reader["UnitPrice"].ToString();
                            }
                            else
                            {
                                textBox6.Clear();
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error retriving unit price :" + ex.Message);

                }
            }
        }

        private void cbbillbutton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("Enter all details");
                return;
            }
            string CustomerName = textBox1.Text;
            string CustomerPhone = (textBox2.Text);
            string Address = textBox3.Text;
            string ProductName = listofproducts.Text;
            int Quantity = int.Parse(quantityBox.Text);
            decimal UnitPrice = decimal.Parse(textBox6.Text);
            decimal TotalPrice = decimal.Parse(textBox7.Text);
            DateTime Date = dateTimePicker1.Value;

            TotalPrice = Quantity * UnitPrice;
            string conString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MobileShopDB;Integrated Security=True";

            string query1 = "INSERT INTO Sales (CustomerName,ProductName,Phone,Quantity,TotalPrice,Date) VALUES (@CustomerName, @ProductName, @CustomerPhone, @Quantity,@TotalPrice,@Date)";

            string query3 = "UPDATE Products SET Quantity = Quantity - @Quantity WHERE ProductName = @ProductName";
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand(query1, con);
                    cmd1.Parameters.AddWithValue("@CustomerName", CustomerName);
                    cmd1.Parameters.AddWithValue("@ProductName", ProductName);
                    cmd1.Parameters.AddWithValue("@CustomerPhone", CustomerPhone);
                    cmd1.Parameters.AddWithValue("@Quantity", Quantity);
                    cmd1.Parameters.AddWithValue("@TotalPrice", TotalPrice);
                    cmd1.Parameters.AddWithValue("@Date", Date);


                    SqlCommand cmd3 = new SqlCommand(query3, con);

                    cmd3.Parameters.AddWithValue("@Quantity", Quantity);
                    cmd3.Parameters.AddWithValue("@ProductName", ProductName);




                    cmd1.ExecuteNonQuery();

                    cmd3.ExecuteNonQuery();

                    MessageBox.Show("Data saved successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error Saving Data :" + ex.Message);
            }




        }
  
       
        private void ClearButton_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            
            textBox6.Clear();
            textBox7.Clear();
           
        }
    }
}
