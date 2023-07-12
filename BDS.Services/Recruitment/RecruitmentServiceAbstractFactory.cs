using BDS.Data.EF;
using BDS.Services.Common;
using BDS.Services.RecruitmentMedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Services.Recruitment
{
    public class RecruitmentServiceAbstractFactory : IReacruitmentServiceAbstractFactory
    {
        private readonly BdsDbContext _context;
        private readonly IStorageService _storageService;
        private readonly IRecruitmentMediaService _recruitmentMediaService;
        public RecruitmentServiceAbstractFactory(BdsDbContext context, IStorageService storageService,
            IRecruitmentMediaService recruitmentMediaService)
        {
            _context = context;
            _storageService = storageService;
            _recruitmentMediaService = recruitmentMediaService;
        }
        public IRecruitmentService CreateRecruitmentService()
        {
            return new RecruitmentService(_context, _storageService, _recruitmentMediaService);
        }
    }
}
