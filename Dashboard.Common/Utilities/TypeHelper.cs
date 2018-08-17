using System;
using System.ComponentModel;

namespace Dashboard.Common.Utilities
{
    public static class TypeHelper
    {
        public static bool IsNullable(Type type)
        {
            if (type.IsClass)
                return true;
            else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                return true;
            else
                return false;
        }

        public static object GetDefaultValue(Type type)
        {
            if (type.IsValueType)
                return Activator.CreateInstance(type);

            return null;
        }

        public static T ConvertValue<T>(object value)
        {
            Type conversionType = typeof(T);

            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null || value == DBNull.Value)
                    return default(T);

                conversionType = new NullableConverter(conversionType).UnderlyingType;
            }

            return (T)Convert.ChangeType(value, conversionType);
        }
    }
}
