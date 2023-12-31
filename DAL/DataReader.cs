using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DAL
{
    public class DataReader
    {
        private DbDataReader reader;
        public DataReader(DbDataReader reader) {
            this.reader = reader;
        }

        public int GetInt32(string column) {
            int data = 0;
            if (checkColumnAvaliability(reader, column)) {
                data = reader.IsDBNull(reader.GetOrdinal(column)) ? (int)0 : (int)reader[column];
            }

            return data;
        }

        public string GetString(string column) {
            string data = string.Empty;
            if (checkColumnAvaliability(reader, column)) {
                data = reader[column].ToString();
            }
            return data;
        }

        private bool checkColumnAvaliability(DbDataReader reader, string columnName) {
            int columnCount = reader.GetColumnSchema().Where(t => t.ColumnName == columnName).Count();
            return columnCount > 0;
        }
    }
}
