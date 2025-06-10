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
    public partial class FormReportProducts : Form
    {
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
            string connectionString = "Data Source=LAPTOP-EKC9LDBK\\PANNNTASTIC;Initial Catalog=pabd;Integrated Security=True;";

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

            reportViewer1.LocalReport.ReportPath= @"D:\KULIAH\SMT4 (ad matkul smt 6)\PABD\ucp1\ReportProducts.rdlc"; // Ensure the report path is correct
            reportViewer1.RefreshReport(); // Refresh the report viewer to display the report

        }
        
    }
}
