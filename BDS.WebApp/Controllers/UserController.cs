using System;
using System.Linq;
using System.Security.Claims;
using BDS.Data.Entities;
using BDS.Services.Project;
using BDS.Services.RealEstate;
using BDS.Services.Request;
using BDS.Services.User;
using BDS.Services.Wishlist;
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
        private readonly IWishlistService _wishlistService;
        public UserController(
            IUserServiceAbstractFactory userServiceAbstractFactory, 
            IProjectAbstractFactory projectAbstractFactory, 
            IWishlistServiceAbtractFactory wishlistServiceAbtractFactory
            )
        {
            _userService = userServiceAbstractFactory.CreateUserService();
            _projectService = projectAbstractFactory.CreateProjectServices();
            _wishlistService = wishlistServiceAbtractFactory.CreateWishlistService();
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                long userId = _userService.GetUserId(User);
                ViewBag.wishlist = _wishlistService.GetByUserId(userId).Result;
            }
            
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                long userId = _userService.GetUserId(User);
                ViewBag.wishlist = _wishlistService.GetByUserId(userId).Result;
            }

            return View();
        }
        [HttpPost]
        public IActionResult Reg([FromForm] UserCreateRequest request)
        {
            var result =  _userService.Register(request).Result;

            if (result)
            {
                var rs =  _userService.Authenticate(request.UserName, request.Password, true).Result;
            
                if(rs)
                    return LocalRedirect("~/");
            }
            return LocalRedirect("~/User/Register");
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
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                long userId = _userService.GetUserId(User);
                ViewBag.wishlist = _wishlistService.GetByUserId(userId).Result;
            }
            var user = _userService.GetCurrentUser(User).Result;
            
            return View(user);
        }

        [Authorize]
        public IActionResult UpdateInfomation()
        {
            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                long userId = _userService.GetUserId(User);
                ViewBag.wishlist = _wishlistService.GetByUserId(userId).Result;
            }
            var user = _userService.GetCurrentUser(User).Result;
            
            return View(user);
        }
        [Authorize]
        public IActionResult ChangePassword()
        {
            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                long userId = _userService.GetUserId(User);
                ViewBag.wishlist = _wishlistService.GetByUserId(userId).Result;
            }
            var user = _userService.GetCurrentUser(User).Result;
            
            return View(user);
        }
        
        [Authorize]
        public IActionResult RealEstate()
        {
            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                long userId = _userService.GetUserId(User);
                ViewBag.wishlist = _wishlistService.GetByUserId(userId).Result;
            }
           
            RealEstateViewModel model = new RealEstateViewModel();
            model.user = _userService.GetCurrentUser(User).Result;
            model.realEstates = _userService.GetAllUserRealEstate(model.user.Id).Result;
            
            return View(model);
        }
        
        [Authorize]
        public IActionResult RealEstateDetail(long id)
        {
            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                long userId = _userService.GetUserId(User);
                ViewBag.wishlist = _wishlistService.GetByUserId(userId).Result;
            }
            var model = _userService.GetUserRealEstateById(id).Result;
            
            return View(model);
        }
    }
}