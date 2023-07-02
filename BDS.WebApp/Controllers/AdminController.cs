using System;
using System.Linq;
using BDS.Data.Entities;
using BDS.Data.Enum;
using BDS.Services.Area;
using BDS.Services.Model;
using BDS.Services.News;
using BDS.Services.Project;
using BDS.Services.ProjectMedia;
using BDS.Services.RealEstate;
using BDS.Services.Recruitment;
using BDS.Services.Request;
using BDS.Services.Request.News;
using BDS.Services.Request.Recruitment;
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
        private readonly IProjectMediaService _projectMediaService;

        public AdminController(
            IProjectAbstractFactory projectAbstractFactory,
            IAreaServiceAbstractFactory areaServiceAbstractFactory,
            IRealEstateServiceAbstractFactory realEstateServiceAbstractFactory,
            INewsServiceAbstractFactory newsServiceAbstractFactory,
            IReacruitmentServiceAbstractFactory reacruitmentServiceAbstractFactory,UserManager<User> userManager,
            IProjectMediaServiceAbstractFactory projectMediaServiceAbstractFactory
            )
        {
            _projectService = projectAbstractFactory.CreateProjectServices();
            _areaService = areaServiceAbstractFactory.CreateAreaService();
            _realEstateService = realEstateServiceAbstractFactory.CreateRealEstateService();
            _newsService = newsServiceAbstractFactory.CreateNewService();
            _recruitmentService = reacruitmentServiceAbstractFactory.CreateRecruitmentService();
            _userManager = userManager;
            _projectMediaService = projectMediaServiceAbstractFactory.CreateProjectMediaService();
        }
        // GET
        //[Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Project(int index = 1)
        {
            var model = _projectService.GetAllPaging(index,6).Result;
            ViewBag.index = index;
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
            
            string procedureVideo = Request.Form["ProcedureVideo"];
            string introduceVideo = Request.Form["IntroduceVideo"];

            var media = _projectMediaService.GetByProjectId(id).Result;
            foreach (var item in media)
            {
                if (item.Type == MediaType.IntroduceVideo && !String.IsNullOrEmpty(introduceVideo))
                {
                    item.Path = introduceVideo;
                    _projectMediaService.Update(item);
                }
                else if (item.Type == MediaType.ProcedureVideo && !String.IsNullOrEmpty(procedureVideo))
                {
                    item.Path = procedureVideo;
                    _projectMediaService.Update(item);
                }
            }
            

            var rs = _projectService.Update(request).Result;

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

        public IActionResult Area(int index = 1)
        {
            var model = _areaService.GetAllPaging(index,6).Result;
            ViewBag.index = index;
            
            return View(model);
        }
        public IActionResult UpdateArea(long id)
        {
            UpdateAreaViewModel model = new UpdateAreaViewModel();
            model.projects = _projectService.GetAll().Result;
            model.area = _areaService.GetById(id).Result;
            
            
            return View(model);
        }
        public IActionResult UpdateAreaPost([FromForm] Area area,long id)
        {
            area.id = id;
            var rs = _areaService.Update(area).Result;
            
            return LocalRedirect("~/Admin/Area");
        }
        public IActionResult DeleteArea(long id)
        {
            var rs = _areaService.Delete(id).Result;
            
            return LocalRedirect("~/Admin/Area");
        }
        public IActionResult NewArea()
        { 
            var model = _projectService.GetAll().Result;
            
            return View(model);
        }
        public IActionResult NewAreaPost([FromForm] Area area)
        {
            var rs = _areaService.Create(area).Result;
            
           return LocalRedirect("~/Admin/Area");
        }
        
        public IActionResult RealEstate(int index = 1)
        {
            var realEstate = _realEstateService.GetAllPaging(index,6).Result;
            var model = realEstate.OrderByDescending(x => x.DateModify).ToList();
            ViewBag.index = index;

            return View(model);
        }
        public IActionResult RealEstateDetail(long id)
        {
            RealEstateDetailViewModel model = new RealEstateDetailViewModel();
            model.realEstateModel = _realEstateService.GetById(id).Result;
            model.area = _areaService.GetById(model.realEstateModel.areaID).Result;
            
            
            return View(model);
        }
        public IActionResult UpdateRealEstate(long id)
        {
            UpdateRealEstateViewModel model = new UpdateRealEstateViewModel();
            model.realEstateTypes = _realEstateService.GetAllRealEstateType().Result;
            model.realEstateModel = _realEstateService.GetById(id).Result;
            model.areas = _areaService.GetAll().Result;
            
            return View(model);
        }
        [HttpPost]
        public IActionResult UpdateRealEstatePost([FromForm] RealEstateUpdateRequest request,long id)
        {
            request.id = id;
           var rs = _realEstateService.Update(request).Result;
            return LocalRedirect("~/Admin/RealEstateDetail/" + id);
        }

        public IActionResult NewRealEstate()
        {
            NewRealEstateViewModel model = new NewRealEstateViewModel();
            model.realEstateTypes = _realEstateService.GetAllRealEstateType().Result;
            model.areas = _areaService.GetAll().Result;
            return View(model);
        }
        [HttpPost]
        public IActionResult NewRealEstatePost([FromForm] RealEstateCreateRequest request)
        {
            var rs = _realEstateService.Create(request).Result;
            return LocalRedirect("~/Admin/RealEstate/");
        }
        public IActionResult DeleteRealEstate(long id)
        {
            var rs = _realEstateService.Delete(id).Result;
            return LocalRedirect("~/Admin/RealEstate/");
        }

        public IActionResult News(int index = 1)
        {
            var model = _newsService.GetAllPaging(index,6).Result;
            ViewBag.index = index;
            return View(model);
        }
        
        public IActionResult NewsDetail(long id)
        {
            var model = _newsService.GetById(id).Result;
            
            return View(model);
        }
        public IActionResult UpdateNews(long id)
        {
            var model = _newsService.GetById(id).Result;
            return View(model);
        }
        [HttpPost]
        public IActionResult UpdateNewsPost([FromForm] News request,long id)
        {
            request.id = id;
            var rs = _newsService.Update(request).Result;
            return LocalRedirect("~/Admin/NewsDetail/" + id);
        }

        public IActionResult AddNews()
        {
            return View();
        }

        public IActionResult AddNewsPost([FromForm] NewsCreateRequest request)
        {
            var rs = _newsService.Create(request).Result;
            return LocalRedirect("~/Admin/News");
        }

        public IActionResult DeleteNews(long id)
        {
            var rs = _newsService.Delete(id).Result;
            return LocalRedirect("~/Admin/News");
        }

        public IActionResult Recruitment(int pageIndex = 1)
        {
            var model = _recruitmentService.GetAllPaging(pageIndex,6).Result;

            return View(model);
        }

        public IActionResult RecruitmentDetail(long id)
        {
            var model = _recruitmentService.GetById(id).Result;

            return View(model);
        }

        public IActionResult UpdateRecruitment(long id)
        {
            var model = _recruitmentService.GetById(id).Result;
            return View(model);
        }

        public IActionResult UpdateRecruitmentPost([FromForm] Recruitment request ,long id)
        {
            request.id = id;
            var rs = _recruitmentService.Update(request).Result;
            return LocalRedirect("~/Admin/RecruitmentDetail/" + id);
        }
        public IActionResult NewRecruitment()
        {
            return View();
        }
        public IActionResult NewRecruitmentPost([FromForm] RecruitmentCreateRequest request)
        {
            var rs = _recruitmentService.Create(request).Result;
            return LocalRedirect("~/Admin/Recruitment/");
        }

        public IActionResult DeleteRecruitment(long id)
        {
            var rs = _recruitmentService.Delete(id).Result;
            return LocalRedirect("~/Admin/Recruitment/");

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