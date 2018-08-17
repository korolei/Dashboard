using System;

namespace Dashboard.Models.BorrowingProfile.ClientModels
{
    public class BorrowingProfileDeal
    {
        public double Consolidated { get; set; }
        public int DealID { get; set; }
        public string Desc { get; set; }
        public string Market { get; set; }
        public double OEFC { get; set; }
        public double PercentBorrowed { get; set; }
        public string Profile { get; set; }
        public double Province { get; set; }
        public DateTime TradeDate { get; set; }
        public int Trades { get; set; }
    }
}