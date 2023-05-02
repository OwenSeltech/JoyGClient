using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JoyGClient.Controllers
{
    public class DashboardController : Controller
    {
       
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult UserDashboard()
        {
            return View();
        }

    }
}
