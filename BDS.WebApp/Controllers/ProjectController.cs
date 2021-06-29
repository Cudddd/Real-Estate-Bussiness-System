using BDS.Data.Enum;
using BDS.Services.Area;
using BDS.Services.Project;
using BDS.Services.User;
using BDS.Services.Wishlist;
using BDS.WebApp.Models.Project;
using Microsoft.AspNetCore.Mvc;

namespace BDS.WebApp.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IAreaService _areaService;
        private readonly IWishlistService _wishlistService;
        private readonly IUserService _userService;
        
        public ProjectController(IProjectService projectService,IAreaService areaService, 
            IWishlistService wishlistService,IUserService userService)
        {
            _projectService = projectService;
            _areaService = areaService;
            _wishlistService = wishlistService;
            _userService = userService;
        }
        // GET
        public IActionResult Index()
        {
            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;
            
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                long userId = _userService.GetUserId(User);
                ViewBag.wishlist = _wishlistService.GetByUserId(userId).Result;
            }

            ProjectViewModel projectViewModel = new ProjectViewModel();
            projectViewModel.Projects = _projectService.FilterByInvesloper("Vinhomes").Result;
            projectViewModel.projectBanners = _projectService.GetProjectBanner().Result;
            
            return View(projectViewModel);
        }
        
        // GET Detail Project
        public IActionResult Detail(long id)
        {
            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                long userId = _userService.GetUserId(User);
                ViewBag.wishlist = _wishlistService.GetByUserId(userId).Result;
            }

            ProjectDetailViewModel projectDetailViewModel = new ProjectDetailViewModel();

            projectDetailViewModel.project = _projectService.GetById(id).Result;
            projectDetailViewModel.areas = _areaService.GetByProjectId(id).Result;
            var Medias = _projectService.GetProjectMedia(id).Result;

            if (Medias.Count < 6)
            {
                foreach (var item in Medias)
                {
                    if (item.Type == (int) MediaType.BannerImg)
                        projectDetailViewModel.projectBanner = item;
                    else if (item.Type == MediaType.IntroduceVideo)
                        projectDetailViewModel.introduceVideo = item;
                    else if (item.Type == MediaType.MapImg)
                        projectDetailViewModel.mapImg = item;
                    else if (item.Type == MediaType.ProcedureVideo)
                        projectDetailViewModel.procedureVideo = item;
                    else if (item.Type == MediaType.IntroduceImg)
                        projectDetailViewModel.introduceImg = item;
                }
                
            }
            return View(projectDetailViewModel);
        }

        public IActionResult MoreProject(string invesloper = "Vinhomes")
        {
            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;
            
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                long userId = _userService.GetUserId(User);
                ViewBag.wishlist = _wishlistService.GetByUserId(userId).Result;
            }
            
            ProjectViewModel projectViewModel = new ProjectViewModel();
            if(invesloper != "Vinhomes")
                projectViewModel.Projects = _projectService.FilterOtherInvesloper().Result;
            else projectViewModel.Projects = _projectService.FilterByInvesloper("Vinhomes").Result;
            projectViewModel.projectBanners = _projectService.GetProjectBanner().Result;
            
            return View(projectViewModel);
        }

    }
}