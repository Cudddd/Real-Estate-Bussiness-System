using BDS.Data.EF;
using BDS.Services.Area;
using BDS.Services.Common;
using BDS.Services.ProjectMedia;
using BDS.Services.RealEstate;
using BDS.Services.RealEstateMedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Services.Project
{
    public class ProjectAbstractFactory : IProjectAbstractFactory
    {
        private readonly BdsDbContext _context;
        private readonly IStorageService _storageService;
        private readonly IProjectMediaService _projectMediaService;
        private readonly IAreaService _areaService;
        private readonly IRealEstateMediaService _realEstateMediaService;
        private readonly IRealEstateService _realEstateService;

        public ProjectAbstractFactory(BdsDbContext context,
        IStorageService storageService,
        IProjectMediaService projectMediaService,
        IAreaService areaService,
        IRealEstateMediaService realEstateMediaService,
        IRealEstateService realEstateService) {

            _context = context;
            _storageService = storageService;
            _projectMediaService = projectMediaService;
            _areaService = areaService;
            _realEstateMediaService = realEstateMediaService;
            _realEstateService = realEstateService;
        }

        
        public IProjectService CreateProjectServices()
        {
            return  new ProjectService(
                _context,
            _storageService,
            _projectMediaService,
            _areaService,
            _realEstateMediaService,
            _realEstateService
            );
        }
    }
}
