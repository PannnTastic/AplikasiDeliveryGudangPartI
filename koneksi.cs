using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Data;
using System.Data.SqlClient;

namespace DeliveryApp
{
    internal class koneksi
    {
        public string ConnectionString() // untuk membangun dan mengembalikan string koneksi ke database
        {
            try
            {
                string localIP = GetLocalIPAddress(); // mendapatkan alamat IP lokal
                /*string connectStr = $"Server={localIP}\\PANNNTASTIC;Database=pabd;Trusted_Connection=True;";*/
                string connectStr = $"Server=192.168.85.99\\PANNNTASTIC;Database=deliveryApp;User ID=sa;Password=12345;";
                return connectStr;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
        }

        public static string GetLocalIPAddress() // untuk mengambil IP Address pada PC yang menjalankan aplikasi
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork) // Mengambil IPv4
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Tidak ada alamat IP yang ditemukan.");
        }

        public SqlConnection GetConnection() // untuk membuat objek koneksi ke database
        {
            try
            {
                string connectionString = ConnectionString();
                SqlConnection connection = new SqlConnection(connectionString);
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
