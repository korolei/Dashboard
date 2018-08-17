using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Web.WebAPI
{
    [Authorize(Policy="RequireWindowsProviderPolicy")]
    public class DashboardBaseController : Controller
    {

    }
}