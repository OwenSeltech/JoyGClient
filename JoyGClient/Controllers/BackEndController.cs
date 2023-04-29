using JoyGClient.Entities;
using JoyGClient.Interfaces;
using JoyGClient.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JoyGClient.Controllers
{
    [Authorize]
    public class BackEndController : Controller
	{
        private readonly IClassificationService _classificationService;
        private readonly IDishTypeService _dishTypeService;
        public BackEndController(IClassificationService classificationService,IDishTypeService dishTypeService)
        {
            _classificationService = classificationService;
            _dishTypeService = dishTypeService;
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
            ViewBag.mssgEdit = TempData["mssgEdit"] as string;
            var classifications = await _classificationService.GetClassifications();
            return View(classifications);
		}

        [HttpGet]
        public async Task<ActionResult> ManageDishTypes(DishTypes dishTypes)
        {
            ViewBag.mssg = TempData["mssg"] as string;
            ViewBag.mssgEdit = TempData["mssgEdit"] as string;
            var types = await _dishTypeService.GetDishTypes();
            return View(types);
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
           
            if (ModelState.IsValid)
            {
                classificationModel.UpdatedBy = user;
                classificationModel.CreatedBy = user;
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
           
        }

        [HttpGet]

        public async Task<ActionResult> _EditClassification(string id)
        {
            var classifications = new ClassificationModel();
            var result = await _classificationService.GetClassificationById(id);
            classifications.ClassificationName = result.ClassificationName;
            return PartialView(classifications);
        }

        [HttpPost]

        public async Task<ActionResult> _EditClassification(ClassificationModel classificationModel)
        {
            string message = string.Empty;
            var user = User.Identity.Name;

            if (ModelState.IsValid)
            {
                classificationModel.UpdatedBy = user;
                classificationModel.CreatedBy = user;
                var response = await _classificationService.EditClassification(classificationModel);
                if (!response.IsSuccess) TempData["mssgEdit"] = response.Message;
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
                TempData["mssgEdit"] = errorMessageString;
                return RedirectToAction("ManageRestaurantClassifications", "BackEnd");
            }
        }

    }
}
