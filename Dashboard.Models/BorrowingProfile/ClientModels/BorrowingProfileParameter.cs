using System;

namespace Dashboard.Models.BorrowingProfile.ClientModels
{
    public sealed class BorrowingProfileParameter
    {
        public DateTime EndDate { get; set; }
        public bool IncludeCPP { get; set; }
        public bool IncludeOIIC { get; set; }
        public DateTime StartDate { get; set; }
    }
}