using System.Data;
using System.Xml.Linq;
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

        public DataTable GetTomtenissarByName(String Name) {
            MySqlConnection dbcon = new MySqlConnection(connectionString);
            dbcon.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter("select * from Tomtenisse where Namn = @inputName", dbcon);
            adapter.SelectCommand.Parameters.AddWithValue("@inputName", Name);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "result");
            DataTable tomtenisseTable = ds.Tables["result"];
            dbcon.Close();


            return tomtenisseTable;
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
        public DataTable UpdateShoeSize(String ShoeSize, String Name, String ID)
        {
            MySqlConnection dbcon = new MySqlConnection(connectionString);
            dbcon.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter("update Tomtenisse set Skostorlek = @inputShoeSize where Namn = @inputName and IdNr = @inputID", dbcon);
            adapter.SelectCommand.Parameters.AddWithValue("@inputShoeSize", ShoeSize);
            adapter.SelectCommand.Parameters.AddWithValue("@inputName", Name);
            adapter.SelectCommand.Parameters.AddWithValue("@inputID", ID);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "result");
            DataTable tomtenisseTable = ds.Tables["result"];
            dbcon.Close();


            return tomtenisseTable;
        }
        public DataTable MakeShoeSizeNull(String Name, String ID)
        {
            MySqlConnection dbcon = new MySqlConnection(connectionString);
            dbcon.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter("update Tomtenisse set Skostorlek = NULL where Namn = @inputName and IdNr = @inputID", dbcon);
            adapter.SelectCommand.Parameters.AddWithValue("@inputName", Name);
            adapter.SelectCommand.Parameters.AddWithValue("@inputID", ID);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "result");
            DataTable tomtenisseTable = ds.Tables["result"];
            dbcon.Close();


            return tomtenisseTable;
        }
    }
}
