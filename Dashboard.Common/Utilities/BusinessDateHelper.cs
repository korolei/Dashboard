using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Dashboard.Common.Configuration;

namespace Dashboard.Common.Utilities
{
	public static class BusinessDateHelper
	{
		#region Fields

		private static readonly IEnumerable<DateTime> _holidays = GetHolidays();

		#endregion

		#region Public Methods

		public static DateTime AddBusinessDays(DateTime baseDate, int businessDaysToAdd)
		{
			ParameterValidator.AssertIsNotMinOrMaxDate("baseDate", baseDate);
            ParameterValidator.AssertIsPositiveValue("businessDaysToAdd", businessDaysToAdd);

			DateTime result = baseDate;
			int daysAdded = 0;

			while (daysAdded < businessDaysToAdd)
			{
				if (result == DateTime.MaxValue)
				{
					string message = "Adding the specified number of days results in a value greater than the maximum allowable date value.";
					throw new InvalidOperationException(message);
				}
				else
				{
					result = result.AddDays(1);

					if (IsWorkingDay(result))
					{
						daysAdded++;
					}
				}
			}

			return result;
		}

		public static DateTime SubtractBusinessDays(DateTime baseDate, int businessDaysToSubtract)
		{
			ParameterValidator.AssertIsNotMinOrMaxDate("baseDate", baseDate);
            ParameterValidator.AssertIsPositiveValue("businessDaysToSubtract", businessDaysToSubtract);

			DateTime result = baseDate;
			int daysSubtracted = 0;

			while (daysSubtracted < businessDaysToSubtract)
			{
				if (result == DateTime.MinValue)
				{
					string message = "Subtracting the specified number of days results in a value less than minimum allowable date value.";
					throw new InvalidOperationException(message);
				}
				else
				{
					result = result.AddDays(-1);

					if (IsWorkingDay(result))
					{
						daysSubtracted++;
					}
				}
			}

			return result;
		}

		public static bool IsWorkingDay(DateTime inputDate)
		{
			bool result = true;

			if (inputDate.DayOfWeek == DayOfWeek.Saturday || inputDate.DayOfWeek == DayOfWeek.Sunday)
			{
				result = false;
			}
			else
			{
				if (_holidays.Any(x => x == inputDate))
				{
					result = false;
				}
			}

			return result;
		}

		public static IEnumerable<DateTime> GetHolidays(DateTime fromDate, DateTime toDate)
		{
			return _holidays.Where(x => x >= fromDate && x <= toDate);
		}

		#endregion

		#region Private Methods

		private static IEnumerable<DateTime> GetHolidays()
		{
			Collection<DateTime> result = new Collection<DateTime>();

            string connectionString = GlobalConfigurationSettings.MossySettings[ApplicationSettings.TreasuryDatabase];
            Dictionary<string, object> parameters = new Dictionary<string,object>() {
                { "CalendarShortName", "CA" }
            };

            result = SqlHelper.GetModelFromStoredProcedure(connectionString, "dbo.DT_GetHolidays", parameters, reader => reader.GetDateTime(0));

			return result;
		}

		#endregion
	}
}
