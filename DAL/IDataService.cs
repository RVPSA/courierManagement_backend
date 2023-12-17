using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace DAL
{
    public interface IDataService
    {
        public void CloseConnection();
        public DbDataReader ExecuteReader(string spName, DbParameter[] parameter, int? timeOut = null);
        public int ExecuteNonQuery(string spName, DbParameter[] parameter, int? timeOut= null);

        public void BeginTransaction();
        public void CommitTransaction();
        public void RollbackTransaction();
    }
}
