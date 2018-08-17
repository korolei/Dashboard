using System;

namespace Dashboard.Models.BorrowingProfile.ClientModels
{
    public class BorrowingTradeDetails
    {
        public double Coupon { get; set; }

        public string FaceVal{ get; set; }

        public string HedgeCurrency { get; set; }

        public string HedgeInterest{ get; set; }

        public string HedgeName{ get; set; }

        public DateTime? MDate{ get; set; }

        public string ProgramName{ get; set; }

        public string SN { get; set; }

        public DateTime? TDate{ get; set; }

        public int TradeNum{ get; set; }
    }
}
