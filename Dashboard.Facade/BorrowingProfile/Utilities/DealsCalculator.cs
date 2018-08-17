using System;
using System.Collections.Generic;
using System.Linq;
using Dashboard.Models.BorrowingProfile.Models;

namespace Dashboard.Facade.BorrowingProfile.Utilities
{
    public class DealsCalculator
    {
        private static void Add(ProfileDealInfoModel eax, ProfileDealInfoModel edx)
        {
            if (edx == null) return;
            eax.ConsolidatedAmount += edx.ProvinceAmount + edx.OefcAmount;
            eax.ProvinceAmount += edx.ProvinceAmount;
            eax.OefcAmount += edx.OefcAmount;
            eax.PercentBorrowed += edx.PercentBorrowed;
        }

        private static T Sum<T>(T accumulator, IEnumerable<T> items) where T : ProfileDealInfoModel
        {
            foreach (var item in items)
                Add(accumulator, item);

            return accumulator;
        }

        private static T Sum<T>(IEnumerable<T> items) where T : ProfileDealInfoModel
        {
            var enumerable = items as T[] ?? items.ToArray();
            T accumulator = CloneEmpty(enumerable.FirstOrDefault());

            return Sum(accumulator, enumerable);
        }

        public static T Sum<T>(params T[] items) where T : ProfileDealInfoModel
        {
            return Sum((IEnumerable<T>)items);
        }

        private static T CloneEmpty<T>(T source)
        {
            if (source == null)
                return default(T);

            return (T)Activator.CreateInstance(source.GetType());
        }
    }
}
