using System.Collections.Generic;
using System.Data;

namespace Mobile_Shared_Api.DataAccess
{
    public abstract class BaseDataLayer
    {
        protected string ConnectionString { get; set; }

        protected BaseDataLayer(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public abstract IDbDataParameter BuildParameter(string name, object value, DbType type);
        public abstract IDbDataParameter BuildParameter(string name, object value, string udtName);
        public abstract int ExecuteNonQuery(string sql, List<IDbDataParameter> parameters = null);
        public abstract List<T> GetTypedList<T>(string sql, DataReaderDelegate<T> dataLoader, List<IDbDataParameter> parameters = null);
        public abstract T GetSingleRow<T>(string sql, DataReaderDelegate<T> dataLoader, List<IDbDataParameter> parameters = null);
        public abstract T GetScalar<T>(string sql, List<IDbDataParameter> parameters = null);

    }
}