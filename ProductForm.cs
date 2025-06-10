using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Runtime.Caching;

namespace DeliveryApp
{
    public partial class ProductForm : Form
    {
        private readonly string connectionString = @"Server=LAPTOP-EKC9LDBK\PANNNTASTIC;Database=pabd;Trusted_Connection=True;";
        private readonly MemoryCache _cache = MemoryCache.Default;
        private readonly CacheItemPolicy _policy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
        };

        private const string CACHE_KEY_PRODUCTS = "ProductsData";
        public ProductForm()
        {
            InitializeComponent();
            LoadProductData();
        }

        private void LoadProductData()
        {
            try
            {
                DataTable cachedTable;

                if (_cache.Contains(CACHE_KEY_PRODUCTS))
                {
                    cachedTable = _cache.Get(CACHE_KEY_PRODUCTS) as DataTable;
                }
                else
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("spGetAllProducts", connection);
                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        cachedTable = new DataTable();
                        adapter.Fill(cachedTable);

                        _cache.Set(CACHE_KEY_PRODUCTS, cachedTable, _policy);
                    }
                }

                dataGridViewProducts.DataSource = cachedTable;
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

                    SqlCommand command = new SqlCommand("spInsertProduct", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@product_name", productName);
                    command.Parameters.AddWithValue("@stock_quantity", stockQuantity);

                    SqlParameter resultMessageParam = new SqlParameter("@result_message", SqlDbType.NVarChar, 255)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(resultMessageParam);

                    command.ExecuteNonQuery();

                    string resultMessage = command.Parameters["@result_message"].Value.ToString();
                    MessageBox.Show(resultMessage, "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _cache.Remove(CACHE_KEY_PRODUCTS); // Hapus cache
                    LoadProductData(); // Reload dari DB
                    ClearForm();
                }
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

            string productId = dataGridViewProducts.SelectedRows[0].Cells["ProductID"].Value.ToString();
            string productName = textBoxProductName.Text;
            int stockQuantity = (int)numericUpDownStockQuantity.Value;

            var confirmResult = MessageBox.Show(
                "Are you sure you want to update this Product?",
                "Confirm Update",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmResult == DialogResult.No)
            {
                return;
            }

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

                    SqlCommand command = new SqlCommand("spUpdateProduct", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@product_id", productId);
                    command.Parameters.AddWithValue("@product_name", productName);
                    command.Parameters.AddWithValue("@stock_quantity", stockQuantity);

                    SqlParameter resultMessageParam = new SqlParameter("@result_message", SqlDbType.NVarChar, 255)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(resultMessageParam);

                    command.ExecuteNonQuery();

                    string resultMessage = command.Parameters["@result_message"].Value.ToString();
                    MessageBox.Show(resultMessage, "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _cache.Remove(CACHE_KEY_PRODUCTS); // Hapus cache
                    LoadProductData(); // Reload dari DB
                    ClearForm();
                }
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

            string productId = dataGridViewProducts.SelectedRows[0].Cells["ProductID"].Value.ToString();

            var confirmResult = MessageBox.Show(
                "Are you sure you want to delete this Product?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmResult == DialogResult.No)
            {
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("spDeleteProduct", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@product_id", productId);

                    SqlParameter resultMessageParam = new SqlParameter("@result_message", SqlDbType.NVarChar, 255)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(resultMessageParam);

                    command.ExecuteNonQuery();

                    string resultMessage = command.Parameters["@result_message"].Value.ToString();
                    MessageBox.Show(resultMessage, "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _cache.Remove(CACHE_KEY_PRODUCTS); // Hapus cache
                    LoadProductData(); // Reload dari DB
                }
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
                string productName = dataGridViewProducts.SelectedRows[0].Cells["ProductName"].Value.ToString();
                int stockQuantity = int.Parse(dataGridViewProducts.SelectedRows[0].Cells["StockQuantity"].Value.ToString());

                textBoxProductName.Text = productName;
                numericUpDownStockQuantity.Value = stockQuantity;
            }
        }

        private void ClearForm()
        {
            textBoxProductName.Clear();
            numericUpDownStockQuantity.Value = 0;
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                _cache.Remove(CACHE_KEY_PRODUCTS);
                LoadProductData();
                ClearForm();
                MessageBox.Show("Product data refreshed from database.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing product data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
