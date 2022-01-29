using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpToSqlLib {
    
    public class GLAccountsController {

        public Connection Connection { get; set; }

        //insert new data into table
        public bool Insert(GLAccount gla) {
            /*var Sql = $" INSERT GLAccounts " +
                        $" (AccountNo, AccountDescription) " +
                        $" VALUES " +
                        $" ({gla.AccountNo}, '{gla.AccountDescription}');";*/

            //Or use SQL parameter
            var Sql = $" INSERT GLAccounts " +
                        $" (AccountNo, AccountDescription) " +
                        $" VALUES " +
                        $" (@AccountNo, @AccountDescription);";
            var SqlCmd = new SqlCommand(Sql, Connection.SqlConnection);
            SqlCmd.Parameters.AddWithValue("@AccountNo", gla.AccountNo);
            SqlCmd.Parameters.AddWithValue("@AccountDescription", gla.AccountDescription);
            var rowsAffected = SqlCmd.ExecuteNonQuery();            // returns non-result set
            if (rowsAffected != 1) {
                throw new Exception("Insert failed");
            }
            return true;
        }


        //Update Database
        public bool Update(GLAccount gla) {
            var Sql = $" UPDATE GLAccounts SET " +
                        $" AccountDescription = @AccountDescription " +
                        $" Where AccountNo = @AccountNo; ";
            var SqlCmd = new SqlCommand(Sql, Connection.SqlConnection);
            SqlCmd.Parameters.AddWithValue("@AccountNo", gla.AccountNo);
            SqlCmd.Parameters.AddWithValue("@AccountDescription", gla.AccountDescription);
            var rowsAffected = SqlCmd.ExecuteNonQuery();            // returns non-result set
            if (rowsAffected != 1) {
                throw new Exception("Update failed");
            }
            return true;
        }



        //Delete Database
        public bool Delete(GLAccount gla) {

            var Sql = $" DELETE GLAccounts " +
                        $" Where AccountNo = @AccountNo; ";

            var SqlCmd = new SqlCommand(Sql, Connection.SqlConnection);
            SqlCmd.Parameters.AddWithValue("@AccountNo", gla.AccountNo);
            var rowsAffected = SqlCmd.ExecuteNonQuery();            // returns non-result set
            if (rowsAffected != 1) {
                throw new Exception("Delete failed");
            }
            return true;

        }




        public GLAccount Get(int Id) {
            var Sql = $"SELECT * From GLAccounts Where AccountNo = {Id}";
            var SqlCmd = new SqlCommand(Sql, Connection.SqlConnection);
            var reader = SqlCmd.ExecuteReader();
            if (!reader.HasRows) {
                reader.Close();
                return null;
            }
            reader.Read();
            var gla = new GLAccount();
            gla.AccountNo = Convert.ToInt32(reader["AccountNo"]);
            gla.AccountDescription = reader["AccountDescription"].ToString();
            reader.Close();
            return gla;
        }

        public List<GLAccount> List() {
            var Sql = "SELECT * From GLAccounts;";
            var SqlCmd = new SqlCommand(Sql, Connection.SqlConnection);
            var reader = SqlCmd.ExecuteReader();

            //create generic collection of the class
            var glas = new List<GLAccount>();

            while (reader.Read()) {
                var gla = new GLAccount();
                gla.AccountNo = Convert.ToInt32(reader["AccountNo"]);
                gla.AccountDescription = reader["AccountDescription"].ToString();
                glas.Add(gla);
            }

            reader.Close();
            return glas;
        }
    }
}
