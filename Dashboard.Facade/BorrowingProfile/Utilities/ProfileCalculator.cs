using System;
using System.Collections.Generic;
using System.Linq;
using Dashboard.Common.Utilities;
using Dashboard.Models.BorrowingProfile.Models;

namespace Dashboard.Facade.BorrowingProfile.Utilities
{
    public static class ProfileCalculator
    {
        private static void Add(ProfileDataModel eax, ProfileDataModel edx)
        {
            if (edx != null)
            {
                eax.ConsolidatedAmount += edx.ProvinceAmount + edx.OefcAmount;
                eax.Deals += edx.Deals;
                eax.ProvinceAmount += edx.ProvinceAmount;
                eax.OefcAmount += edx.OefcAmount;
                eax.PercentBorrowed += edx.PercentBorrowed;
            }
        }

        private static T Sum<T>(T accumulator, IEnumerable<T> items) where T : ProfileDataModel
        {
            foreach (var item in items)
                Add(accumulator, item);

            return accumulator;
        }

        public static T Sum<T>(IEnumerable<T> items) where T : ProfileDataModel
        {
            var enumerable = items as T[] ?? items.ToArray();
            var accumulator = CloneEmpty(enumerable.FirstOrDefault());

            return Sum(accumulator, enumerable);
        }

        public static T[] CalculateTotals<T>(IEnumerable<T> items, T target, FiscalYear year) where T : ProfileDataModel
        {
            var totals = new T[4];

            totals[0] = Sum(items) ?? (T) Activator.CreateInstance(target.GetType());
            totals[0].ProfileName = "Total Actual Borrowing";


            totals[1] = CloneEmpty(target);
            if (totals[1] != null)
            {
                totals[1].ConsolidatedAmount = target.ConsolidatedAmount;
                totals[1].OefcAmount = target.OefcAmount;
                totals[1].ProvinceAmount = target.ProvinceAmount;
                totals[1].ProfileName = "Borrowing Requirement";
            }


            totals[2] = CloneEmpty(target);
            if (totals[2] != null)
            {
                totals[2].ConsolidatedAmount = totals[1].ConsolidatedAmount - totals[0].ConsolidatedAmount;
                totals[2].OefcAmount = totals[1].OefcAmount - totals[0].OefcAmount;
                totals[2].ProvinceAmount = totals[1].ProvinceAmount - totals[0].ProvinceAmount;
                totals[2].ProfileName = "Remaining";
            }

            totals[3] = CalculateBorrowingPace(target, totals[0], year);

            return totals;
        }

        private static T CalculateBorrowingPace<T>(T borrowingTargets, T borrowedAmounts, FiscalYear year)
            where T : ProfileDataModel
        {
            var percentCompleted = CloneEmpty(borrowingTargets);

            percentCompleted.ProfileName = "Pace (%) / Time (" + CalculateTimeElapsed(year).ToString("N0") + "%)";

            percentCompleted.ProvinceAmount = (borrowedAmounts.ProvinceAmount == 0 || borrowingTargets.ProvinceAmount == 0) ? 0:
                Math.Round(borrowedAmounts.ProvinceAmount / borrowingTargets.ProvinceAmount * 100, 1);

            percentCompleted.OefcAmount = (borrowedAmounts.OefcAmount == 0 || borrowingTargets.OefcAmount == 0) ? 0 : Math.Round(borrowedAmounts.OefcAmount / borrowingTargets.OefcAmount * 100, 1);

            percentCompleted.ConsolidatedAmount = (borrowedAmounts.ConsolidatedAmount == 0 || borrowingTargets.ConsolidatedAmount == 0) ? 0 :
                Math.Round(borrowedAmounts.ConsolidatedAmount / borrowingTargets.ConsolidatedAmount * 100, 1);

            return percentCompleted;
        }

        public static double CalculateTimeElapsed(FiscalYear fiscal)
        {
            var startDate = fiscal.StartDate;
            var day = double.Parse(((DateTime.Today.Ticks - startDate.Ticks) / 864000000000 + 1).ToString("0."));
            if (day > 365)
                day = 365;
            return day / 365 * 100;
        }

        public static T CloneEmpty<T>(T source)
        {
            if (ReferenceEquals(source, null))
                return default(T);

            return (T) Activator.CreateInstance(source.GetType());
        }
    }
}