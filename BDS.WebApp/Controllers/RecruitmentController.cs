using BDS.Data.Entities;
using BDS.Services.Project;
using BDS.Services.Recruitment;
using BDS.Services.User;
using BDS.Services.Wishlist;
using Microsoft.AspNetCore.Mvc;

namespace BDS.WebApp.Controllers
{
    public class RecruitmentController : Controller
    {
        private readonly IRecruitmentService _recruitmentService;
        private readonly IProjectService _projectService;
        private readonly IWishlistService _wishlistService;
        private readonly IUserService _userService;

        public RecruitmentController(
            IReacruitmentServiceAbstractFactory reacruitmentServiceAbstractFactory, 
            IProjectAbstractFactory projectAbstractFactory, 
            IWishlistServiceAbtractFactory wishlistServiceAbtractFactory,
            IUserServiceAbstractFactory userServiceAbstractFactory
            )
        {
            _recruitmentService = reacruitmentServiceAbstractFactory.CreateRecruitmentService();
            _projectService = projectAbstractFactory.CreateProjectServices();
            _wishlistService = wishlistServiceAbtractFactory.CreateWishlistService();
            _userService = userServiceAbstractFactory.CreateUserService();
        }
        // GET
        public IActionResult Index(int pageIndex = 1)
        {
            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                long userId = _userService.GetUserId(User);
                ViewBag.wishlist = _wishlistService.GetByUserId(userId).Result;
            }
            ViewBag.pageIndex = pageIndex;
            var data = _recruitmentService.GetAllPaging(pageIndex, 4).Result;
            
            return View(data);
        }
        
        public IActionResult Detail(long id)
        {
            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                long userId = _userService.GetUserId(User);
                ViewBag.wishlist = _wishlistService.GetByUserId(userId).Result;
            }

            var data = _recruitmentService.GetById(id).Result;

            
            return View(data);
        }
        
    }
}