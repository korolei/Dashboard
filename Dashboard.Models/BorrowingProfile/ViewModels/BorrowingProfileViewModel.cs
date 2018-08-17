using Dashboard.Common.Utilities;
using Dashboard.Models.BorrowingProfile.Models;

namespace Dashboard.Models.BorrowingProfile.ViewModels
{
    public class BorrowingProfileViewModel
    {
        public ProfileDataModel[] Profiles { get; set; }
        public ProfileDataModel[] ProfilesTotals { get; set; }
        public ProfileDataModel[] Hedges { get; set; }
        public ProfileDataModel Target { get; set; }
        public ProfileDealsModel ProfileDeals { get; set; }
        public ProfileDetailsModel ProfileDetails { get; set; }
        public BorrowingProfileFilter Filter{ get; set; }
        public FiscalYear[] Fiscals{ get; set; }
        public double ElapsedTime { get; set;}
    }
}