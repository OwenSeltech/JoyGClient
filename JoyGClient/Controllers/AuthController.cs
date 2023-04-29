using JoyGClient.Interfaces;
using JoyGClient.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using JoyGClient.Entities;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace JoyGClient.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AuthController(IAuthService authService, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _authService = authService;
            _signInManager = signInManager;
            _userManager = userManager;
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
                        var claimsIdentity = new ClaimsIdentity(
                            userDto.claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties
                        {
                            ExpiresUtc = DateTime.Now.AddMinutes(10),
                        };
                        await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

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
                        var claimsIdentity = new ClaimsIdentity(
                            userDto.claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties
                        {
                            ExpiresUtc = DateTime.Now.AddMinutes(10),
                        };
                        await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                        if (userDto.Roles.ElementAt(0) == "DataAdmin") return RedirectToAction("Index", "Dashboard");
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
