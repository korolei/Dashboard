using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dashboard.Common.Utilities
{
	public static class ParameterValidator
	{
		public static void AssertIsNotNull(string parameterName, object parameterValue)
		{
			if (parameterValue == null)
			{
				string message = String.Format(CultureInfo.InvariantCulture, "The '{0}' parameter cannot be null.", parameterName);
				throw new ArgumentNullException(parameterName, message);
			}
		}

        [Obsolete("Prefer using EntityBase")]
		public static void AssertIsNotNullOrInvalid(string parameterName, EntityBase parameterValue)
		{
			if (parameterValue == null)
			{
				string message = String.Format(CultureInfo.InvariantCulture, "The '{0}' parameter cannot be null.", parameterName);
				throw new ArgumentNullException(parameterName, message);
			}
			else
			{
				parameterValue.AssertIsValid();
			}
		}

		public static void AssertIsNotNullOrWhiteSpace(string parameterName, string parameterValue)
		{
			if (String.IsNullOrWhiteSpace(parameterValue))
			{
				string message = String.Format(CultureInfo.InvariantCulture, "The '{0}' parameter cannot be null or empty.", parameterName);
				throw new ArgumentException(message, parameterName);
			}
		}

        public static void AssertDoesNotEndWith(string parameterName, string parameterValue, string endsWith)
        {
            if (parameterValue.EndsWith(endsWith))
            {
                string message = String.Format(CultureInfo.InvariantCulture, "The '{0}' parameter cannot end with {1}.", parameterName, endsWith);
                throw new ArgumentException(message, parameterName);
            }
        }

        public static bool IsMinOrMaxDate(DateTime parameterValue)
        {
            return parameterValue == DateTime.MinValue || parameterValue == DateTime.MaxValue;
        }

		public static void AssertIsNotMinOrMaxDate(string parameterName, DateTime parameterValue)
		{
            if (IsMinOrMaxDate(parameterValue))
			{
				string message = String.Format(CultureInfo.InvariantCulture, "The '{0}' parameter is not a valid date value.", parameterName);
				throw new ArgumentOutOfRangeException(parameterName, message);
			}
		}
        
        public static bool IsPositiveValue(int parameterValue)
        {
            return parameterValue > 0;
        }

        public static void AssertIsPositiveValue(string parameterName, int parameterValue)
        {
            if (!IsPositiveValue(parameterValue))
            {
                string message = String.Format(CultureInfo.InvariantCulture, "The '{0}' parameter must be greater than zero.", parameterName);
                throw new ArgumentOutOfRangeException(parameterName, message);
            }
        }

        public static bool IsPositiveValue(double parameterValue)
        {
            return parameterValue > 0;
        }

        public static void AssertIsPositiveValue(string parameterName, double parameterValue)
        {
            if (!IsPositiveValue(parameterValue))
            {
                string message = String.Format(CultureInfo.InvariantCulture, "The '{0}' parameter must be greater than zero.", parameterName);
                throw new ArgumentOutOfRangeException(parameterName, message);
            }
        }

        public static bool IsPositiveValue(decimal parameterValue)
        {
            return parameterValue > 0;
        }

        public static void AssertIsPositiveValue(string parameterName, decimal parameterValue)
        {
            if (!IsPositiveValue(parameterValue))
            {
                string message = String.Format(CultureInfo.InvariantCulture, "The '{0}' parameter must be greater than zero.", parameterName);
                throw new ArgumentOutOfRangeException(parameterName, message);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "By design")]
        public static void AssertIsEnum<T>()
        {
            if (!typeof(T).IsEnum)
            {
                throw new InvalidOperationException("'" + typeof(T).FullName + "' is not an enumeration.");
            }
        }

		public static void AssertIsValidEnumValue(string parameterName, int parameterValue, Type enumType)
		{
			if (!Enum.IsDefined(enumType, parameterValue))
			{
				string message = String.Format(CultureInfo.InvariantCulture, "The '{0}' parameter is not a valid {1} value.", parameterName, enumType.Name);
				throw new ArgumentOutOfRangeException(parameterName, message);
			}
		}

		public static void AssertIsNotEmptyGuid(string parameterName, Guid parameterValue)
		{
			if (parameterValue == Guid.Empty)
			{
				string message = String.Format(CultureInfo.InvariantCulture, "The '{0}' parameter cannot be an empty Guid.", parameterName);
				throw new ArgumentOutOfRangeException(parameterName, message);
			}
		}

        public static bool IsNullOrEmptyList<T>(IEnumerable<T> parameterValue)
        {
            return parameterValue == null || parameterValue.Count() == 0;
        }

		public static void AssertIsNotNullOrEmptyList<T>(string parameterName, IEnumerable<T> parameterValue)
		{
            if (IsNullOrEmptyList(parameterValue))
			{
				string message = String.Format(CultureInfo.InvariantCulture, "The '{0}' parameter cannot be null or empty.", parameterName);
				throw new ArgumentException(message, parameterName);
			}
		}

        public static bool IsNullOrEmptyStream(Stream parameterValue)
        {
            return parameterValue == null || parameterValue.Length == 0;
        }

		public static void AssertIsNotNullOrEmptyStream(string parameterName, Stream parameterValue)
		{
            if (IsNullOrEmptyStream(parameterValue))
            {
                string message = String.Format(CultureInfo.InvariantCulture, "The '{0}' parameter cannot be null or empty.", parameterName);
                throw new ArgumentException(message, parameterName);
            }
		}

		public static bool IsValidFilePath(string parameterValue)
		{
			return Regex.IsMatch(parameterValue, @"^((([a-zA-Z]:)|(\\{2}[a-zA-Z0-9_\.\-]+)|(\\{2}(?:(?:25[0-5]|2[0-4]\d|[01]\d\d|\d?\d)(?(?=\.?\d)\.)){4}))(\\(\w[\w ]*)))");
		}

		public static void AssertIsValidFilePath(string parameterName, string parameterValue)
		{
			if (!IsValidFilePath(parameterValue))
			{
                string message = String.Format(CultureInfo.InvariantCulture, "The '{0}' parameter is not a valid file path.", parameterName);
                throw new ArgumentException(message, parameterName);
			}
		}
    }
}
