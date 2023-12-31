using Configurations;
using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace DAL
{
    public class DataService : IDataService
    {
        //Variables
        private string _connectionString = AppSettings.DatabaseURL;
        private SqlConnection sqlConnection;
        private SqlDataReader sqlDataReader;
        private SqlTransaction sqlTransaction;

        public DataService() {
            sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
        }

        //Close connection with database
        public void CloseConnection()
        {
            if(sqlConnection != null && sqlConnection.State == System.Data.ConnectionState.Open) {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            if (sqlDataReader != null && !sqlDataReader.IsClosed) {
                sqlDataReader.Dispose();
            }
            if (sqlConnection != null) {
                sqlConnection.Dispose();
            }
        }

        //Sql Transaction handle
        public void BeginTransaction() {
            if (sqlConnection != null) {
                if (sqlConnection.State == System.Data.ConnectionState.Closed) {
                    sqlConnection.Open();
                    sqlTransaction = sqlConnection.BeginTransaction();
                }
            }
        }
        public void CommitTransaction() {
            if (sqlConnection != null) {
                if (sqlTransaction != null) {
                    sqlTransaction.Commit();
                    sqlTransaction = null;
                }
                if (sqlConnection.State == System.Data.ConnectionState.Open) {
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                }
            }
        }
        public void RollbackTransaction() {
            if (sqlConnection != null) {
                if(sqlTransaction != null)
                {
                    sqlTransaction.Rollback();
                    sqlTransaction = null;
                }
                if (sqlConnection.State == System.Data.ConnectionState.Open) {
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                }
            }
        }

        //Execute Just Query
        public int ExecuteNonQuery(string spName, DbParameter[] parameter,int? timeOut = null) {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            if (timeOut != null) {
                sqlCommand.CommandTimeout = Convert.ToInt32(timeOut);
            }
            sqlCommand.CommandText = spName;
            sqlCommand.Parameters.Clear();
            AddParameters(parameter, sqlCommand);
            if (sqlTransaction != null) {
                sqlCommand.Transaction = sqlTransaction;
            }
            int result = sqlCommand.ExecuteNonQuery();
            return result;

        }

        //Read SQL Data
        public DbDataReader ExecuteReader(string spName, DbParameter[] parameter,int? timeOut = null)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            if (timeOut != null) {
                sqlCommand.CommandTimeout = Convert.ToInt32(timeOut);
            }
            sqlCommand.CommandText = spName;
            sqlCommand.Parameters.Clear();
            AddParameters(parameter,sqlCommand);
            if (sqlTransaction != null) {
                sqlCommand.Transaction = sqlTransaction;
            }
            sqlDataReader = sqlCommand.ExecuteReader();
            return sqlDataReader;
            

        }
        private void AddParameters(DbParameter[] parameter,SqlCommand command) {
            if (parameter != null)
            {
                foreach (DbParameter parameter1 in parameter)
                {
                command.Parameters.Add(parameter1);
                }
            }
        }
    }
}
