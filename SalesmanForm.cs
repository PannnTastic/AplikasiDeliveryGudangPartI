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
    public partial class SalesmanForm : Form
    {
        private readonly string connectionString = @"Server=LAPTOP-EKC9LDBK\PANNNTASTIC;Database=pabd;Trusted_Connection=True;";
        private readonly MemoryCache _cache = MemoryCache.Default;
        private readonly CacheItemPolicy _policy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5) // Cache berlaku 5 menit
        };
        private const string CACHE_KEY_SALESMAN = "SalesmanData";
        public SalesmanForm()
        {
            InitializeComponent();
            LoadSalesmanData();
        }

        private void LoadSalesmanData()
        {
            try
            {
                DataTable cachedTable;

                if (_cache.Contains(CACHE_KEY_SALESMAN))
                {
                    cachedTable = _cache.Get(CACHE_KEY_SALESMAN) as DataTable;
                }
                else
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("spGetAllSalesman", connection);
                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        cachedTable = new DataTable();
                        adapter.Fill(cachedTable);

                        _cache.Set(CACHE_KEY_SALESMAN, cachedTable, _policy);
                    }
                }

                dataGridViewSalesman.DataSource = cachedTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading salesman data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            string fullName = textBoxSalesmanName.Text;
            string phone = textBoxPhone.Text;

            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Please enter all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("spInsertSalesman", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@full_name", fullName);
                    command.Parameters.AddWithValue("@phone", phone);

                    SqlParameter resultMessageParam = new SqlParameter("@result_message", SqlDbType.NVarChar, 255)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(resultMessageParam);

                    command.ExecuteNonQuery();

                    string resultMessage = command.Parameters["@result_message"].Value.ToString();
                    MessageBox.Show(resultMessage, "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadSalesmanData(); // Refresh data
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding salesman: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewSalesman.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a salesman to update.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirmResult = MessageBox.Show(
                "Are you sure you want to update this Salesman Data?",
                "Confirm Update",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmResult == DialogResult.No)
            {
                return;
            }

            string salesmanId = dataGridViewSalesman.SelectedRows[0].Cells["SalesmanID"].Value.ToString();
            string fullName = textBoxSalesmanName.Text;
            string phone = textBoxPhone.Text;

            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Please enter all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("spUpdateSalesman", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@salesman_id", salesmanId);
                    command.Parameters.AddWithValue("@full_name", fullName);
                    command.Parameters.AddWithValue("@phone", phone);

                    SqlParameter resultMessageParam = new SqlParameter("@result_message", SqlDbType.NVarChar, 255)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(resultMessageParam);

                    command.ExecuteNonQuery();

                    string resultMessage = command.Parameters["@result_message"].Value.ToString();
                    MessageBox.Show(resultMessage, "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadSalesmanData(); // Refresh data
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating salesman: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewSalesman.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a salesman to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirmResult = MessageBox.Show(
                "Are you sure you want to delete this Salesman Data?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmResult == DialogResult.No)
            {
                return;
            }

            string salesmanId = dataGridViewSalesman.SelectedRows[0].Cells["SalesmanID"].Value.ToString();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("spDeleteSalesman", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@salesman_id", salesmanId);

                    SqlParameter resultMessageParam = new SqlParameter("@result_message", SqlDbType.NVarChar, 255)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(resultMessageParam);

                    command.ExecuteNonQuery();

                    string resultMessage = command.Parameters["@result_message"].Value.ToString();
                    MessageBox.Show(resultMessage, "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadSalesmanData(); // Refresh data
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting salesman: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void DataGridViewSalesman_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewSalesman.SelectedRows.Count > 0)
            {
                string fullName = dataGridViewSalesman.SelectedRows[0].Cells["FullName"].Value.ToString();
                string phone = dataGridViewSalesman.SelectedRows[0].Cells["PhoneNumber"].Value.ToString();

                textBoxSalesmanName.Text = fullName;
                textBoxPhone.Text = phone;
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                _cache.Remove(CACHE_KEY_SALESMAN); // Hapus cache
                LoadSalesmanData();
                ClearForm();
                MessageBox.Show("Salesman data refreshed.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing salesman data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            textBoxSalesmanName.Clear();
            textBoxPhone.Clear();
        }

        private void buttonExportCsv_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("spGetAllSalesman", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
                        saveFileDialog.FileName = "LaporanSalesman.csv";

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                            {
                                // Header
                                sw.WriteLine(string.Join(",", dataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName)));

                                // Data rows
                                foreach (DataRow row in dataTable.Rows)
                                {
                                    sw.WriteLine(string.Join(",", row.ItemArray.Select(field => field.ToString().Replace(",", ";"))));
                                }
                            }

                            MessageBox.Show("Laporan berhasil diekspor ke CSV!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting to CSV: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonExportPdf_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("spGetAllSalesman", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                        saveFileDialog.FileName = "LaporanSalesman.pdf";

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            Document document = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(saveFileDialog.FileName, FileMode.Create));
                            document.Open();

                            Paragraph title = new Paragraph("Laporan Daftar Salesman\n\n", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.BOLD));
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

        private void SalesmanForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Delivery delivery = new Delivery();
            delivery.Show();
            this.Hide(); // Sembunyikan form SalesmanForm saat menutupnya, agar tidak menghapusnya dari memori
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string searchText = searchBox.Text.Trim().ToLower();
                
                if (string.IsNullOrEmpty(searchText))
                {
                    // If search box is empty, show all data
                    if (dataGridViewSalesman.DataSource is DataTable originalTable)
                    {
                        originalTable.DefaultView.RowFilter = string.Empty;
                    }
                    else
                    {
                        // If somehow the DataSource is not set, reload data
                        LoadSalesmanData();
                    }
                }
                else
                {
                    // Get the data source
                    DataTable dataTable = (DataTable)dataGridViewSalesman.DataSource;
                    if (dataTable != null)
                    {
                        // Filter by FullName or PhoneNumber containing the search text
                        string filterExpression = $"FullName LIKE '%{searchText}%' OR PhoneNumber LIKE '%{searchText}%'";
                        dataTable.DefaultView.RowFilter = filterExpression;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching data: {ex.Message}", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
