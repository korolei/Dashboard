using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Dashboard.Common.Utilities
{
	public static class EnumHelper
	{
		/// <summary>
		/// Gets the display name for the specified enumeration.
		/// </summary>
		/// <param name="input">The enumeration value.</param>
		/// <returns>The display name for the specified enum value.</returns>
		public static string GetDisplayName(Enum input)
		{
			ParameterValidator.AssertIsNotNull("input", input);

			Type enumType = input.GetType();
			string result = Enum.GetName(enumType, input);

			var fi = enumType.GetField(result);
			var attribute = fi.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault();

			if (attribute != null)
			{
				result = ((DescriptionAttribute)attribute).Description;
			}

			return result;
		}

		/// <summary>
		/// Converts the specified string value to its corresponding enumeration.
		/// </summary>
		/// <typeparam name="T">The enum type.</typeparam>
		/// <param name="value">The enum value.</param>
		/// <returns>An enumeration of the specified type.</returns>
		public static T Parse<T>(string value)
		{
            ParameterValidator.AssertIsEnum<T>();
            ParameterValidator.AssertIsNotNullOrWhiteSpace("value", value);

            return (T)Enum.Parse(typeof(T), value);
		}

		/// <summary>
		/// Converts the specified string value to its corresponding enumeration.
		/// </summary>
		/// <typeparam name="T">The enum type.</typeparam>
		/// <param name="value">The enum value.</param>
		/// <param name="ignoreCase">A flag indicating whether or not to ignore the case of the enum value.</param>
		/// <returns>An enumeration of the specified type.</returns>
		public static T Parse<T>(string value, bool ignoreCase)
        {
            ParameterValidator.AssertIsEnum<T>();
            ParameterValidator.AssertIsNotNullOrWhiteSpace("value", value);

            return (T)Enum.Parse(typeof(T), value, ignoreCase);
		}

        /// <summary>
        /// Scans Description values of an enum, returning matched elements
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="description">The enum description.</param>
        /// <returns>An enumeration of the specified type.</returns>
        public static IEnumerable<T> GetForDescription<T>(string description)
        {
            ParameterValidator.AssertIsEnum<T>();
            ParameterValidator.AssertIsNotNullOrWhiteSpace("description", description);

            Type enumType = typeof(T);
            Collection<T> enums = new Collection<T>();
            IEnumerable<FieldInfo> fieldInfos = enumType.GetFields().Where(o => o.FieldType == enumType);

            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                DescriptionAttribute attribute = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault() as DescriptionAttribute;

                if (attribute != null && attribute.Description == description)
                {
                    T enumFieldValue = (T)fieldInfo.GetValue(default(T));

                    enums.Add(enumFieldValue);
                }
            }

            return enums;
        }
    }
}
