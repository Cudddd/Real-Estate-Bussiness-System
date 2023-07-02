using BDS.Data.EF;
using BDS.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Services.ProjectMedia
{
    public class ProjectMediaServiceAbstractFactory : IProjectMediaServiceAbstractFactory
    {
        private readonly BdsDbContext _context;
        private readonly IStorageService _storageService;
        public ProjectMediaServiceAbstractFactory(BdsDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }
        public IProjectMediaService CreateProjectMediaService()
        {
            return new ProjectMediaService(_context, _storageService);
        }
    }
}
