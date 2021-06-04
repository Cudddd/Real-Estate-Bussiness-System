using BDS.Services.Project;
using BDS.Services.RealEstate;
using Microsoft.AspNetCore.Mvc;

namespace BDS.WebApp.Controllers
{
    public class RealEstateController : Controller
    {
        private readonly IRealEstateService _realEstateService;

        private readonly IProjectService _projectService;

        RealEstateController(IRealEstateService realEstateService,IProjectService projectService)
        {
            _realEstateService = realEstateService;
            _projectService = projectService;
        }
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}