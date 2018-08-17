using System;

namespace Dashboard.Models.BorrowingProfile.Models
{
    public class TradeDetailsModel
    {
        public DateTime MaturityDate { get; set; }
        public string Series { get; set; }
        public DateTime TradeDate { get; set; }
        public int TradeNum { get; set; }
        public string Description { get; set; }
    }
}