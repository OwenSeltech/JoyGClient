using JoyGClient.Interfaces;
using JoyGClient.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using JoyGClient.Entities;

namespace JoyGClient.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly SignInManager<AppUser> _signInManager;
        public AuthController(IAuthService authService, SignInManager<AppUser> signInManager)
        {
            _authService = authService;
            _signInManager = signInManager;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(LoginModel loginModel)
        {
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                if (Request.Method == "POST")
                {
                    var userDto = await _authService.Login(loginModel);
                    if (userDto.Message != "Success")
                    {
                        message = userDto.Message;
                    }
                    else
                    {
                        Response.Cookies.Append(
                           CookieAuthenticationDefaults.AuthenticationScheme,
                        userDto.Token,
                           new CookieOptions { HttpOnly = true, Expires = DateTimeOffset.UtcNow.AddDays(7) });

                        if (userDto.Roles.ElementAt(0) == "DataAdmin") return RedirectToAction("Index", "Dashboard");
                    }
                   
                }
            }
            else
            {
                message = "Something Went Wrong";
                ViewBag.Message = message;
            }
            return View(loginModel);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel registerModel)
        {
            string message = string.Empty;
            
            if (ModelState.IsValid)
            {
                if (Request.Method == "POST")
                {
                    registerModel.CreatedBy = "Web";
                    registerModel.UserRole = "DataAdmin";
                    var userDto = await _authService.Register(registerModel);
                    if (userDto.Message != "Success")
                    {
                        message = userDto.Message;
                    }
                    else
                    {
                        Response.Cookies.Append(
                          CookieAuthenticationDefaults.AuthenticationScheme,
                       userDto.Token,
                          new CookieOptions { HttpOnly = true, Expires = DateTimeOffset.UtcNow.AddDays(7) });
                    }
                    //ViewBag.Message = message;
                }
            }
            else
            {
                message = "Something Went Wrong";
                ViewBag.Message = message;
            }
            return View(registerModel);
        }


    }
}
