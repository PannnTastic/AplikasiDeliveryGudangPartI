using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using Microsoft.ReportingServices.Interfaces;

namespace DeliveryApp
{
    public partial class FormReportDelivery : Form
    {
        public FormReportDelivery()
        {
            InitializeComponent();
        }

        private void FormReportDelivery_Load(object sender, EventArgs e)
        {
            DeliveryReport();
            this.reportViewer1.RefreshReport();
        }

        private void DeliveryReport()
        {
            string connectionString = "Data Source=LAPTOP-EKC9LDBK\\PANNNTASTIC;Initial Catalog=pabd;Integrated Security=True;";

            string query = @"
                   SELECT 
                    d.delivery_id AS DeliveryID, 
                    d.delivery_date AS DeliveryDate, 
                    s.full_name AS SalesmanName, 
                    p.product_name AS ProductName, 
                    d.quantity AS Quantity, 
                   CASE WHEN d .delivery_date < CAST(GETDATE() AS DATE) 
                          THEN 'Telah Berlalu' 
                    WHEN CAST(d .delivery_date AS DATE) = CAST(GETDATE() AS DATE) 
                          THEN 'Hari Ini'
                    WHEN CAST(d .delivery_date AS DATE) = CAST(DATEADD(DAY, 1, GETDATE()) AS DATE) 
                          THEN 'Besok' 
                    WHEN DATEDIFF(DAY, GETDATE(), d .delivery_date) <= 7  
                           THEN 'Dalam ' + CAST(DATEDIFF(DAY, GETDATE(), d .delivery_date) AS VARCHAR(5)) + ' Hari' ELSE 'Lebih dari 7 Hari' END AS KeteranganWaktu, DATEDIFF(HOUR, GETDATE(), d.delivery_date) 
                          AS SisaJam, DATEDIFF(MINUTE, GETDATE(), d.delivery_date) AS SisaMenit
                    FROM  delivery AS d 
                    INNER JOIN
                          salesman AS s ON d.salesman_id = s.salesman_id INNER JOIN
                          products AS p ON d.product_id = p.product_id
                    ORDER BY DeliveryDate DESC";

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString)) 
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }

            ReportDataSource rds = new ReportDataSource("DataSet1",dt);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            reportViewer1.LocalReport.ReportPath = @"D:\KULIAH\SMT4 (ad matkul smt 6)\PABD\ucp1\ReportDelivery.rdlc";
            reportViewer1.RefreshReport();


        }

        private void FormReportDelivery_FormClosing(object sender, FormClosingEventArgs e)
        {
            Delivery delivery = new Delivery();
            delivery.Show();
            this.Hide();
        }
    }
}
