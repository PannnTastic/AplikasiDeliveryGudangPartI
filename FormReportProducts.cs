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
using System.IO;

namespace DeliveryApp
{
    public partial class FormReportProducts : Form
    {
        koneksi kn = new koneksi(); // Instance of the koneksi class to get the connection string
        public FormReportProducts()
        {
            InitializeComponent();
        }

        private void FormReportProducts_Load(object sender, EventArgs e)
        {

            ProductsReport();
            this.reportViewer1.RefreshReport();
        }

        private void ProductsReport()
        {
            string connectionString = kn.ConnectionString(); // Get the connection string from the koneksi class

            string query = @"
                SELECT 
                    product_id AS ProductID, 
                    product_name AS ProductName, 
                    stock_quantity AS StockQuantity,
                CASE WHEN stock_quantity = 0 
                    THEN 'Stok Habis' 
                WHEN stock_quantity <= 10 
                    THEN 'Stok Rendah'
                ELSE 'Stok Aman' 
                END AS StatusStok, 
                  created_at AS CreatedAt, 
                  updated_at AS UpdatedAt
                FROM  products
                ORDER BY UpdatedAt DESC";

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString)) 
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }
            ReportDataSource rds = new ReportDataSource("DataSet1", dt);
            // Set the data source for the report viewer
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            string  reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ReportProducts.rdlc"); // Use relative path to the report file
            reportViewer1.LocalReport.ReportPath= reportPath; // Ensure the report path is correct
            reportViewer1.RefreshReport(); // Refresh the report viewer to display the report

        }

        private void FormReportProducts_FormClosing(object sender, FormClosingEventArgs e)
        {
            Delivery delivery = new Delivery();
            delivery.Show();
            this.Hide(); // Hide the current form instead of closing it
        }
    }
}
