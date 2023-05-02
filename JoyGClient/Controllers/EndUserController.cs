using JoyGClient.Entities;
using JoyGClient.Interfaces;
using JoyGClient.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web;

namespace JoyGClient.Controllers
{
	public class EndUserController : Controller
	{
        private readonly IRestaurantService _restaurantService;
        private readonly ISurveyService _surveyService;

        public EndUserController(IRestaurantService restaurantService, ISurveyService surveyService)
        {
			_surveyService = surveyService;
			_restaurantService = restaurantService;
           
        }

        [HttpGet]
		public async Task<IActionResult> SurveyAsync()
		{
			var restaurants = await _restaurantService.GetRestaurants();
			var viewModel = new SurveyModel
			{
				Restaurants = restaurants.Select(c => new SelectListItem
				{
					Value = c.Id.ToString(),
					Text = c.RestaurantName
				}),
				DateVisited = DateTime.Now
			};
			return View(viewModel);
		}

        [HttpPost]
        public async Task<IActionResult> SurveyAsync(SurveyModel surveyModel)
        {
            var user = User.Identity.Name;
            string message = string.Empty;
            var model = new SurveyModel();

            var restaurants = await _restaurantService.GetRestaurants();
            surveyModel.Restaurants = restaurants.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.RestaurantName
            });
            var viewModel = new SurveyModel
            {
                Restaurants = restaurants.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.RestaurantName
                })
            };

           if (ModelState.IsValid)
            {
                surveyModel.AmbienceRating = Request.Form["q1"];
                surveyModel.ServiceRating = Request.Form["q2"];
                surveyModel.OverallRating = Request.Form["q3"];
                surveyModel.User = user;

                if (string.IsNullOrEmpty(surveyModel.AmbienceRating))
                {
                    message = "Select Ambience Rating";
                    ViewBag.Message = message;
                    return View(surveyModel);
                }
                if (string.IsNullOrEmpty(surveyModel.ServiceRating))
                {
                    message = "Select Service Rating";
                    ViewBag.Message = message;
                    return View(surveyModel);
                }
                if (string.IsNullOrEmpty(surveyModel.OverallRating))
                {
                    message = "Select Overall Rating";
                    ViewBag.Message = message;
                    return View(surveyModel);
                }

                var response = await _surveyService.AddSurvey(surveyModel);
                if (!response.IsSuccess)
                {
                    message = response.Message;
                    ViewBag.Message = message;
                    return View(surveyModel);
                }
                return RedirectToAction("DishesEnjoyed", "EndUser", new { id = response.Message });
               
            }
            else
            {
                message = "Something Went Wrong";
                ViewBag.Message = message;
                
            }
            return View(surveyModel);
        }
        [HttpGet]
        public async Task<IActionResult> DropRestaurantChanged(string selectedValue)
        {
            var results = await _restaurantService.GetRestaurantById(selectedValue);
            return Json(new { Classification = results.RestaurantClassification.ClassificationName});

        }

        [HttpGet]
        public async Task<IActionResult> DishesEnjoyed(string id)
        {
            //var restaurants = await _restaurantService.GetRestaurants();
            //var viewModel = new SurveyModel
            //{
            //    Restaurants = restaurants.Select(c => new SelectListItem
            //    {
            //        Value = c.Id.ToString(),
            //        Text = c.RestaurantName
            //    }),
            //    DateVisited = DateTime.Now
            //};
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> _AddDishesEnjoyed()
        {
            var restaurants = await _restaurantService.GetRestaurants();
            var viewModel = new SurveyModel
            {
                Restaurants = restaurants.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.RestaurantName
                }),
                DateVisited = DateTime.Now
            };
            return PartialView(viewModel);
        }

    }
}
