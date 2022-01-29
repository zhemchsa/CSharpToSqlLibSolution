using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpToSqlLib {
    public class Connection {
        
        public string ConnectionString { get; set; }
        public SqlConnection SqlConnection { get; set; }

        public void Open(string Database) {
            ConnectionString = $"server = localhost\\sqlexpress;" +
                                $"database = {Database};" +
                                $"trustServerCertificate=true;" +
                                $"trusted_connection=true;";
            SqlConnection = new SqlConnection(ConnectionString);
            SqlConnection.Open();
            if(SqlConnection.State != System.Data.ConnectionState.Open) {
                throw new Exception("Connection did not open!");
            }
        }

        public void Close() {
            SqlConnection.Close();
        }
    }
}
