using BDS.Services.News;
using BDS.Services.Project;
using BDS.Services.User;
using BDS.Services.Wishlist;
using Microsoft.AspNetCore.Mvc;

namespace BDS.WebApp.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly IProjectService _projectService;
        private readonly IWishlistService _wishlistService;
        private readonly IUserService _userService;

        public NewsController(INewsServiceAbstractFactory newsAbstractFactory, IProjectAbstractFactory projectAbstractFactory,
            IWishlistServiceAbtractFactory wishlistAbtractFactory, IUserServiceAbstractFactory userAbstractFactory)
        {
            _newsService = newsAbstractFactory.CreateNewService();
            _projectService = projectAbstractFactory.CreateProjectServices();
            _wishlistService = wishlistAbtractFactory.CreateWishlistService();
            _userService = userAbstractFactory.CreateUserService();
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
            var data = _newsService.GetAllPaging(pageIndex, 4).Result;

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

            var data = _newsService.GetById(id).Result;
            
            return View(data);
        }
        
        
    }
}