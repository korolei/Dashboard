using System.Web.Http;
using Dashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swagger.Net.Annotations;

namespace Dashboard.Web.WebAPI
{
    [Produces("application/json")]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly GlobalClientSettings _globalSettings;

        public HomeController(IOptions<GlobalClientSettings> globalSettings)
        {
            _globalSettings = globalSettings?.Value;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [SwaggerResponse(200, "Global client settings", typeof(GlobalClientSettings))]
        [Microsoft.AspNetCore.Mvc.Route("dashboard-settings")]
        public JsonResult GetSettings()
        {
            return Json(_globalSettings);
        }
    }
}