

namespace Dashboard.Models.BorrowingProfile.ClientModels
{
    public class BorrowingTradeDetailsParameter
    {
        public int DealID { get; set; }
        public bool EnableCpp { get; set; }
        public bool EnableOiic { get; set; }
        public System.DateTime EndDate { get; set; }
        public string ProfileName { get; set; }
        public System.DateTime StartDate { get; set; }
    }
}