using MySql.Data.MySqlClient;
using System.Data;
using System.Xml.Linq;

namespace WebApplication1.Models
{
    public class LeksakModel
    {
        private readonly IConfiguration _configuration;
        private string connectionString;

        public LeksakModel(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration["ConnectionString"];
        }

        public DataTable GetleksakByPrise(int Prise)
        {
            MySqlConnection dbcon = new MySqlConnection(connectionString);
            dbcon.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter("Call getLeksakerPåPris(@inputPrise)", dbcon);
            adapter.SelectCommand.Parameters.AddWithValue("@inputPrise", Prise);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "result");
            DataTable LeksakerTable = ds.Tables["result"];
            dbcon.Close();


            return LeksakerTable;
        }

    }
}
