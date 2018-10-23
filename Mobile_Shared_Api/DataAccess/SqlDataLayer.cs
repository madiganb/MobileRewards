using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Mobile_Shared_Api.DataAccess
{
    public delegate T DataReaderDelegate<T>(IDataRecord reader);

    public class SqlDataLayer : BaseDataLayer
    {
        public SqlDataLayer(string connectionString)
            : base(connectionString) { }

        public override IDbDataParameter BuildParameter(string name, object value, DbType type)
        {
            return new SqlParameter
            {
                ParameterName = name,
                Value = value ?? DBNull.Value,
                DbType = type
            };
        }

        public override IDbDataParameter BuildParameter(string name, object value, string udtName)
        {
            return new SqlParameter
            {
                ParameterName = name,
                Value = value ?? DBNull.Value,
                SqlDbType = SqlDbType.Structured,
                TypeName = udtName
            };
        }

        public override int ExecuteNonQuery(string sql, List<IDbDataParameter> parameters = null)
        {
            try
            {
                parameters = parameters ?? new List<IDbDataParameter>();

                using (var conn = new SqlConnection(ConnectionString))
                {
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (var p in parameters)
                        {
                            cmd.Parameters.AddWithValue(p.ParameterName, p.Value);
                        }

                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("ERROR SQL Query: {0}", sql, ex));
            }
        }

        public override List<T> GetTypedList<T>(string sql, DataReaderDelegate<T> dataLoader, List<IDbDataParameter> parameters = null)
        {
            try
            {
                var retVal = new List<T>();
                parameters = parameters ?? new List<IDbDataParameter>();

                using (var conn = new SqlConnection(ConnectionString))
                {
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (var p in parameters)
                        {
                            cmd.Parameters.AddWithValue(p.ParameterName, p.Value);
                        }

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                retVal.Add(dataLoader(reader));
                            }
                        }

                        return retVal;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("ERROR SQL Query: {0}", sql, ex));
            }
        }

        public override T GetSingleRow<T>(string sql, DataReaderDelegate<T> dataLoader, List<IDbDataParameter> parameters = null)
        {
            try
            {
                parameters = parameters ?? new List<IDbDataParameter>();

                using (var conn = new SqlConnection(ConnectionString))
                {
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (var p in parameters)
                        {
                            cmd.Parameters.AddWithValue(p.ParameterName, p.Value);
                        }

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                return dataLoader(reader);
                            }
                        }

                        return default(T);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("ERROR SQL Query: {0}", sql, ex));
            }
        }

        public override T GetScalar<T>(string sql, List<IDbDataParameter> parameters = null)
        {
            try
            {
                parameters = parameters ?? new List<IDbDataParameter>();

                using (var conn = new SqlConnection(ConnectionString))
                {
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (var p in parameters)
                        {
                            cmd.Parameters.AddWithValue(p.ParameterName, p.Value);
                        }

                        return (T)Convert.ChangeType(cmd.ExecuteScalar(), typeof(T), CultureInfo.InvariantCulture);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("ERROR SQL Query: {0}", sql, ex));
            }
        }
    }
}