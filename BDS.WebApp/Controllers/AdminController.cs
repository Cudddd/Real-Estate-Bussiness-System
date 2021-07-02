using System;
using System.Linq;
using BDS.Data.Entities;
using BDS.Services.Area;
using BDS.Services.News;
using BDS.Services.Project;
using BDS.Services.RealEstate;
using BDS.Services.Recruitment;
using BDS.Services.Request;
using BDS.WebApp.Models.Admin;
using BDS.WebApp.Models.RealEstate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BDS.WebApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IAreaService _areaService;
        private readonly IRealEstateService _realEstateService;
        private readonly INewsService _newsService;
        private readonly IRecruitmentService _recruitmentService;
        private readonly UserManager<User> _userManager;

        public AdminController(IProjectService projectService,IAreaService areaService,
            IRealEstateService realEstateService,INewsService newsService,
            IRecruitmentService recruitmentService,UserManager<User> userManager)
        {
            _projectService = projectService;
            _areaService = areaService;
            _realEstateService = realEstateService;
            _newsService = newsService;
            _recruitmentService = recruitmentService;
            _userManager = userManager;
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
        [HttpPost]
        public IActionResult UpdateProjectPost([FromForm] Project request,long id)
        {
            request.id = id;
            
            string ProcedureVideo = Request.Form["ProcedureVideo"];
            string IntroduceVideo = Request.Form["IntroduceVideo"];

            _projectService.Update(request);

            return LocalRedirect("~/Admin/ProjectDetail/"+id);
        }

        public IActionResult DeleteProject(long id)
        {
            var rs = _projectService.Delete(id).Result;
            return LocalRedirect("~/Admin/Project/");
        }

        public IActionResult NewProject()
        {
            return View();
        }
        [HttpPost]
        public IActionResult NewProjectPost([FromForm] ProjectCreateRequest request)
        {

            var rs = _projectService.Create(request).Result;
            return LocalRedirect("~/Admin/Project");
        }
        public IActionResult HighlightProject()
        {
            var model = _projectService.GetHighlightProject().Result;
            
            return View(model);
        }
        public IActionResult AddHighlightProject()
        {
            var projects = _projectService.GetAll().Result;

            var model = projects.Where(x => x.highlight == false).ToList();

            return View(model);
        }
        public IActionResult AddHighlightProjectPost([FromForm] string id)
        {
            var rs = _projectService.SetHighlightProject(long.Parse(id), true).Result;
            //Console.WriteLine(rs);
            return LocalRedirect("~/Admin/HighlightProject");
        }
        public IActionResult RemoveHighlightProject(long id)
        {
            var rs = _projectService.SetHighlightProject(id, false).Result;
            return LocalRedirect("~/Admin/HighlightProject");
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

        public IActionResult News(int pageIndex = 1)
        {
            var model = _newsService.GetAllPaging(pageIndex,6).Result;
            
            return View(model);
        }
        
        public IActionResult NewsDetail(int id)
        {
            var model = _newsService.GetById(id).Result;
            
            return View(model);
        }

        public IActionResult AddNews()
        {
            return View();
        }

        public IActionResult Recruitment(int pageIndex = 1)
        {
            var model = _recruitmentService.GetAllPaging(pageIndex,6).Result;

            return View(model);
        }

        public IActionResult RecruitmentDetail(int id)
        {
            var model = _recruitmentService.GetById(id).Result;

            return View(model);
        }
        public IActionResult NewRecruitment()
        {
            return View();
        }

        public IActionResult User(int pageIndex = 1)
        {
            var model = _userManager.Users.ToList();
            model = model.Skip((pageIndex - 1) * 6).Take(6).ToList();
            
            return View(model);
        }
        public IActionResult UserDetail(long id)
        {
            var model = _userManager.FindByIdAsync(id.ToString()).Result;
            return View(model);
        }
        public IActionResult NewUser()
        {
            return View();
        }
        
    }
}