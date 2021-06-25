using System;
using System.Linq;
using BDS.Services.User;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BDS.WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return Ok("Ok");
        }

        [HttpPost]
        public IActionResult Authenticate()
        {
            bool rememberMe = false;
            var username = Request.Form["username"];
            var password = Request.Form["password"];
            var check = Request.Form["rememberMe"];

            if (check.Contains("true")) rememberMe = true;
            
            var result =  _userService.Authenticate(username, password, rememberMe).Result;
            
            if(result)
                return LocalRedirect("~/");
            
            return Ok();
        }

        public IActionResult Logout()
        {
            _userService.Logout();
            return LocalRedirect("~/");
        }
    }
}