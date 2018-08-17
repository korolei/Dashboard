using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Dashboard.Common.Utilities
{
    public class FiscalYear
    {
        public int StartCalendarYear
        {
            get;
        }
        public int EndCalendarYear
        {
            get;
        }
        public DateTime StartDate
        {
            get;
        }

        public DateTime EndDate { get; }
        public string DisplayYear { get; }

        public static FiscalYear Now => new FiscalYear(DateTime.Now.Year);


        public static FiscalYear[] GetFiscals()
        {
            var years = new Collection<FiscalYear>();

            for (var i = 0; i < 10; i++)
                years.Add(Now.AddYears(-i));

            return years.ToArray();
        }

        public FiscalYear(DateTime date)
        {
            if (date.Month < 4)
                StartCalendarYear = date.Year - 1;
            else
                StartCalendarYear = date.Year;


            EndCalendarYear = StartCalendarYear + 1;
            StartDate = new DateTime(StartCalendarYear, 4, 1);
            EndDate = new DateTime(EndCalendarYear, 3, 31);
            DisplayYear = ToString();
        }

        public FiscalYear(int calendarYearStart)
            : this(new DateTime(calendarYearStart, 4, 1))
        {
        }

        public FiscalYear AddYears(int years)
        {
            return new FiscalYear(StartDate.AddYears(years));
        }

        public sealed override string ToString()
        {
            return "FY" + StartCalendarYear + "/" + EndCalendarYear.ToString().Substring(2);
        }

        public override int GetHashCode()
        {
            return StartCalendarYear;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj, this);
        }

        public static bool operator ==(FiscalYear year1, FiscalYear year2)
        {
            return Equals(year1, year2);
        }

        public static bool operator !=(FiscalYear year1, FiscalYear year2)
        {
            return !Equals(year1, year2);
        }

        public static new bool Equals(object year1, object year2)
        {
            FiscalYear year1Obj = year1 as FiscalYear;
            FiscalYear year2Obj = year2 as FiscalYear;

            if (year1Obj is null && year2Obj is null)
                return true;
            if (year1Obj is null ^ year2Obj is null)
                return false;

            return year1Obj.StartCalendarYear == year2Obj.StartCalendarYear;
        }

        public static FiscalYear Parse(string value)
        {
            const string errMsg = "Invalid Fiscal Year";

            if (!TryParse(value, out var year))
                throw new FormatException(errMsg);

            return year;
        }

        public static bool TryParse(string value, out FiscalYear fiscalYear)
        {
            fiscalYear = null;

            if (string.IsNullOrWhiteSpace(value))
                return false;

            var fySplit = value.Split('/');

            if (fySplit.Length == 2)
            {
                if (int.TryParse(fySplit[0], out var startYear) && int.TryParse(fySplit[1], out var endYear) && startYear > 0 && startYear + 1 == endYear)
                    fiscalYear = new FiscalYear(startYear);
            }
            else if (fySplit.Length == 1)
            {
                if (int.TryParse(fySplit[0], out var startYear))
                    fiscalYear = new FiscalYear(startYear);
            }

            return fiscalYear != null;
        }
    }
}
