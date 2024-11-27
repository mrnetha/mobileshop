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
    public partial class SupplierBIlling : Form
    {
        public SupplierBIlling()
        {
            InitializeComponent();
            textBox5.TextChanged += (s, e) => UpdateTotalPrice();
            textBox6.TextChanged += (s, e) => UpdateTotalPrice();

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void backbutton_Click(object sender, EventArgs e)
        {
            Homepage homepage = new Homepage(LoginForm.IsAdmin);
            homepage.Show();
            this.Hide();
        }

        

        private void sbbillbutton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("Enter all details");
            }
            else
            {

                string SupplierName = textBox1.Text;
                string SupplierPhone = (textBox2.Text);
                string Address = textBox3.Text;
                string ProductName = textBox4.Text;
                int Quantity = int.Parse(textBox5.Text);
                decimal UnitPrice = decimal.Parse(textBox6.Text);
                decimal TotalPrice = decimal.Parse(textBox7.Text);
                string Description = textBox8.Text;

                DateTime Date = dateTimePicker1.Value;
                DateTime Warranty = dateTimePicker2.Value;

                TotalPrice = UnitPrice * Quantity;


                string conString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MobileShopDB;Integrated Security=True";

                string query1 = "INSERT INTO Suppliers (SupplierName, SupplierPhone,Address) VALUES (@SupplierName, @SupplierPhone, @Address)";
                string query2 = "INSERT INTO Purchase (ProductName, Quantity, UnitPrice, TotalPrice, Date) VALUES (@ProductName,@Quantity, @UnitPrice, @TotalPrice, @Date)";
                string query3 = "INSERT INTO Products (ProductName, Quantity, UnitPrice, Description, WarrantyEndDate) VALUES (@ProductName,@Quantity, @UnitPrice, @Description, @Warranty)";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand(query1, con);

                    cmd1.Parameters.AddWithValue("@SupplierName", SupplierName);
                    cmd1.Parameters.AddWithValue("@SupplierPhone", SupplierPhone);
                    cmd1.Parameters.AddWithValue("@Address", Address);

                    SqlCommand cmd2 = new SqlCommand(query2, con);

                    cmd2.Parameters.AddWithValue("@ProductName", ProductName);
                    cmd2.Parameters.AddWithValue("@Quantity", Quantity);
                    cmd2.Parameters.AddWithValue("@UnitPrice", UnitPrice);
                    cmd2.Parameters.AddWithValue("@TotalPrice", TotalPrice);
                    cmd2.Parameters.AddWithValue("@Date", Date);

                    SqlCommand cmd3 = new SqlCommand(query3, con);

                    cmd3.Parameters.AddWithValue("@ProductName", ProductName);
                    cmd3.Parameters.AddWithValue("@Quantity", Quantity);
                    cmd3.Parameters.AddWithValue("@UnitPrice", UnitPrice);
                    cmd3.Parameters.AddWithValue("@Description", Description);
                    cmd3.Parameters.AddWithValue("@Warranty", Warranty);

                    cmd1.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                    cmd3.ExecuteNonQuery();

                    MessageBox.Show("Data saved successfully!");



                }
            }

        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void TPriceLbl_Click(object sender, EventArgs e)
        {

        }

        private void SupplierBIlling_Load(object sender, EventArgs e)
        {


        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
        private void UpdateTotalPrice()
        {
            // Calculate total price when Quantity or UnitPrice changes
            if (int.TryParse(textBox5.Text, out int quantity) && decimal.TryParse(textBox6.Text, out decimal unitPrice))
            {
                decimal totalPrice = quantity * unitPrice;
                textBox7.Text = totalPrice.ToString("F2"); // Format to 2 decimal places
            }
            else
            {
                textBox7.Text = "0.00"; // Reset if inputs are invalid
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();



        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

