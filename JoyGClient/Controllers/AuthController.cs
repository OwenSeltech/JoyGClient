using JoyGClient.DTOs;
using JoyGClient.Interfaces;
using JoyGClient.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using System.Net.Http;
using System.Security.Claims;
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
                        // Create a new cookie authentication ticket with the JWT token
                        //var ticket = new AuthenticationTicket(
                        //    new ClaimsPrincipal(userDto.claims),
                        //    new AuthenticationProperties(),
                        //    CookieAuthenticationDefaults.AuthenticationScheme);

                        Response.Cookies.Append(
                           CookieAuthenticationDefaults.AuthenticationScheme,
                        userDto.Token,
                           new CookieOptions { HttpOnly = true, Expires = DateTimeOffset.UtcNow.AddDays(7) });

                        //await _signInManager.SignInAsync();
                        //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, ticket);

                        //return RedirectToAction("Index", "Home");
                    }
                    ViewBag.Message = message;
                }
            }
            else
            {

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
                    registerModel.UserRole = "EndUser";
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
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    if (state.Errors.Any())
                    {
                        var result = false;
                        // There are validation errors for this property
                        // You can access the error messages using state.Errors
                    }
                }

                message = "Something Went Wrong";
                ViewBag.Message = message;
            }
            return View(registerModel);
        }


    }
}
