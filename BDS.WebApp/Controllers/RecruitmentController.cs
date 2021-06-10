using BDS.Services.Project;
using BDS.Services.Recruitment;
using Microsoft.AspNetCore.Mvc;

namespace BDS.WebApp.Controllers
{
    public class RecruitmentController : Controller
    {
        private readonly IRecruitmentService _recruitmentService;

        private readonly IProjectService _projectService;

        public RecruitmentController(IRecruitmentService recruitmentService, IProjectService projectService)
        {
            _recruitmentService = recruitmentService;
            _projectService = projectService;
        }
        // GET
        public IActionResult Index(int pageIndex)
        {
            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;

            var data = _recruitmentService.GetAllPaging(pageIndex, 4).Result;
            
            return View(data);
        }
    }
}