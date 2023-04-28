using JoyGClient.Entities;
using JoyGClient.Interfaces;
using JoyGClient.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace JoyGClient.Controllers
{
	public class BackEndController : Controller
	{
        private readonly IClassificationService _classificationService;
        public BackEndController(IClassificationService classificationService)
        {
            _classificationService = classificationService;
        }

        public IActionResult Index()
		{
            ViewBag.mssg = TempData["mssg"] as string;
            return View();
		}

        [HttpGet]
        public async Task<ActionResult> ManageRestaurantClassifications(RestaurantClassifications restaurantClassifications)
		{
            ViewBag.mssg = TempData["mssg"] as string;
            var classifications = await _classificationService.GetClassifications();
            return View(classifications);
		}
        [HttpGet]
        
        public async Task<ActionResult> AddClassification(string text)
        {
            return PartialView();
        }
        [HttpPost]
       
        public async Task<ActionResult> AddClassification(ClassificationModel classificationModel)
        {
            string message = string.Empty;
            var user = User.Identity.Name;

            ClassificationModel values = new ClassificationModel();
            if (ModelState.IsValid)
            {
                var response = await _classificationService.AddClassification(classificationModel);
                if (!response.IsSuccess) TempData["mssg"] = response.Message;
                return RedirectToAction("ManageRestaurantClassifications", "BackEnd");
            }
            else
            {
                string errorMessageString = "";
                foreach (var entry in ModelState)
                {
                    var errorMessages = entry.Value.Errors.Select(e => e.ErrorMessage); // Get the error messages for the
                    errorMessageString = string.Join("; ", errorMessages);
                    
                }
                TempData["mssg"] = errorMessageString;
                return RedirectToAction("ManageRestaurantClassifications", "BackEnd");
            }
           
            return PartialView("_AddClassification" , values);
        }
    }
}
