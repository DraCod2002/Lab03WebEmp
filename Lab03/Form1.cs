using System.Data;
using System.Data.SqlClient;

namespace Lab03
{
    public partial class Form1 : Form
    {
        private object command;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)

        {
            try
            {
                string cadena = "Server=DESKTOP-K4C4NU8\\SQLEXPRESSS; Database=lab03DBB; Integrated Security=True";
                SqlConnection connection = new SqlConnection(cadena);
                connection.Open();
                SqlCommand comand = new SqlCommand("SELECT * FROM Products", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(comand);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dgv.DataSource = dataTable;

                MessageBox.Show("Conexión exitosa");

            }
            catch (Exception)
            {
                MessageBox.Show("Error de conecion ");
                throw;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string cadena = "Server=DESKTOP-K4C4NU8\\SQLEXPRESSS; Database=lab03DBB; Integrated Security=True";
            SqlConnection connection = new SqlConnection(cadena);
            connection.Open();
            SqlCommand comand = new SqlCommand("SELECT * FROM Products", connection);
            List<Product> productos = new List<Product>();
            SqlDataReader reader = comand.ExecuteReader();
            while (reader.Read())
            {
                Product producto = new Product
                {
                    ProductId = Convert.ToInt32(reader["ProductId"]),
                    Name = reader["Name"].ToString(),
                    Price = Convert.ToDecimal(reader["Price"])
                };

                productos.Add(producto);
            }
            dgv.DataSource = productos;

        }
    }
}
