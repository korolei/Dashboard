using System;
using System.Data.SqlClient;
using Dashboard.Common.Utilities;

namespace Dashboard.Common.Linq
{
    public static class SqlExtensions
    {
        public static T Field<T>(this SqlDataReader reader, string fieldName)
        {
            if (reader == null)
                throw new ArgumentException("Reader can not be null!");

            return TypeHelper.ConvertValue<T>(reader[fieldName]);
        }

        public static bool IsNull(this SqlDataReader reader, string fieldName)
        {
            if (reader == null)
                throw new ArgumentException("Reader can not be null!");

            return !reader.ContainsField(fieldName) || reader[fieldName] == DBNull.Value;
        }

        public static string[] GetFieldNames(this SqlDataReader reader)
        {
            if (reader == null)
                throw new ArgumentException("Reader can not be null!");

            int fieldCount = reader.FieldCount;
            string[] fieldNames = new string[fieldCount];

            for (int i = 0; i < fieldCount; i++)
                fieldNames[i] = reader.GetName(i);

            return fieldNames;
        }

        public static bool ContainsField(this SqlDataReader reader, string fieldName)
        {
            if (reader == null)
                throw new ArgumentException("Reader can not be null!");
            else if (String.IsNullOrEmpty(fieldName))
                throw new ArgumentException("Field name can not be null!");

            for (int i = 0; i < reader.FieldCount; i++)
                if (reader.GetName(i) == fieldName)
                    return true;

            return false;
        }
    }
}
