using JoyGClient.Models;
using Microsoft.AspNetCore.Mvc;

namespace JoyGClient.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
		

	}
}
