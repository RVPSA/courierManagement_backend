using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace DAL
{
    public class DataService : IDataService
    {
        //Variables
        private string _connectionString = "Data Source=DESKTOP-NT0LTQI\\SQLEXPRESS;Initial Catalog=courier;Integrated Security=True";
        private SqlConnection sqlConnection;
        private SqlDataReader sqlDataReader;

        public DataService() {
            sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
        }

        //Close connection with database
        public void CloseConnection()
        {
            sqlConnection.Close();
            sqlConnection.Dispose();
        }

        //Read SQL Data
        public DbDataReader ExecuteReader(string spName, DbParameter parameter)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.CommandText = spName;
            sqlCommand.Parameters.Clear();
            AddParameters(parameter,sqlCommand);
            sqlDataReader = sqlCommand.ExecuteReader();
            return sqlDataReader;
            

        }
        private void AddParameters(DbParameter parameter,SqlCommand command) {
            command.Parameters.Add(parameter);
        }
    }
}
