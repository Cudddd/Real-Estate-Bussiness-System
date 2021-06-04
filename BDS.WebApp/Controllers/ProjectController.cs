using BDS.Services.Area;
using BDS.Services.Project;
using BDS.WebApp.Models.Project;
using Microsoft.AspNetCore.Mvc;

namespace BDS.WebApp.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IAreaService _areaService;
        
        public ProjectController(IProjectService projectService,IAreaService areaService)
        {
            _projectService = projectService;
            _areaService = areaService;
        }
        // GET
        public IActionResult Index()
        {
            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;

            ProjectViewModel projectViewModel = new ProjectViewModel();
            projectViewModel.VinhomesProjects = _projectService.FilterByInvesloper("Vinhomes").Result;
            
            return View(projectViewModel);
        }
        
        // GET Detail Project
        public IActionResult Detail(long id)
        {
            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;

            ProjectDetailViewModel projectDetailViewModel = new ProjectDetailViewModel();
            
            projectDetailViewModel.project = _projectService.GetById(id).Result;
            projectDetailViewModel.areas = _areaService.GetByProjectId(id).Result;
            
            return View(projectDetailViewModel);
        }
        
    }
}