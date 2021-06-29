using BDS.Services.Area;
using BDS.Services.News;
using BDS.Services.Project;
using BDS.Services.RealEstate;
using BDS.WebApp.Models.Admin;
using BDS.WebApp.Models.RealEstate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BDS.WebApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IAreaService _areaService;
        private readonly IRealEstateService _realEstateService;
        private readonly INewsService _newsService;

        public AdminController(IProjectService projectService,IAreaService areaService,
            IRealEstateService realEstateService,INewsService newsService)
        {
            _projectService = projectService;
            _areaService = areaService;
            _realEstateService = realEstateService;
            _newsService = newsService;
        }
        // GET
        //[Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Project()
        {
            var model = _projectService.GetAll().Result;
            
            return View(model);
        }
        public IActionResult ProjectDetail(long id)
        {
          
            ProjectDetailViewModel model = new ProjectDetailViewModel();
            model.Project = _projectService.GetById(id).Result;
            model.Media = _projectService.GetProjectMedia(id).Result;
            
            return View(model);
        }

        public IActionResult UpdateProject(long id)
        {
            ProjectDetailViewModel model = new ProjectDetailViewModel();
            model.Project = _projectService.GetById(id).Result;
            model.Media = _projectService.GetProjectMedia(id).Result;
            
            return View(model);
        }

        public IActionResult NewProject()
        {
            return View();
        }

        public IActionResult HighlightProject()
        {
            var model = _projectService.GetHighlightProject().Result;
            
            return View(model);
        }
        public IActionResult ProjectSetting()
        {
            var model = _projectService.GetHighlightProject().Result;
            
            return View();
        }

        public IActionResult Area(int pageIndex = 1)
        {
            var model = _areaService.GetAllPaging(pageIndex,6).Result;
            
            return View(model);
        }
        public IActionResult NewArea()
        {
            var model = _projectService.GetAll().Result;
            
            return View(model);
        }
        
        public IActionResult RealEstate(int pageIndex = 1)
        {
            var model = _realEstateService.GetAllPaging(pageIndex,6).Result;
            
            return View(model);
        }
        public IActionResult RealEstateDetail(long id)
        {
           
            var model = _realEstateService.GetById(id).Result;
            
            return View(model);
        }

        public IActionResult NewRealEstate()
        {
            RealEstateSellViewModel model = new RealEstateSellViewModel();
            model.realEstateTypes = _realEstateService.GetAllRealEstateType().Result;
            return View(model);
        }

        public IActionResult News(int pageIndex)
        {
            var model = _newsService.GetAllPaging(pageIndex,6).Result;
            
            return View(model);
        }
        
        public IActionResult NewsDetail(int id)
        {
            var model = _newsService.GetById(id).Result;
            
            return View(model);
        }
        
    }
}