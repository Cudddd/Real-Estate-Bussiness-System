using BDS.Data.EF;
using BDS.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Services.RealEstateMedia
{
    public class RealEstateMediaServiceAbstractFactory : IRealEstateMediaAbstractFactory
    {
        private readonly BdsDbContext _context;
        private readonly IStorageService _storageService;

        public RealEstateMediaServiceAbstractFactory(BdsDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }
        public IRealEstateMediaService CreateRealEstateMediaService()
        {
            return new RealEstateMediaService(_context, _storageService);
        }
    }
}
