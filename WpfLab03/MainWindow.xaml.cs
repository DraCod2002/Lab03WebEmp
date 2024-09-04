using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace WpfLab03
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Función para conectar y obtener los productos usando DataTable (desconectado)
        private DataTable GetProductosUsingDataTable()
        {
            string cadena = "Server=DESKTOP-K4C4NU8\\SQLEXPRESS; Database=lab03DBB; Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(cadena))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT ProductId, Name, Price FROM Products", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        // Función para conectar y obtener los productos usando SqlDataReader (conectado)
        private List<Product> GetProductosUsingReader(string nombre)
        {
            List<Product> productos = new List<Product>();
            string cadena = "Server=DESKTOP-K4C4NU8\\SQLEXPRESS; Database=lab03DBB; Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(cadena))
            {
                connection.Open();
                string query = "SELECT ProductId, Name, Price FROM Products WHERE Name LIKE @nombre";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@nombre", $"%{nombre}%");
                SqlDataReader reader = command.ExecuteReader();

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
            }
            return productos;
        }

        // Evento para el botón Buscar
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(nombre))
            {
                // Usamos la función que retorna una lista de productos usando SqlDataReader
                List<Product> productos = GetProductosUsingReader(nombre);
                dgProductos.ItemsSource = productos;  // Asignar la lista al DataGrid
            }
            else
            {
                // Si no se ingresa nada, mostramos todos los productos
                dgProductos.ItemsSource = GetProductosUsingDataTable().DefaultView;
            }
        }
    }

    // Clase Product para representar los productos
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
