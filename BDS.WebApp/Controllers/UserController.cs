using System;
using System.Linq;
using BDS.Services.Project;
using BDS.Services.User;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BDS.WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IProjectService _projectService;
        public UserController(IUserService userService, IProjectService projectService)
        {
            _userService = userService;
            _projectService = projectService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;
            
            return View();
        }

        public IActionResult Register()
        {
            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;

            return View();
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
            
            return LocalRedirect("~/User/Login");
        }

        public IActionResult Logout()
        {
            _userService.Logout();
            return LocalRedirect("~/");
        }
    }
}