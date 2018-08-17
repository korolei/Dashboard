using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Dashboard.Models.BorrowingProfile.Models
{
    public class ProfileDetailsModel
    {
        public string ProfileName { get; set; }
        public ICollection<TradeDetailsModel> Trades { get; set; }

        public ProfileDetailsModel()
        {
            Trades = new Collection<TradeDetailsModel>();
        }
    }
}