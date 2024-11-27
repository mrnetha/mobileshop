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
    public partial class AddEmployeeUser : Form
    {
        public AddEmployeeUser()
        {
            InitializeComponent();
        }
        string constring = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MobileShopDB;Integrated Security=True";

        private void shopnamelabel_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void searchbutton_Click(object sender, EventArgs e)
        {
            string Username = textBox1.Text;
            string Password = textBox2.Text;
            string Role = textBox3.Text;

            int Admin = radioButton1.Checked ? 1 : 0;

          
            string query = "insert into Employe(Username,Password,Role,Admin)values(@Username,@Password,@Role,@Admin)";
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Username", Username);
                cmd.Parameters.AddWithValue("@Password", Password);
                cmd.Parameters.AddWithValue("@Role", Role);
                cmd.Parameters.AddWithValue("@Admin", Admin);

                cmd.ExecuteNonQuery();
                MessageBox.Show("user added succesfully");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();

                radioButton1.Checked = false;
                radioButton2.Checked = false;
                



            }


        }

        private void backbutton_Click(object sender, EventArgs e)
        {
           
            Homepage homepage = new Homepage(LoginForm.IsAdmin);
            homepage.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void AddEmployeeUser_Load(object sender, EventArgs e)
        {
            PageLoad();
        }
        public void PageLoad()
        {
            string query = "select Username,Password,Role,Admin from Employe";
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, con);

                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();

            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }
    }
}
