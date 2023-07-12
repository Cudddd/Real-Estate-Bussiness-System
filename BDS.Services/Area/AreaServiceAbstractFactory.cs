using BDS.Data.EF;
using BDS.Services.RealEstate;
using BDS.Services.RealEstateMedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Services.Area
{
    public class AreaServiceAbstractFactory : IAreaServiceAbstractFactory
    {
        private readonly BdsDbContext _context;
        private readonly IRealEstateService _realEstateService;
        private readonly IRealEstateMediaService _realEstateMediaService;

        public AreaServiceAbstractFactory(BdsDbContext context, IRealEstateService realEstateService,
            IRealEstateMediaService realEstateMediaService)
        {
            _context = context;
            _realEstateService = realEstateService;
            _realEstateMediaService = realEstateMediaService;
        }
        public IAreaService CreateAreaService()
        {
            return new AreaService(_context,_realEstateService, _realEstateMediaService);
        }
    }
}
