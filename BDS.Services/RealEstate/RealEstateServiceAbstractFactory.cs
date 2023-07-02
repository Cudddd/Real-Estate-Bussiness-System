using BDS.Data.EF;
using BDS.Services.Common;
using BDS.Services.RealEstateMedia;
using BDS.Services.WishlistRealEstate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Services.RealEstate
{
    public class RealEstateServiceAbstractFactory : IRealEstateServiceAbstractFactory
    {
        private readonly BdsDbContext _context;
        private readonly IStorageService _storageService;
        private readonly IRealEstateMediaService _realEstateMediaService;
        private readonly IWishlistRealEstateService _wishlistRealEstateService;

        public RealEstateServiceAbstractFactory(BdsDbContext context, IStorageService storageService,
        IRealEstateMediaService realEstateMediaService, IWishlistRealEstateService wishlistRealEstateService)
        {
            _context = context;
            _storageService = storageService;
            _realEstateMediaService = realEstateMediaService;
            _wishlistRealEstateService = wishlistRealEstateService;
        }
        public IRealEstateService CreateRealEstateService()
        {
            return new RealEstateService(_context, _storageService, _realEstateMediaService, _wishlistRealEstateService);
        }
    }
}
