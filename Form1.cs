﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace DeliveryApp
{
    public partial class Delivery : Form
    {
        koneksi kn = new koneksi();
       
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
            numericUpDownQuantity.Maximum = 999999;
            // Ambil string koneksi dari kelas koneksi
            EnsureIndexes(); // Pastikan index sudah dibuat saat form diinisialisasi
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
                    using (SqlConnection connection = new SqlConnection(kn.ConnectionString()))
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
                    using (SqlConnection connection = new SqlConnection(kn.ConnectionString()))
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
                    using (SqlConnection connection = new SqlConnection(kn.ConnectionString()))
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
            string salesmanId = comboBoxSalesmanId.SelectedValue?.ToString()?.Split('-')[0].Trim();
            string productId = comboBoxProductId.SelectedValue?.ToString()?.Split('-')[0].Trim();
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
                using (SqlConnection connection = new SqlConnection(kn.ConnectionString()))
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
            string salesmanId = comboBoxSalesmanId.SelectedValue?.ToString()?.Split('-')[0].Trim();
            string productId = comboBoxProductId.SelectedValue?.ToString()?.Split('-')[0].Trim();
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
                using (SqlConnection connection = new SqlConnection(kn.ConnectionString()))
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
                using (SqlConnection connection = new SqlConnection(kn.ConnectionString()))
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
            this.Hide(); // Sembunyikan form Delivery saat SalesmanForm dibuka
        }

        private void ButtonOpenProductForm_Click(object sender, EventArgs e)
        {
            ProductForm productForm = new ProductForm();
            productForm.Show();
            this.Hide(); // Sembunyikan form Delivery saat ProductForm dibuka
        }

        private void ButtonOpenReportDelivery_Click(object sender, EventArgs e)
        {
            FormReportDelivery delivery = new FormReportDelivery();
            delivery.Show();
            this.Hide(); // Sembunyikan form Delivery saat FormReportDelivery dibuka
        }

        private void ButtonOpenReportProducts_Click(object sender, EventArgs e)
        {
            FormReportProducts products = new FormReportProducts();
            products.Show();
            this.Hide(); // Sembunyikan form Delivery saat FormReportProducts dibuka
        }
        private void ButtonOpenReportSalesman_Click(object sender, EventArgs e)
        {
            FormReportSalesman salesman = new FormReportSalesman();
            salesman.Show();
            this.Hide(); // Sembunyikan form Delivery saat FormReportSalesman dibuka
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
                using (SqlConnection connection = new SqlConnection(kn.ConnectionString()))
                {
                    connection.Open();

                    // Gunakan Stored Procedure
                    SqlCommand command = new SqlCommand("spGetAllDeliveries", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
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
                                // Tulis header CSV berdasarkan kolom hasil SP
                                sw.WriteLine(string.Join(",", dataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName)));

                                // Tulis baris data
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
                using (SqlConnection connection = new SqlConnection(kn.ConnectionString()))
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
            string heavyQuery = "EXEC spGetAllProducts";
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

        private void buttonExportPdf_Click(object sender, EventArgs e)
        {
            try
            {
                // Pilih lokasi simpan file PDF
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF Files|*.pdf";
                saveFileDialog.Title = "Save Data as PDF";
                saveFileDialog.FileName = "DeliveryReport.pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Document document = new Document(PageSize.A4, 10f, 10f, 10f, 10f);

                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(saveFileDialog.FileName, FileMode.Create));
                    document.Open();

                    // Judul laporan
                    Paragraph title = new Paragraph("Laporan Pengiriman\n\n", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.BOLD));
                    title.Alignment = Element.ALIGN_CENTER;
                    document.Add(title);

                    // Buat tabel PDF sesuai jumlah kolom di DataGridView
                    PdfPTable pdfTable = new PdfPTable(dataGridViewDelivery.Columns.Count);
                    pdfTable.DefaultCell.Padding = 3;
                    pdfTable.WidthPercentage = 100;
                    pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                    // Tambahkan header
                    foreach (DataGridViewColumn column in dataGridViewDelivery.Columns)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                        cell.BackgroundColor = new BaseColor(System.Drawing.Color.LightGray);
                        pdfTable.AddCell(cell);
                    }

                    // Tambahkan baris
                    foreach (DataGridViewRow row in dataGridViewDelivery.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            pdfTable.AddCell(cell.Value?.ToString() ?? "");
                        }
                    }

                    // Tambahkan tabel ke dokumen
                    document.Add(pdfTable);

                    // Tutup dokumen
                    document.Close();

                    MessageBox.Show("Data berhasil diekspor ke PDF!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saat ekspor ke PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void EnsureIndexes()
        {
            string createIndexesScript = @"
                    IF OBJECT_ID('delivery', 'U') IS NOT NULL
            BEGIN
                IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_delivery_date')
                CREATE NONCLUSTERED INDEX IX_delivery_date ON delivery(delivery_date DESC)

                IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_delivery_salesman')
                CREATE NONCLUSTERED INDEX IX_delivery_salesman ON delivery(salesman_id)

                IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_delivery_product')
                CREATE NONCLUSTERED INDEX IX_delivery_product ON delivery(product_id)

                IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_delivery_report')
                CREATE NONCLUSTERED INDEX IX_delivery_report ON delivery(delivery_date, salesman_id, product_id)
            END

            IF OBJECT_ID('products', 'U') IS NOT NULL
            BEGIN
                IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_products_name')
                CREATE NONCLUSTERED INDEX IX_products_name ON products(product_name)

                IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_products_stock')
                CREATE NONCLUSTERED INDEX IX_products_stock ON products(stock_quantity)
            END

            IF OBJECT_ID('salesman', 'U') IS NOT NULL
            BEGIN
                IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_salesman_name')
                CREATE NONCLUSTERED INDEX IX_salesman_name ON salesman(full_name)

                IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_salesman_phone')
                CREATE NONCLUSTERED INDEX IX_salesman_phone ON salesman(phone)
            END";

            try
            {
                using (SqlConnection conn = new SqlConnection(kn.ConnectionString()))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand(createIndexesScript, conn);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal membuat index: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewDelivery_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewDelivery.SelectedRows.Count > 0)
            {
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

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            string searchText = searchBox.Text.Trim().ToLower();
            
            try
            {
                if (string.IsNullOrEmpty(searchText))
                {
                    // If search text is empty, reload all data
                    LoadDeliveryData();
                    return;
                }

                // Get the current data source
                DataTable dataTable = dataGridViewDelivery.DataSource as DataTable;
                if (dataTable == null)
                {
                    return;
                }

                // Create a filtered view of the data
                dataTable.DefaultView.RowFilter = string.Format(
                    "CONVERT(delivery_id, 'System.String') LIKE '%{0}%' OR " +
                    "CONVERT(delivery_date, 'System.String') LIKE '%{0}%' OR " +
                    "CONVERT(salesman_id, 'System.String') LIKE '%{0}%' OR " +
                    "CONVERT(salesman_name, 'System.String') LIKE '%{0}%' OR " +
                    "CONVERT(product_id, 'System.String') LIKE '%{0}%' OR " +
                    "CONVERT(product_name, 'System.String') LIKE '%{0}%' OR " +
                    "CONVERT(quantity, 'System.String') LIKE '%{0}%'",
                    searchText.Replace("'", "''"));

                // Apply the filtered view to the DataGridView
                dataGridViewDelivery.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Search error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
