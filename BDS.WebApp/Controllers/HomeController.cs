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
using BDS.Services.Facades.Services;

namespace BDS.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeFacade _homeFacade;

        public HomeController(
            IHomeFacade homeFacade
         )
        {
            _homeFacade=homeFacade;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _homeFacade.GetData(User);
            ViewBag.HighlightProjects = result.ListProjects;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                ViewBag.wishlist = result.ListWishList;
            }
            
            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            var result = await _homeFacade.GetData(User);
            ViewBag.HighlightProjects = result.ListProjects;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                ViewBag.wishlist = result.ListWishList;
            }
            
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}