using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;

namespace DeliveryApp
{
    public partial class FormReportSalesman : Form
    {
        public FormReportSalesman()
        {
            InitializeComponent();
        }

        private void FormReportSalesman_Load(object sender, EventArgs e)
        {
            SalesmanReport();
            this.reportViewer1.RefreshReport();

        }

        private void SalesmanReport()
        {
            string connectionString = "Data Source=LAPTOP-EKC9LDBK\\PANNNTASTIC;Initial Catalog=pabd;Integrated Security=True;";
            string query = @"
               SELECT 
                    s.salesman_id AS SalesmanID,
                    s.full_name AS SalesmanName,
                    COUNT(CASE WHEN d.delivery_date < CAST(GETDATE() AS DATE) THEN 1 END) AS JumlahDeliverySelesai,
                    COUNT(CASE WHEN d.delivery_date >= CAST(GETDATE() AS DATE) THEN 1 END) AS JumlahDeliveryAkanDatang,
                    MAX(s.updated_at) AS LastUpdatedAt
                FROM 
                    salesman s
                LEFT JOIN 
                    delivery d ON s.salesman_id = d.salesman_id
                GROUP BY 
                    s.salesman_id, 
                    s.full_name
                ORDER BY 
                    JumlahDeliverySelesai DESC, 
                    JumlahDeliveryAkanDatang DESC;";

            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                dataAdapter.Fill(dataTable);
            }

            ReportDataSource rds = new ReportDataSource("DataSet1", dataTable);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.LocalReport.ReportPath = @"D:\KULIAH\SMT4 (ad matkul smt 6)\PABD\ucp1\ReportSalesman.rdlc"; // Ensure the report path is correct
            reportViewer1.RefreshReport();
        }

        private void FormReportSalesman_FormClosing(object sender, FormClosingEventArgs e)
        {
            Delivery delivery = new Delivery();
            delivery.Show();
            this.Hide(); // Hide the report form instead of closing it
        }
    }
}
