using System;
using BDS.Services.Area;
using BDS.Services.Project;
using BDS.Services.RealEstate;
using Microsoft.AspNetCore.Mvc;

namespace BDS.WebApp.Controllers
{
    public class AreaController : Controller
    {
        private readonly IAreaService _areaService;
        private readonly IProjectService _projectService;
        private readonly IRealEstateService _realEstateService;

        public AreaController(IAreaService areaService,IProjectService projectService,IRealEstateService realEstateService)
        {
            _areaService = areaService;
            _projectService = projectService;
            _realEstateService = realEstateService;
        }
        // GET
        public IActionResult Index(long id, string name)
        {
            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;
            ViewBag.AreaName = name;

            var realEsate = _realEstateService.GetByAreaId(id).Result;

            return View(realEsate);
        }
    }
}