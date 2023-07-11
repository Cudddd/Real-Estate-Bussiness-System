using System;
using BDS.Services.AbtractFactory;
using BDS.Services.Area;
using BDS.Services.Project;
using BDS.Services.RealEstate;
using BDS.Services.User;
using BDS.Services.Wishlist;
using BDS.WebApp.Models.AreaViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BDS.WebApp.Controllers
{
    public class AreaController : Controller
    {
        private readonly IAreaService _areaService;
        private readonly IProjectService _projectService;
        private readonly IRealEstateService _realEstateService;
        private readonly IWishlistService _wishlistService;
        private readonly IUserService _userService;

        public AreaController(
            IProjectService projectService,
            IRealEstateService realEstateService,
            IWishlistService wishlistService,
            IUserService userService,
            IServiceFactory serviceFactory)
        {
            _areaService = serviceFactory.CreateAreaService(Area.V1);
            _projectService = projectService;
            _realEstateService = realEstateService;
            _wishlistService = wishlistService;
            _userService = userService;
        }
        // GET
        public IActionResult Index(long id, string name, int pageIndex = 1)
        {
            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                long userId = _userService.GetUserId(User);
                ViewBag.wishlist = _wishlistService.GetByUserId(userId).Result;
            }
            ViewBag.AreaName = name;
            ViewBag.AreaId = id;
            AreaViewModel areaViewModel = new AreaViewModel();

            areaViewModel.realEstates = _realEstateService.GetByAreaId(id, pageIndex, 6).Result;
            areaViewModel.pageIndex = pageIndex;

            return View(areaViewModel);
        }
    }
}