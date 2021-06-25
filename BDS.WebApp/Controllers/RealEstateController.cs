using System;
using System.Linq;
using BDS.Data.Enum;
using BDS.Services.Project;
using BDS.Services.RealEstate;
using BDS.WebApp.Models.RealEstate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BDS.WebApp.Controllers
{
    public class RealEstateController : Controller
    {
        private readonly IRealEstateService _realEstateService;

        private readonly IProjectService _projectService;

        public RealEstateController(IRealEstateService realEstateService,IProjectService projectService)
        {
            _realEstateService = realEstateService;
            _projectService = projectService;
        }
        // GET
        public IActionResult Index(int pageIndex)
        {
           // pageIndex = 2;

            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;

            RealEstateViewModel realEstateViewModel = new RealEstateViewModel();
            realEstateViewModel.realEstates = _realEstateService.GetAllPaging(pageIndex, 4).Result;

            return View(realEstateViewModel);
        }

        public IActionResult Detail(long id)
        {
            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;

            var data = _realEstateService.GetById(id).Result;

            var temp = data.realEstateMedia.Where(x => x.Type == MediaType.NormalImg).ToList();
            data.realEstateMedia = temp;

            return View(data);
        }


        [Authorize]
        public IActionResult Sell()
        {
            ViewBag.HighlightProjects = _projectService.GetHighlightProject().Result;
            
            return View();
        }

    }
}