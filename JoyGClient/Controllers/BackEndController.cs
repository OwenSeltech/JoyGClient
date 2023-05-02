using JoyGClient.Entities;
using JoyGClient.Interfaces;
using JoyGClient.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web;

namespace JoyGClient.Controllers
{
    [Authorize]
    public class BackEndController : Controller
	{
        private readonly IClassificationService _classificationService;
        private readonly IDishTypeService _dishTypeService;
        private readonly IRestaurantService _restaurantService;
        private readonly IDishesService _dishesService;
        private readonly IUsersRepository _usersRepository;



        public BackEndController(IClassificationService classificationService,IDishTypeService dishTypeService, IRestaurantService restaurantService,IDishesService dishesService, IUsersRepository usersRepository)
        {
            _classificationService = classificationService;
            _dishTypeService = dishTypeService;
            _restaurantService = restaurantService;
            _dishesService = dishesService;
            _usersRepository = usersRepository;
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
        public async Task<ActionResult> ManageRestaurants(Restaurant restaurant)
        {
            ViewBag.mssg = TempData["mssg"] as string;
            ViewBag.mssgEdit = TempData["mssgEdit"] as string;
            var restaurants = await _restaurantService.GetRestaurants();
            return View(restaurants);
        }

        [HttpGet]
        public async Task<ActionResult> RestaurantDetails(string id)
        {
            string restId = "";
            if(id == "" || id == null)
            {
                var referrerUrl = Request.Headers["Referer"].ToString();
                var uriBuilder = new UriBuilder(referrerUrl);
                var queryParams = HttpUtility.ParseQueryString(uriBuilder.Query);
                restId = queryParams["id"];
            }
            else
            {
                restId= id;
            }
            ViewBag.mssg = TempData["mssg"] as string;
            ViewBag.mssgEdit = TempData["mssgEdit"] as string;
            var restaurant = await _restaurantService.GetRestaurantById(restId);
            var dishes = await _dishesService.GetDishesByRestaurantAsync(restaurant);

            var model = new RestaurantDishes
            {
                Restaurant = restaurant,
                Dishes = dishes
            };
            return View(model);
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
                return RedirectToAction("ManageDishTypes", "BackEnd");
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
                return RedirectToAction("ManageDishTypes", "BackEnd");
            }
        }
        [HttpGet]
        public ActionResult _AddDishType()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<ActionResult> _AddDishType(DishTypeModel dishTypeModel)
        {
            string message = string.Empty;
            var user = User.Identity.Name;

            if (ModelState.IsValid)
            {
                dishTypeModel.UpdatedBy = user;
                dishTypeModel.CreatedBy = user;
                var response = await _dishTypeService.AddDishType(dishTypeModel);
                if (!response.IsSuccess) TempData["mssg"] = response.Message;
                return RedirectToAction("ManageDishTypes", "BackEnd");
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
                return RedirectToAction("ManageDishTypes", "BackEnd");
            }

        }
        [HttpGet]

        public async Task<ActionResult> _EditDishType(string id)
        {
            var dishType = new DishTypeModel();
            var result = await _dishTypeService.GetDishTypeById(id);
            dishType.DishTypeName = result.DishTypeName;
            return PartialView(dishType);
        }

        [HttpPost]

        public async Task<ActionResult> _EditDishType(DishTypeModel dishTypeModel)
        {
            string message = string.Empty;
            var user = User.Identity.Name;

            if (ModelState.IsValid)
            {
                dishTypeModel.UpdatedBy = user;
                dishTypeModel.CreatedBy = user;
                var response = await _dishTypeService.EditDishType(dishTypeModel);
                if (!response.IsSuccess) TempData["mssgEdit"] = response.Message;
                return RedirectToAction("ManageDishTypes", "BackEnd");
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
                return RedirectToAction("ManageDishTypes", "BackEnd");
            }
        }

        [HttpGet]
        public async Task<ActionResult> _AddRestaurantAsync()
        {
            var restClassifications = await _classificationService.GetClassifications();
            var viewModel = new RestaurantModel
            {
                Classifications = restClassifications.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.ClassificationName
                })
            };
            return PartialView(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> _AddRestaurant(RestaurantModel restaurantModel)
        {
            string message = string.Empty;
            var user = User.Identity.Name;

            if (ModelState.IsValid)
            {
                restaurantModel.UpdatedBy = user;
                restaurantModel.CreatedBy = user;
                var response = await _restaurantService.AddRestaurant(restaurantModel);
                if (!response.IsSuccess) TempData["mssg"] = response.Message;
                return RedirectToAction("ManageRestaurants", "BackEnd");
            }
            else
            {
                string errorMessageString = "";
                foreach (var entry in ModelState)
                {
                    var errorMessages = entry.Value.Errors.Select(e => e.ErrorMessage);
                    if (entry.Key == "SelectedClassificationsId") errorMessageString = "Please Select Classification";
                    else errorMessageString = string.Join("; ", errorMessages);

                }
                TempData["mssg"] = errorMessageString;
                return RedirectToAction("ManageRestaurants", "BackEnd");
            }

        }
        [HttpGet]

        public async Task<ActionResult> _EditRestaurant(string id)
        {
            var result = await _restaurantService.GetRestaurantById(id);
            var restClassifications = await _classificationService.GetClassifications();
            var viewModel = new RestaurantModel
            {
                Classifications = restClassifications.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.ClassificationName
                }),
                RestaurantName = result.RestaurantName,
                SelectedClassificationsId = result.RestaurantClassification.Id,
                Id = id
            };
      
