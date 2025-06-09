using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DeliveryApp
{
    public partial class SalesmanForm : Form
    {
        private readonly string connectionString = @"Server=LAPTOP-EKC9LDBK\PANNNTASTIC;Database=pabd;Trusted_Connection=True;";

        public SalesmanForm()
        {
            InitializeComponent();
            LoadSalesmanData();
        }

        private void LoadSalesmanData()
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

                    dataGridViewSalesman.DataSource = dataTable;
                }
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
                    SqlCommand command = new SqlCommand("INSERT INTO salesman (full_name, phone) VALUES (@fullName, @phone)", connection);
                    command.Parameters.AddWithValue("@fullName", fullName);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Salesman added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadSalesmanData();
                textBoxSalesmanName.Clear();
                textBoxPhone.Clear();
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

            string salesmanId = dataGridViewSalesman.SelectedRows[0].Cells["Salesman ID"].Value.ToString();
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
                    SqlCommand command = new SqlCommand("UPDATE salesman SET full_name = @fullName, phone = @phone WHERE salesman_id = @salesmanId", connection);
                    command.Parameters.AddWithValue("@salesmanId", salesmanId);
                    command.Parameters.AddWithValue("@fullName", fullName);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Salesman updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadSalesmanData();
                textBoxSalesmanName.Clear();
                textBoxPhone.Clear();
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

            string salesmanId = dataGridViewSalesman.SelectedRows[0].Cells["Salesman ID"].Value.ToString();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("DELETE FROM salesman WHERE salesman_id = @salesmanId", connection);
                    command.Parameters.AddWithValue("@salesmanId", salesmanId);
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Salesman deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadSalesmanData();
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
                string fullName = dataGridViewSalesman.SelectedRows[0].Cells["Full Name"].Value.ToString();
                string phone = dataGridViewSalesman.SelectedRows[0].Cells["Phone"].Value.ToString();

                textBoxSalesmanName.Text = fullName;
                textBoxPhone.Text = phone;
            }
        }


    }
}
