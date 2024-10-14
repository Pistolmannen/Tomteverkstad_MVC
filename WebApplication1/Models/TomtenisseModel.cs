using System.Data;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;

namespace WebApplication1.Models
{
    
    public class TomtenisseModel
    {
        private readonly IConfiguration _configuration;
        private string connectionString;

        public TomtenisseModel(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration["ConnectionString"];
        }

        public DataTable GetTomtenissar() {
            MySqlConnection dbcon = new MySqlConnection(connectionString);
            dbcon.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter("select * from Tomtenisse", dbcon);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "result");
            DataTable tomtenisseTable = ds.Tables["result"];
            dbcon.Close();


            return tomtenisseTable;
        }
    }
}
