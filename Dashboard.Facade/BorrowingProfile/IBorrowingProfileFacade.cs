using System.Threading.Tasks;
using Dashboard.Common.Utilities;
using Dashboard.Models.BorrowingProfile.Models;
using Dashboard.Models.BorrowingProfile.ViewModels;

namespace Dashboard.Facade.BorrowingProfile
{
    public interface IBorrowingProfileFacade
    {
        Task<ProfileDealsModel> GetDealsDataAsync(BorrowingProfileFilter filter);
        Task<ProfileDetailsModel> GetDetailsAsync(BorrowingProfileFilter filter, int dealId);
        Task<ProfileDataModel[]> GetHedgesAsync(BorrowingProfileFilter filter);
        Task<ProfileDataModel> GetTargetAsync(FiscalYear filterFiscal);
        Task<ProfileDataModel[]> GetProfilesDataAsync(BorrowingProfileFilter filter);
    }
}