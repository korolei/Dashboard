using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace Dashboard.Common.Utilities
{
    public static class SqlHelper
    {
        public static T Coalesce<T>(object value)
        {
            if (Convert.IsDBNull(value))
                return default(T);
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static Collection<T> GetModelFromStoredProcedure<T>(string connectionString, string storedProcedure, IDictionary<string, object> parameterList, Func<SqlDataReader, T> transform)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                return GetModelFromStoredProcedure<T>(connection, storedProcedure, parameterList, transform);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public static Collection<T> GetModelFromStoredProcedure<T>(SqlConnection connection, string storedProcedure, IDictionary<string, object> parameterList, Func<SqlDataReader, T> transform)
        {
            IDictionary<string, object> parameters = parameterList ?? new Dictionary<string, object>();
            Func<SqlDataReader, T> transformFn = transform ?? DefaultTransform<T>;
            Collection<T> data = new Collection<T>();

            using (SqlCommand command = new SqlCommand(storedProcedure, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                foreach (KeyValuePair<string, object> param in parameters)
                    command.Parameters.Add(new SqlParameter(param.Key, param.Value));

                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                        data.Add(transformFn(reader));
                }
            }

            return data;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public static Collection<T> GetModelFromStoredProcedure<T>(SqlConnection connection, string storedProcedure, IDictionary<string, object> parameterList, IDictionary<string, DataTable> tableParameterList, Func<SqlDataReader, T> transform)
        {
            IDictionary<string, object> parameters = parameterList ?? new Dictionary<string, object>();
            IDictionary<string, DataTable> tableParameters = tableParameterList ?? new Dictionary<string, DataTable>();
            Func<SqlDataReader, T> transformFn = transform ?? DefaultTransform<T>;
            Collection<T> data = new Collection<T>();

            using (SqlCommand command = new SqlCommand(storedProcedure, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                foreach (KeyValuePair<string, object> param in parameters)
                    command.Parameters.Add(new SqlParameter(param.Key, param.Value));

                foreach (KeyValuePair<string, DataTable> tvp in tableParameters)
                {
                    SqlParameter tvpParam = command.Parameters.AddWithValue(tvp.Key, tvp.Value);
                    tvpParam.SqlDbType = SqlDbType.Structured;
                    tvpParam.TypeName = tvp.Value.TableName;
                }

                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                        data.Add(transformFn(reader));
                }
            }

            return data;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public static IEnumerable<T> EnumerateStoredProcedure<T>(string connectionString, string storedProcedure, IDictionary<string, object> parameterList, Func<SqlDataReader, T> transform)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                IDictionary<string, object> parameters = parameterList ?? new Dictionary<string, object>();
                Func<SqlDataReader, T> transformFn = transform ?? DefaultTransform<T>;
                Collection<T> data = new Collection<T>();

                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    foreach (KeyValuePair<string, object> param in parameters)
                        command.Parameters.Add(new SqlParameter(param.Key, param.Value));

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                            yield return transformFn(reader);
                    }
                }
            }
        }

        public static T GetSingleModelFromStoredProcedure<T>(string connectionString, string storedProcedure, IDictionary<string, object> parameterList, Func<SqlDataReader, T> transform)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return GetSingleModelFromStoredProcedure<T>(connection, storedProcedure, parameterList, transform);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public static T GetSingleModelFromStoredProcedure<T>(SqlConnection connection, string storedProcedure, IDictionary<string, object> parameterList, Func<SqlDataReader, T> transform)
        {
            CommandBehavior behaviour = CommandBehavior.CloseConnection | CommandBehavior.SingleRow;
            IDictionary<string, object> parameters = parameterList ?? new Dictionary<string, object>();
            Func<SqlDataReader, T> transformFn = transform ?? DefaultTransform<T>;
            T data = default(T);

            using (SqlCommand command = new SqlCommand(storedProcedure, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                foreach (KeyValuePair<string, object> param in parameters)
                    command.Parameters.Add(new SqlParameter(param.Key, param.Value));

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader(behaviour))
                {
                    if (reader.Read())
                        data = transformFn(reader);
                }
            }

            return data;
        }

        public static T GetScalarFromFunction<T>(string connectionString, string storedProcedure, IDictionary<string, object> parameterList)
        {
            SqlDbType dbType = GetDBType(typeof(T));

            return GetScalarFromFunction<T>(connectionString, storedProcedure, parameterList, dbType);
        }

        public static T GetScalarFromFunction<T>(string connectionString, string storedProcedure, IDictionary<string, object> parameterList, SqlDbType outputType)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                return GetScalarFromFunction<T>(conn, storedProcedure, parameterList, outputType);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public static T GetScalarFromFunction<T>(SqlConnection conn, string storedProcedure, IDictionary<string, object> parameterList)
        {
            SqlDbType dbType = GetDBType(typeof(T));

            return GetScalarFromFunction<T>(conn, storedProcedure, parameterList, dbType);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public static T GetScalarFromFunction<T>(SqlConnection conn, string storedProcedure, IDictionary<string, object> parameterList, SqlDbType outputType)
        {
            IDictionary<string, object> parameters = parameterList ?? new Dictionary<string, object>();

            using (SqlCommand command = new SqlCommand(storedProcedure, conn))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                foreach (KeyValuePair<string, object> param in parameters)
                    command.Parameters.Add(new SqlParameter(param.Key, param.Value));

                SqlParameter returnVal = new SqlParameter("@returnVal" + Guid.NewGuid(), outputType);
                returnVal.Direction = ParameterDirection.ReturnValue;
                command.Parameters.Add(returnVal);

                command.ExecuteNonQuery();

                return TypeHelper.ConvertValue<T>(returnVal.Value);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public static void ExecuteNonQuery(string connectionString, string storedProcedure, IDictionary<string, object> parameterList)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                ExecuteNonQuery(connection, storedProcedure, parameterList);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public static void ExecuteNonQuery(SqlConnection connection, string storedProcedure, IDictionary<string, object> parameterList)
        {
            IDictionary<string, object> parameters = parameterList ?? new Dictionary<string, object>();

            using (SqlCommand command = new SqlCommand(storedProcedure, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                foreach (KeyValuePair<string, object> param in parameters)
                    command.Parameters.Add(new SqlParameter(param.Key, param.Value));

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                command.ExecuteNonQuery();
            }
        }

        private static T DefaultTransform<T>(SqlDataReader reader)
        {
            return default(T);
        }

        public static DateTime BoundToSqlDateTime(DateTime date)
        {
            if (date < SqlDateTime.MinValue.Value)
                return SqlDateTime.MinValue.Value;
            else if (date > SqlDateTime.MaxValue.Value)
                return SqlDateTime.MaxValue.Value;
            return date;
        }

        public static SqlDbType GetDBType(Type theType)
        {
            SqlParameter param = new SqlParameter();
            TypeConverter tc = TypeDescriptor.GetConverter(param.DbType);

            if (tc.CanConvertFrom(theType))
            {
                param.DbType = (DbType)tc.ConvertFrom(theType.Name);
            }
            else
            {
                // try to forcefully convert, may throw FormatException
                param.DbType = (DbType)tc.ConvertFrom(theType.Name);
            }

            return param.SqlDbType;
        }
    }
}