            return PartialView(viewModel);
        }

        [HttpPost]

        public async Task<ActionResult> _EditRestaurant(RestaurantModel restaurantModel)
        {
            string message = string.Empty;
            var user = User.Identity.Name;

            if (ModelState.IsValid)
            {
                restaurantModel.UpdatedBy = user;
                restaurantModel.CreatedBy = user;
                var response = await _restaurantService.EditRestaurant(restaurantModel);
                if (!response.IsSuccess) TempData["mssgEdit"] = response.Message;
                return RedirectToAction("ManageRestaurants", "BackEnd");
            }
            else
            {
                string errorMessageString = "";
                foreach (var entry in ModelState)
                {
                    var errorMessages = entry.Value.Errors.Select(e => e.ErrorMessage); // Get the error messages for the
                    if (entry.Key == "SelectedClassificationsId") errorMessageString = "Please Select Classification";
                    else errorMessageString = string.Join("; ", errorMessages);
                }
                TempData["mssgEdit"] = errorMessageString;
                return RedirectToAction("ManageRestaurants", "BackEnd");
            }
        }
        [HttpGet]
        public async Task<ActionResult> _RestaurantDetails()
        {
            var restClassifications = await _classificationService.GetClassifications();
            var viewModel = new RestaurantModel
            {
                Classifications = restClassifications.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.ClassificationName
                })
            };
            return PartialView(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> _AddDishAsync()
        {
            var dishTypes = await _dishTypeService.GetDishTypes();
            var viewModel = new DishModel
            {
                DishTypes = dishTypes.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.DishTypeName
                }),
            };
            return PartialView(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> _AddDish(DishModel dishModel)
        {
            string message = string.Empty;
            var user = User.Identity.Name;

            if (ModelState.IsValid)
            {
                var referrerUrl = Request.Headers["Referer"].ToString();
                var uriBuilder = new UriBuilder(referrerUrl);
                var queryParams = HttpUtility.ParseQueryString(uriBuilder.Query);
                var id = queryParams["id"];

                dishModel.UpdatedBy = user;
                dishModel.CreatedBy = user;
                dishModel.RestaurantId = id;
                var response = await _dishesService.AddDish(dishModel);
                if (!response.IsSuccess) TempData["mssg"] = response.Message;
                return RedirectToAction("RestaurantDetails", "BackEnd");
            }
            else
            {
                string errorMessageString = "";
                foreach (var entry in ModelState)
                {
                    var errorMessages = entry.Value.Errors.Select(e => e.ErrorMessage); // Get the error messages for the
                    if (entry.Key == "SelectedDishTypeId") errorMessageString = "Please Select Dish Type";
                    else errorMessageString = string.Join("; ", errorMessages);

                }
                TempData["mssg"] = errorMessageString;
                return RedirectToAction("RestaurantDetails", "BackEnd");
            }

        }
        [HttpGet]

        public async Task<ActionResult> _EditDish(string id)
        {
            var result = await _dishesService.GetDishById(id);
            var dishTypes = await _dishTypeService.GetDishTypes();
            var viewModel = new DishModel
            {
                DishTypes = dishTypes.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.DishTypeName
                }),
                DishName = result.DishName,
                DishDescription= result.DishDescription,
                SelectedDishTypeId = result.DishType.Id,
                Id = id
            };

            return PartialView(viewModel);
        }

        [HttpPost]

        public async Task<ActionResult> _EditDish(DishModel dishModel)
        {
            string message = string.Empty;
            var user = User.Identity.Name;

            if (ModelState.IsValid)
            {
                var referrerUrl = Request.Headers["Referer"].ToString();
                var uriBuilder = new UriBuilder(referrerUrl);
                var queryParams = HttpUtility.ParseQueryString(uriBuilder.Query);
                var id = queryParams["id"];
                dishModel.UpdatedBy = user;
                dishModel.CreatedBy = user;
                dishModel.RestaurantId = id;
                var response = await _dishesService.EditDish(dishModel);
                if (!response.IsSuccess) TempData["mssgEdit"] = response.Message;
                return RedirectToAction("RestaurantDetails", "BackEnd");
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
                return RedirectToAction("RestaurantDetails", "BackEnd");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ReportsAsync()
        {
            return View();
        }



    }
}
