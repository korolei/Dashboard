using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Swagger.Net.Annotations;
using WebApiWrapper.Connected_Services.BorrowingProfileProvider;
using WebApiWrapper.ServiceRepositories;

namespace WebApiWrapper.Controllers
{
    [RoutePrefix("api/borrowing_profile")]
    [EnableCors(origins: "*", headers: "*", methods: "get, post")]
    public class BorrowingProfileController : ApiController
    {
        private readonly BorrowingProfileStore _store = new BorrowingProfileStore();

        [HttpPost, Route("targets")]
        [SwaggerResponse(200,"Borrowing Profile Target",typeof(BorrowingProfile))]
        public async Task<IHttpActionResult> GetBorrowingTargets([FromBody]int fiscalYear)
        {
            var info = await _store.Execute(o => o.GetBorrowingTargetsAsync(fiscalYear));
            return Ok(info);
        }

        [HttpPost, Route("profiles")]
        [SwaggerResponse(200, "Borrowing Profiles", typeof(BorrowingProfile[]))]
        public async Task<IHttpActionResult> GetProfiles(BorrowingProfileParameter param)
        {
            if (param != null)
            {
                var info = await _store.Execute(o => o.GetBorrowingProfilesAsync(param));
                return Ok(info);
            }
            return BadRequest("Parameter can not be null!");
        }

        [HttpPost, Route("hedges")]
        [SwaggerResponse(200, "Borrowing Profiles Hedges", typeof(BorrowingProfileHedges[]))]
        public async Task<IHttpActionResult> GetBorrowingProfileHedges([FromBody]BorrowingProfileParameter param)
        {
            var info = await _store.Execute(o => o.GetBorrowingProfileHedgesAsync(param));
            return Ok(info);
        }

        [HttpPost, Route("details/{dealId:int}")]
        [SwaggerResponse(200, "Borrowing Trade Details", typeof(BorrowingTradeDetails[]))]
        public  async Task<IHttpActionResult> GetDetails([FromBody]BorrowingTradeDetailsParameter param, int dealId=0)
        {
            return Ok(dealId > 0 ? 
                await _store.Execute(o => o.GetBorrowingTradeDetailsByDealAsync(param)) :
                await _store.Execute(o => o.GetBorrowingTradeDetailsByProfileAsync(param)));
        }

        [HttpPost, Route("deals")]
        [SwaggerResponse(200, "Borrowing Profile Deals", typeof(BorrowingProfileDeal[]))]
        public async Task<IHttpActionResult> GetDeals([FromBody]BorrowingProfileDealParameter param)
        {
            return Ok(await _store.Execute(o => o.GetBorrowingDealsAsync(param)));
        }
    }
}
