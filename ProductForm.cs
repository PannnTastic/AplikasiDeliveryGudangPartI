using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

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

        private void buttonExportPdf_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("spGetAllProducts", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                        saveFileDialog.FileName = "LaporanProduk.pdf";

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            Document document = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(saveFileDialog.FileName, FileMode.Create));
                            document.Open();

                            Paragraph title = new Paragraph("Laporan Daftar Produk\n\n", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.BOLD));
                            title.Alignment = Element.ALIGN_CENTER;
                            document.Add(title);

                            PdfPTable pdfTable = new PdfPTable(dataTable.Columns.Count);
                            pdfTable.WidthPercentage = 100;
                            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            // Header
                            foreach (DataColumn column in dataTable.Columns)
                            {
                                PdfPCell headerCell = new PdfPCell(new Phrase(column.ColumnName));
                                headerCell.BackgroundColor = new BaseColor(System.Drawing.Color.LightGray);
                                pdfTable.AddCell(headerCell);
                            }

                            // Isi baris
                            foreach (DataRow row in dataTable.Rows)
                            {
                                foreach (var item in row.ItemArray)
                                {
                                    pdfTable.AddCell(item?.ToString() ?? "");
                                }
                            }

                            document.Add(pdfTable);
                            document.Close();

                            MessageBox.Show("Laporan berhasil diekspor ke PDF!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting to PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonExportCsv_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("spGetAllProducts", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
                        saveFileDialog.FileName = "LaporanProduk.csv";

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                            {
                                // Header
                                sw.WriteLine(string.Join(",", dataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName)));

                                // Data
                                foreach (DataRow row in dataTable.Rows)
                                {
                                    sw.WriteLine(string.Join(",", row.ItemArray.Select(field => field.ToString().Replace(",", ";"))));
                                }
                            }

                            MessageBox.Show("Laporan produk berhasil diekspor ke CSV!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting to CSV: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
