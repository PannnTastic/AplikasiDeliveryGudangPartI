using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Runtime.Caching;

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

            string salesmanId = dataGridViewSalesman.SelectedRows[0].Cells["Salesman ID"].Value.ToString();

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
    }
}
