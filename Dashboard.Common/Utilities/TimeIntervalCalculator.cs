using System;

namespace Dashboard.Common.Utilities
{
    public enum TimeInterval
    {
        Daily,
        Weekly,
        Monthly
    }

    [Flags]
    public enum TimeIntervalOptions
    {
        None = 0,
        Inclusive = 1,
        FitToPeriod = 2,
        Default = FitToPeriod | Inclusive
    }

    public static class TimeIntervalCalculator
    {
        public static DateRange GetRange(DateTime date, TimeInterval interval)
        {
            return GetRange(date, interval, TimeIntervalOptions.Default);
        }

        public static DateRange GetRange(DateTime date, TimeInterval interval, TimeIntervalOptions opts)
        {
            DateTime start = GetStart(date.Date, interval, opts);

            return new DateRange()
            {
                StartDate = start,
                EndDate = GetEnd(start, interval, opts)
            };
        }

        private static DateTime GetStart(DateTime time, TimeInterval interval, TimeIntervalOptions opts)
        {
            if (!HasFlag(opts, TimeIntervalOptions.FitToPeriod))
                return time;

            switch (interval)
            {
                case TimeInterval.Monthly:
                    return time.AddDays(-(time.Day - 1));
                case TimeInterval.Weekly:
                    return time.AddDays(-(int)time.DayOfWeek);
                case TimeInterval.Daily:
                default:
                    return time;
            }
        }

        private static DateTime GetEnd(DateTime time, TimeInterval interval, TimeIntervalOptions opts)
        {
            DateTime result;

            switch (interval)
            {
                case TimeInterval.Monthly:
                    result = time.AddMonths(1);
                    break;
                case TimeInterval.Weekly:
                    result = time.AddDays(7);
                    break;
                case TimeInterval.Daily:
                default:
                    result = time.AddDays(1);
                    break;
            }

            if (HasFlag(opts, TimeIntervalOptions.Inclusive))
                result = result.AddDays(-1);

            return result;
        }

        private static bool HasFlag(TimeIntervalOptions opts, TimeIntervalOptions flag)
        {
            return ((int)opts & (int)flag) != 0;
        }
    }
}
