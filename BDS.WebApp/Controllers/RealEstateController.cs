using System;
using System.Linq;
using BDS.Data.Enum;
using BDS.Services.Model;
using BDS.Services.Project;
using BDS.Services.RealEstate;
using BDS.Services.Request;
using BDS.Services.User;
using BDS.Services.UserRealEstate;
using BDS.Services.Wishlist;
using BDS.WebApp.Models.RealEstate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BDS.WebApp.Controllers
{
    public class RealEstateController : Controller
    {
        private readonly IRealEstateService _realEstateService;
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;
        private readonly IWishlistService _wishlistService;
        private readonly IUserRealEstateService _userRealEstateService;

        public RealEstateController(IRealEstateService realEstateService,
            IProjectService projectService, IWishlistService wishlistService,IUserService userService,
            IUserRealEstateService userRealEstateService)
        {
            _realEstateService = realEstateService;
            _projectService = projectService;
            _userService = userService;
            _wishlistService = wishlistService;
            _userRealEstateService = userRealEstateService;
        }
        // GET
        public IActionResult Index(int pageIndex = 1)
        {
           // pageIndex = 2;

            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                long userId = _userService.GetUserId(User);
                ViewBag.wishlist = _wishlistService.GetByUserId(userId).Result;
            }

            RealEstateViewModel realEstateViewModel = new RealEstateViewModel();
            realEstateViewModel.realEstates = _realEstateService.GetAllPaging(pageIndex, 6).Result;
            realEstateViewModel.pageIndex = pageIndex;

            return View(realEstateViewModel);
        }

        public IActionResult Detail(long id)
        {
            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                long userId = _userService.GetUserId(User);
                ViewBag.wishlist = _wishlistService.GetByUserId(userId).Result;
            }

            var data = _realEstateService.GetById(id).Result;

            var temp = data.realEstateMedia.Where(x => x.Type == MediaType.NormalImg).ToList();
            data.realEstateMedia = temp;

            return View(data);
        }


        [Authorize]
        public IActionResult Sell(long id = -1)
        {
            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                long userId = _userService.GetUserId(User);
                ViewBag.wishlist = _wishlistService.GetByUserId(userId).Result;
            }

            RealEstateSellViewModel model = new RealEstateSellViewModel();
            model.realEstateTypes = _realEstateService.GetAllRealEstateType().Result;
            UserRealEstateModel userRealEstateModel = new UserRealEstateModel();
            if(id != -1)
                userRealEstateModel = _userService.GetUserRealEstateById(id).Result;

            if (userRealEstateModel != null && userRealEstateModel.id != 0)
            {
                model.realEstateModel = new RealEstateModel();
                model.realEstateModel.id = userRealEstateModel.id;
                model.realEstateModel.name = userRealEstateModel.name;
                model.realEstateModel.sell = userRealEstateModel.sell;
                model.realEstateModel.length = userRealEstateModel.length;
                model.realEstateModel.width = userRealEstateModel.width;
                model.realEstateModel.orientation = userRealEstateModel.orientation;
                model.realEstateModel.acreage = userRealEstateModel.acreage;
                model.realEstateModel.price = userRealEstateModel.price;
                model.realEstateModel.location = userRealEstateModel.location;
                model.realEstateModel.type = userRealEstateModel.type;
                model.realEstateModel.facade = userRealEstateModel.facade;
                model.realEstateModel.mainLine = userRealEstateModel.mainLine;
                model.realEstateModel.floor = userRealEstateModel.floor;
                model.realEstateModel.bedRoom = userRealEstateModel.bedRoom;
                model.realEstateModel.bathRoom = userRealEstateModel.bathRoom;
                model.realEstateModel.DateCreated = userRealEstateModel.DateCreated;
                model.realEstateModel.DateModify = userRealEstateModel.DateModify;
                model.realEstateModel.address = userRealEstateModel.address;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult CreateRealEstate([FromForm] RealEstateCreateRequest request)
        {
            var user = _userService.GetCurrentUser(User).Result;
            var rs = _userRealEstateService.Create(request,user.Id).Result;
            return LocalRedirect("~/User/RealEstate/");
        }

    }
}