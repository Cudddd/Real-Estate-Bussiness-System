using System.Threading.Tasks;
using BDS.Services.Facades.Services;
using BDS.WebApp.Models.AreaViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BDS.WebApp.Controllers
{
    public class AreaController : Controller
    {
        private readonly IAreaFacade _areaFacade;
        public AreaController(IAreaFacade areaFacade)
        {
            _areaFacade = areaFacade;
        }
        // GET
        public async Task<IActionResult> Index(long id, string name,int pageIndex = 1)
        {
            var result = await _areaFacade.GetAreaService(User,id, name, pageIndex);

            ViewBag.HighlightProjects = result.ListProject;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                ViewBag.wishlist = result.ListWishList;
            }
            ViewBag.AreaName = name;
            ViewBag.AreaId = id;
            AreaViewModel areaViewModel = new AreaViewModel();

            areaViewModel.realEstates = result.ListRealEstate;
            areaViewModel.pageIndex = pageIndex;

            return View(areaViewModel);
        }
    }
}