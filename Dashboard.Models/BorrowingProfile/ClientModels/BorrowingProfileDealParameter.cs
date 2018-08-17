using System;

namespace Dashboard.Models.BorrowingProfile.ClientModels
{
    public class BorrowingProfileDealParameter
    {
        public DateTime EndDate { get; set; }
        public bool IncludeCpp { get; set; }
        public bool IncludeOiic { get; set; }
        public string Profile { get; set; }
        public DateTime StartDate { get; set; }
    }
}