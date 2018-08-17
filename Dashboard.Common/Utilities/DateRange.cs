using System;

namespace Dashboard.Common.Utilities
{
    public struct DateRange
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public static bool operator ==(DateRange leftValue, DateRange rightValue)
        {
            return leftValue.StartDate == rightValue.StartDate && leftValue.EndDate == rightValue.EndDate;
        }

        public static bool operator !=(DateRange leftValue, DateRange rightValue)
        {
            return leftValue.StartDate != rightValue.StartDate || leftValue.EndDate != rightValue.EndDate;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return StartDate.GetHashCode() + EndDate.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            if(obj is DateRange)
                return this == ((DateRange)obj);
            return false;
        }
    }
}
