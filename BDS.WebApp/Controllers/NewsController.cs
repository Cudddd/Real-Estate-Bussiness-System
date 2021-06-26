using BDS.Services.News;
using BDS.Services.Project;
using Microsoft.AspNetCore.Mvc;

namespace BDS.WebApp.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly IProjectService _projectService;

        public NewsController(INewsService newsService, IProjectService projectService)
        {
            _newsService = newsService;
            _projectService = projectService;
        }
        // GET
        public IActionResult Index(int pageIndex)
        {
            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;

            var data = _newsService.GetAllPaging(pageIndex, 4).Result;

            return View(data);
        }

        public IActionResult Detail(long id)
        {
            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;

            var data = _newsService.GetById(id).Result;
            
            return View(data);
        }
        
        
    }
}