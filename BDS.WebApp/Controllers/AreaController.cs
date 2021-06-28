using System;
using BDS.Services.Area;
using BDS.Services.Project;
using BDS.Services.RealEstate;
using BDS.WebApp.Models.AreaViewModel;
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
        public IActionResult Index(long id, string name,int pageIndex = 1)
        {
            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;
            ViewBag.AreaName = name;
            ViewBag.AreaId = id;
            AreaViewModel areaViewModel = new AreaViewModel();

            areaViewModel.realEstates = _realEstateService.GetByAreaId(id,pageIndex,6).Result;
            areaViewModel.pageIndex = pageIndex;

            return View(areaViewModel);
        }
    }
}