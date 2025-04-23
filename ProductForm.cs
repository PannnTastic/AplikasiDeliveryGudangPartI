using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DeliveryApp
{
    public partial class ProductForm : Form
    {
        private readonly string connectionString = @"Server=LAPTOP-EKC9LDBK\PANNNTASTIC;Database=pabd;Trusted_Connection=True;";

        public ProductForm()
        {
            InitializeComponent();
            LoadProductData();
        }

        private void LoadProductData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT product_id AS 'Product ID', product_name AS 'Product Name', stock_quantity AS 'Stock Quantity' FROM products", connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridViewProducts.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading product data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            string productName = textBoxProductName.Text;
            int stockQuantity = (int)numericUpDownStockQuantity.Value;

            if (string.IsNullOrWhiteSpace(productName))
            {
                MessageBox.Show("Please enter a product name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO products (product_name, stock_quantity) VALUES (@productName, @stockQuantity)", connection);
                    command.Parameters.AddWithValue("@productName", productName);
                    command.Parameters.AddWithValue("@stockQuantity", stockQuantity);
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Product added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadProductData();
                textBoxProductName.Clear();
                numericUpDownStockQuantity.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to update.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string productId = dataGridViewProducts.SelectedRows[0].Cells["Product ID"].Value.ToString();
            string productName = textBoxProductName.Text;
            int stockQuantity = (int)numericUpDownStockQuantity.Value;

            if (string.IsNullOrWhiteSpace(productName))
            {
                MessageBox.Show("Please enter a product name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UPDATE products SET product_name = @productName, stock_quantity = @stockQuantity WHERE product_id = @productId", connection);
                    command.Parameters.AddWithValue("@productId", productId);
                    command.Parameters.AddWithValue("@productName", productName);
                    command.Parameters.AddWithValue("@stockQuantity", stockQuantity);
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Product updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadProductData();
                textBoxProductName.Clear();
                numericUpDownStockQuantity.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string productId = dataGridViewProducts.SelectedRows[0].Cells["Product ID"].Value.ToString();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("DELETE FROM products WHERE product_id = @productId", connection);
                    command.Parameters.AddWithValue("@productId", productId);
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Product deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadProductData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void DataGridViewProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count > 0)
            {
                string productName = dataGridViewProducts.SelectedRows[0].Cells["Product Name"].Value.ToString();
                int stockQuantity = int.Parse(dataGridViewProducts.SelectedRows[0].Cells["Stock Quantity"].Value.ToString());

                textBoxProductName.Text = productName;
                numericUpDownStockQuantity.Value = stockQuantity;
            }
        }
    }
}
