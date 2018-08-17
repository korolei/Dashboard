using System.Threading.Tasks;
using Dashboard.Common.Utilities;
using Dashboard.Facade.BorrowingProfile;
using Dashboard.Facade.BorrowingProfile.Utilities;
using Dashboard.Models.BorrowingProfile.Settings;
using Dashboard.Models.BorrowingProfile.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swagger.Net.Annotations;

namespace Dashboard.Web.WebAPI
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigins")]
    public class BorrowingProfileController : DashboardBaseController
    {
        private readonly IBorrowingProfileFacade _facade;
        private readonly BorrowingProfileClientSettings _clientSettings;

        public BorrowingProfileController(IBorrowingProfileFacade facade,IOptions<BorrowingProfileClientSettings> clientSettings)
        {
            _clientSettings = clientSettings?.Value;
            _facade = facade;
        }

        [HttpGet]
        [Route("borrowing-profile-settings")]
        [SwaggerResponse(200, "Borrowing Profiles client settings", typeof(BorrowingProfileClientSettings))]
        public async Task<IActionResult> GetBorrowingProfileSettings()
        {
            return Ok(await Task.Run(()=>_clientSettings));
        }

        [HttpPost]
        [SwaggerResponse(200, "Borrowing Profiles by filter", typeof(BorrowingProfileViewModel))]
        public async Task<IActionResult> Post([FromBody]BorrowingProfileFilter filter)
        {
            if (filter == null)
            {
                filter = BorrowingProfileFilter.Default;
            }
            var vm = new BorrowingProfileViewModel {Filter = filter, Fiscals = FiscalYear.GetFiscals()};
            var fiscalYear = new FiscalYear(filter.StartFiscalYear);
            
            vm.Profiles = await _facade.GetProfilesDataAsync(filter);
            vm.Target = await _facade.GetTargetAsync(fiscalYear);           
            vm.Hedges = await _facade.GetHedgesAsync(filter);

            if (vm.Profiles != null)
            {
                vm.ProfilesTotals = ProfileCalculator.CalculateTotals(vm.Profiles, vm.Target, fiscalYear);
                vm.ElapsedTime = ProfileCalculator.CalculateTimeElapsed(fiscalYear);
            }
            return Ok(vm);
        }

        [HttpPost]
        [Route("profile-trades/{dealId}")]
        [SwaggerResponse(200, "Borrowing Profiles by Profile Name and/or Deal Id", typeof(BorrowingProfileViewModel))]
        public async Task<IActionResult> PostByDealId([FromBody]BorrowingProfileFilter filter, [FromQuery]int? dealId)
        {
            var details = await _facade.GetDetailsAsync(filter, dealId??0);
            return Ok(details);
        }

        [HttpPost]
        [Route("profile-deals")]
        [SwaggerResponse(200, "Deals Data by Profile Name", typeof(BorrowingProfileViewModel))]
        public async Task<IActionResult> PostByProfileName([FromBody]BorrowingProfileFilter filter)
        {
            var details = await _facade.GetDealsDataAsync(filter);
            return Ok(details);
        }
    }
}