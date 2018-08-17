using System;
using Dashboard.Common.Utilities;
using Dashboard.Models.BorrowingProfile.Enums;

namespace Dashboard.Models.BorrowingProfile.ViewModels
{
    public class BorrowingProfileFilter
    {
        public DisplayType Display { get; set; }
        public bool IncludeOiic { get; set; }
        public bool IncludeCpp { get; set; }
        public int StartFiscalYear { get; set; }
        public DataViewMode ViewMode { get; set; }
        public string ProfileName { get; set; }
        

        public static BorrowingProfileFilter Default => new BorrowingProfileFilter
        {
            Display = DisplayType.Grid,
            StartFiscalYear = new FiscalYear(DateTime.Now).StartCalendarYear,
            IncludeCpp = false,
            IncludeOiic = false,
            ViewMode = DataViewMode.Profile,
            ProfileName = string.Empty
        };
    }
}