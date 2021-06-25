using BDS.Data.Enum;
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
        
    }
}