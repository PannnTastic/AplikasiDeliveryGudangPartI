using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.Caching;

namespace DeliveryApp
{
    public partial class Delivery : Form
    {
        private readonly string connectionString = @"Server=LAPTOP-EKC9LDBK\PANNNTASTIC;Database=pabd;Trusted_Connection=True;";

        private readonly MemoryCache _cache = MemoryCache.Default;
        private readonly CacheItemPolicy _policy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5) // Cache berlaku 5 menit
        };

        private const string CACHE_KEY_DELIVERIES = "DeliveryData";
        private const string CACHE_KEY_PRODUCTS = "ProductData";
        private const string CACHE_KEY_SALESMAN = "SalesmanData";

        public Delivery()
        {
            InitializeComponent();
            LoadDeliveryData();
            LoadProductData();
            LoadSalesmanData();
            ClearForm(); // Bersihkan form input saat pertama kali dibuka


            dateTimePickerDeliveryDate.ValueChanged += DateTimePickerDeliveryDate_ValueChanged;
        }

        private void LoadSalesmanData()
        {
            try
            {
                DataTable dt;
                if (_cache.Contains(CACHE_KEY_SALESMAN))
                {
                    dt = _cache.Get(CACHE_KEY_SALESMAN) as DataTable;
                }
                else
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("spGetAllSalesman", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        dt = new DataTable();
                        adapter.Fill(dt);
                        _cache.Set(CACHE_KEY_SALESMAN, dt, _policy);
                    }
                }
                comboBoxSalesmanId.DataSource = dt;
                comboBoxSalesmanId.DisplayMember = "FullName";
                comboBoxSalesmanId.ValueMember = "SalesmanID";
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
                DataTable dt;
                if (_cache.Contains(CACHE_KEY_PRODUCTS))
                {
                    dt = _cache.Get(CACHE_KEY_PRODUCTS) as DataTable;
                }
                else
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("spGetAllProducts", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        dt = new DataTable();
                        adapter.Fill(dt);
                        _cache.Set(CACHE_KEY_PRODUCTS, dt, _policy);
                    }
                }
                comboBoxProductId.DataSource = dt;
                comboBoxProductId.DisplayMember = "ProductName";
                comboBoxProductId.ValueMember = "ProductID";
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
                DataTable dt;

                if (_cache.Contains(CACHE_KEY_DELIVERIES))
                {
                    dt = _cache.Get(CACHE_KEY_DELIVERIES) as DataTable;
                }
                else
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("spGetAllDeliveries", connection);
                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        dt = new DataTable();
                        adapter.Fill(dt);

                        _cache.Set(CACHE_KEY_DELIVERIES, dt, _policy);
                    }
                }

                dataGridViewDelivery.DataSource = dt;
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

            if (deliveryDate < DateTime.Now)
            {
                TimeSpan timeElapsed = DateTime.Now - deliveryDate;
                MessageBox.Show($"Tanggal telah berlalu: {timeElapsed.Days} hari lalu.",
                                "Invalid Date",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("spInsertDelivery", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Parameter input
                    command.Parameters.AddWithValue("@delivery_date", deliveryDate);
                    command.Parameters.AddWithValue("@salesman_id", string.IsNullOrEmpty(salesmanId) ? (object)DBNull.Value : salesmanId);
                    command.Parameters.AddWithValue("@product_id", productId);
                    command.Parameters.AddWithValue("@quantity", quantity);

                    // Output parameter
                    SqlParameter resultMessageParam = new SqlParameter("@result_message", SqlDbType.NVarChar, 255)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(resultMessageParam);

                    command.ExecuteNonQuery();

                    string resultMessage = command.Parameters["@result_message"].Value.ToString();
                    MessageBox.Show(resultMessage, "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _cache.Remove(CACHE_KEY_DELIVERIES); // Hapus cache agar data refresh
                    LoadDeliveryData(); // Reload dengan data terbaru
                    ClearForm(); // Bersihkan form input
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding delivery record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewDelivery.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a delivery record to update.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string deliveryId = dataGridViewDelivery.SelectedRows[0].Cells["delivery_id"].Value.ToString();
            DateTime deliveryDate = dateTimePickerDeliveryDate.Value;
            string salesmanId = comboBoxSalesmanId.SelectedItem?.ToString()?.Split('-')[0].Trim();
            string productId = comboBoxProductId.SelectedItem?.ToString()?.Split('-')[0].Trim();
            int quantity = (int)numericUpDownQuantity.Value;

            var confirmResult = MessageBox.Show(
                "Are you sure you want to update this delivery record?",
                "Confirm Update",
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

                    SqlCommand command = new SqlCommand("spUpdateDelivery", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@delivery_id", deliveryId);
                    command.Parameters.AddWithValue("@delivery_date", deliveryDate);
                    command.Parameters.AddWithValue("@salesman_id", string.IsNullOrEmpty(salesmanId) ? (object)DBNull.Value : salesmanId);
                    command.Parameters.AddWithValue("@product_id", productId);
                    command.Parameters.AddWithValue("@quantity", quantity);

                    SqlParameter resultMessageParam = new SqlParameter("@result_message", SqlDbType.NVarChar, 255)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(resultMessageParam);

                    command.ExecuteNonQuery();

                    string resultMessage = command.Parameters["@result_message"].Value.ToString();
                    MessageBox.Show(resultMessage, "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _cache.Remove(CACHE_KEY_DELIVERIES); // Hapus cache agar data refresh
                    LoadDeliveryData(); // Reload dengan data terbaru
                    ClearForm(); // Bersihkan form input
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating delivery record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewDelivery.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a delivery record to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string deliveryId = dataGridViewDelivery.SelectedRows[0].Cells["delivery_id"].Value.ToString();

            var confirmResult = MessageBox.Show(
                "Are you sure you want to delete this delivery record?",
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

                    SqlCommand command = new SqlCommand("spDeleteDelivery", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@delivery_id", deliveryId);

                    SqlParameter resultMessageParam = new SqlParameter("@result_message", SqlDbType.NVarChar, 255)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(resultMessageParam);

                    command.ExecuteNonQuery();

                    string resultMessage = command.Parameters["@result_message"].Value.ToString();
                    MessageBox.Show(resultMessage, "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _cache.Remove(CACHE_KEY_DELIVERIES); // Hapus cache agar data refresh
                    LoadDeliveryData(); // Reload dengan data terbaru
                    ClearForm(); // Bersihkan form input setelah penghapusan
                }
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
                _cache.Remove(CACHE_KEY_DELIVERIES); // Hapus cache agar data refresh
                LoadDeliveryData(); // Reload dengan data terbaru
                ClearForm(); // Bersihkan form input
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
                comboBoxSalesmanId.SelectedValue = salesmanId;
                comboBoxProductId.SelectedValue = productId;
                numericUpDownQuantity.Value = int.Parse(quantity);
            }
        }

        private void DateTimePickerDeliveryDate_ValueChanged(object sender, EventArgs e)
        {
            // Pastikan tanggal dan waktu yang dipilih adalah saat ini atau di masa depan
            if (dateTimePickerDeliveryDate.Value < DateTime.Now)
            {
                MessageBox.Show("Tanggal dan waktu telah berlalu. Harap pilih tanggal dan waktu yang valid.",
                                "Invalid Date and Time",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                dateTimePickerDeliveryDate.Value = DateTime.Now; // Reset ke tanggal dan waktu saat ini
            }
        }

        private void ButtonExportReport_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM delivery", connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
                        saveFileDialog.Title = "Save Delivery Report";
                        saveFileDialog.FileName = "LaporanPengiriman.csv";
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            string filePath = saveFileDialog.FileName;
                            using (StreamWriter sw = new StreamWriter(filePath))
                            {
                                // Header
                                sw.WriteLine(string.Join(",", dataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName)));

                                // Data rows
                                foreach (DataRow row in dataTable.Rows)
                                {
                                    sw.WriteLine(string.Join(",", row.ItemArray.Select(field => field.ToString().Replace(",", ";"))));
                                }
                            }

                            MessageBox.Show($"Laporan berhasil diekspor ke:\n{filePath}", "Export Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AnalyzeQuery(string sqlQuery)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.InfoMessage += (sender, e) =>
                    {
                        MessageBox.Show(e.Message, "Query Statistics");
                    };

                    connection.Open();

                    string wrappedQuery = $@"
                    SET STATISTICS IO ON;
                    SET STATISTICS TIME ON;
                    {sqlQuery};
                    SET STATISTICS IO OFF;
                    SET STATISTICS TIME OFF;";

                    using (SqlCommand command = new SqlCommand(wrappedQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error analyzing query: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonAnalyzeQuery_Click(object sender, EventArgs e)
        {
            string heavyQuery = "EXEC spGetAllDeliveries";
            AnalyzeQuery(heavyQuery);
        }

        private void ClearForm()
        {
            dateTimePickerDeliveryDate.Value = DateTime.Now;
            comboBoxSalesmanId.SelectedIndex = -1;
            comboBoxProductId.SelectedIndex = -1;
            numericUpDownQuantity.Value = numericUpDownQuantity.Minimum;
            dataGridViewDelivery.ClearSelection();

        }



    }
}
