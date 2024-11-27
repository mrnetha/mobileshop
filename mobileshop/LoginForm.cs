using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace mobileshop
{
    public partial class LoginForm : Form
    {
        public static bool IsAdmin { get; private set; }
        public LoginForm()
        {
           
            InitializeComponent();
            
        }
  

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Enter User Name and Password");
            }
            else
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MobileShopDB;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Admin FROM Employe WHERE Username = @Username AND Password = @Password";
                    SqlCommand cmd=new SqlCommand(query, connection);

                    cmd.Parameters.AddWithValue("@Username", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Password", textBox2.Text);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();  // Read the first row
                        bool isAdmin = reader["Admin"].ToString() == "1";
                        MessageBox.Show("Login Successful");
                        

                        IsAdmin = isAdmin;

                        // Pass the isAdmin flag to the Homepage
                        Homepage homepage = new Homepage(isAdmin);
                        homepage.Show();
                        this.Hide();


                    }
                    else
                    {
                        MessageBox.Show("Invalid Credentials");
                    }

                }


            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
