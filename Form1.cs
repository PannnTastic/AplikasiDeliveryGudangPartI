using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DeliveryApp
{
    public partial class Form1 : Form
    {
        private readonly string connectionString = @"Server=LAPTOP-EKC9LDBK\PANNNTASTIC;Database=pabd;Trusted_Connection=True;";

        public Form1()
        {
            InitializeComponent();
            LoadSalesmanData();
            LoadProductData();
            LoadDeliveryData();
        }

        private void LoadSalesmanData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT salesman_id, full_name FROM salesman", connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBoxSalesmanId.Items.Add($"{reader["salesman_id"]} - {reader["full_name"]}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading salesman data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProductData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT product_id, product_name FROM products", connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBoxProductId.Items.Add($"{reader["product_id"]} - {reader["product_name"]}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading product data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDeliveryData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM delivery", connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridViewDelivery.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading delivery data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonSubmit_Click(object sender, EventArgs e)
        {
            DateTime deliveryDate = dateTimePickerDeliveryDate.Value;
            string salesmanId = comboBoxSalesmanId.SelectedItem?.ToString()?.Split('-')[0].Trim();
            string productId = comboBoxProductId.SelectedItem?.ToString()?.Split('-')[0].Trim();
            int quantity = (int)numericUpDownQuantity.Value;


            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO delivery ( delivery_date, salesman_id, product_id, quantity) VALUES ( @deliveryDate, @salesmanId, @productId, @quantity)", connection);
                    
                    command.Parameters.AddWithValue("@deliveryDate", deliveryDate);
                    command.Parameters.AddWithValue("@salesmanId", salesmanId);
                    command.Parameters.AddWithValue("@productId", productId);
                    command.Parameters.AddWithValue("@quantity", quantity);
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Delivery record added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDeliveryData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding delivery record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            
            DateTime deliveryDate = dateTimePickerDeliveryDate.Value;
            string salesmanId = comboBoxSalesmanId.SelectedItem?.ToString()?.Split('-')[0].Trim();
            string productId = comboBoxProductId.SelectedItem?.ToString()?.Split('-')[0].Trim();
            int quantity = (int)numericUpDownQuantity.Value;

            

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UPDATE delivery SET delivery_date = @deliveryDate, salesman_id = @salesmanId, product_id = @productId, quantity = @quantity WHERE delivery_id = @deliveryId", connection);
                    
                    command.Parameters.AddWithValue("@deliveryDate", deliveryDate);
                    command.Parameters.AddWithValue("@salesmanId", salesmanId);
                    command.Parameters.AddWithValue("@productId", productId);
                    command.Parameters.AddWithValue("@quantity", quantity);
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Delivery record updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDeliveryData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating delivery record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
           

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("DELETE FROM delivery WHERE delivery_id = @deliveryId", connection);
                    
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Delivery record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDeliveryData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting delivery record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDeliveryData();
                MessageBox.Show("Data refreshed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ButtonOpenSalesmanForm_Click(object sender, EventArgs e)
        {
            SalesmanForm salesmanForm = new SalesmanForm();
            salesmanForm.Show();
        }

        private void ButtonOpenProductForm_Click(object sender, EventArgs e)
        {
            ProductForm productForm = new ProductForm();
            productForm.Show();
        }
        private void DataGridViewDelivery_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewDelivery.SelectedRows.Count > 0)
            {
                // Ambil nilai dari kolom delivery_id
                string deliveryId = dataGridViewDelivery.SelectedRows[0].Cells["delivery_id"].Value.ToString();
                string deliveryDate = dataGridViewDelivery.SelectedRows[0].Cells["delivery_date"].Value.ToString();
                string salesmanId = dataGridViewDelivery.SelectedRows[0].Cells["salesman_id"].Value.ToString();
                string productId = dataGridViewDelivery.SelectedRows[0].Cells["product_id"].Value.ToString();
                string quantity = dataGridViewDelivery.SelectedRows[0].Cells["quantity"].Value.ToString();

                // Isi field input dengan data dari baris yang dipilih
                
                dateTimePickerDeliveryDate.Value = DateTime.Parse(deliveryDate);
                comboBoxSalesmanId.SelectedItem = $"{salesmanId} - {GetSalesmanName(salesmanId)}";
                comboBoxProductId.SelectedItem = $"{productId} - {GetProductName(productId)}";
                numericUpDownQuantity.Value = int.Parse(quantity);
            }
        }

        // Helper untuk mendapatkan nama salesman berdasarkan ID
        private string GetSalesmanName(string salesmanId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT full_name FROM salesman WHERE salesman_id = @salesmanId", connection);
                command.Parameters.AddWithValue("@salesmanId", salesmanId);
                return command.ExecuteScalar()?.ToString() ?? string.Empty;
            }
        }

        // Helper untuk mendapatkan nama produk berdasarkan ID
        private string GetProductName(string productId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT product_name FROM products WHERE product_id = @productId", connection);
                command.Parameters.AddWithValue("@productId", productId);
                return command.ExecuteScalar()?.ToString() ?? string.Empty;
            }
        }

    }
}
