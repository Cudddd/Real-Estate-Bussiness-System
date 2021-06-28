using System;
using System.Linq;
using System.Security.Claims;
using BDS.Services.Project;
using BDS.Services.RealEstate;
using BDS.Services.User;
using BDS.WebApp.Models.User;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public IActionResult Infomation()
        {
            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;
            var user = _userService.GetCurrentUser(User).Result;
            
            return View(user);
        }

        [Authorize]
        public IActionResult UpdateInfomation()
        {
            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;
            var user = _userService.GetCurrentUser(User).Result;
            
            return View(user);
        }
        [Authorize]
        public IActionResult ChangePassword()
        {
            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;
            var user = _userService.GetCurrentUser(User).Result;
            
            return View(user);
        }
        
        [Authorize]
        public IActionResult RealEstate()
        {
            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;
           
            RealEstateViewModel model = new RealEstateViewModel();
            model.user = _userService.GetCurrentUser(User).Result;
            model.realEstates = _userService.GetAllUserRealEstate(model.user.Id).Result;
            
            return View(model);
        }
        
        [Authorize]
        public IActionResult RealEstateDetail(long id)
        {
            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;
            var model = _userService.GetUserRealEstateById(id).Result;
            
            return View(model);
        }
    }
}