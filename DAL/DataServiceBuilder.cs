﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class DataServiceBuilder
    {
        public static IDataService CreateDataService() {
            return new DataService();
        }

        public static DbParameter createDBParameter(string paramName, DbType paramType, ParameterDirection parameterDirection
            ,object value) {
            SqlParameter param = new SqlParameter();
            param.DbType = paramType;
            param.ParameterName = paramName;
            param.Direction = parameterDirection;
            param.Value = value;
            return param;
        }
    }
}
