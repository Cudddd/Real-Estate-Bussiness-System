using System;
using BDS.Services.User;
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
        [HttpPost]
        public IActionResult Login()
        {
            var username = Request.Form["username"];
            var password = Request.Form["password"];
            var token =  _userService.Authenticate(username, password, true).Result;
            //var rs = _userService.Register().Result;
            if(token == null) Console.WriteLine("null");
            Console.WriteLine(token);
            
            
            return Ok();
        }
    }
}