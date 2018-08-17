using System;
using System.Collections.Generic;

namespace Dashboard.Models.BorrowingProfile.Models
{
    public class ProfileDealsModel
    {
        public List<ProfileDealInfoModel> Deals { get; set; }
        public ProfileDealInfoModel ProfileDetailsTotal { get; set; }
    }

    public class ProfileDealInfoModel
    {
        public int DealId { get; set; }
        public string Description { get; set; }
        public double ConsolidatedAmount { get; set; }
        public string Market { get; set; }
        public double OefcAmount { get; set; }
        public double PercentBorrowed { get; set; }
        public string ProfileName { get; set; }
        public double ProvinceAmount { get; set; }
        public DateTime TradeDate { get; set; }
        public int TradesNum { get; set; }
    }
}
