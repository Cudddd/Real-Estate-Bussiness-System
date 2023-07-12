using BDS.Services.Facades.Services;
using BDS.Services.News;
using BDS.Services.Project;
using BDS.Services.User;
using BDS.Services.Wishlist;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BDS.WebApp.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsFacade _newsFacade;

        public NewsController(INewsFacade newsFacade)
        {
            _newsFacade = newsFacade;
        }
        // GET
        public async Task<IActionResult> Index(int pageIndex = 1)
        {
            var result = await _newsFacade.GetNews(User, pageIndex);
            ViewBag.HighlightProjects = result.ListProjects;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                ViewBag.wishlist = result.ListWishList;
            }
            ViewBag.pageIndex = pageIndex;
            var data = result.ListNews;

            return View(data);
        }

        public async Task<IActionResult> Detail(long id)
        {
            var result = await _newsFacade.GetNewsDetail(User, id);
            ViewBag.HighlightProjects = result.ListProjects;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                ViewBag.wishlist = result.ListWishList;
            }

            var data = result.New;
            
            return View(data);
        }
        
        
    }
}