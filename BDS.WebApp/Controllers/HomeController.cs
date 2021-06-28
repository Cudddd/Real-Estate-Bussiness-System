using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BDS.Services.Project;
using BDS.Services.User;
using BDS.Services.Wishlist;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BDS.WebApp.Models;

namespace BDS.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IWishlistService _wishlistService;
        private readonly IUserService _userService;

        public HomeController(IProjectService projectService,IWishlistService wishlistService,IUserService userService)
        {
            _projectService = projectService;
            _wishlistService = wishlistService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;
            long userId = _userService.GetUserId(User);
            ViewBag.wishlist = _wishlistService.GetByUserId(userId).Result;
            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}