using BDS.Services.Project;
using Microsoft.AspNetCore.Mvc;

namespace BDS.WebApp.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        // GET
        public IActionResult Index()
        {
            return View(_projectService.GetHighlightProject().Result);
        }
        
        // GET Detail Project
        public IActionResult DetailProject(long id)
        {
            var project = _projectService.GetById(id);
            return View(project.Result);
        }
    }
}