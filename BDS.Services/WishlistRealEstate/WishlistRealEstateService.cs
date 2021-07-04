using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BDS.Data.EF;

namespace BDS.Services.WishlistRealEstate
{
    public class WishlistRealEstateService : IWishlistRealEstateService
    {
        private readonly BdsDbContext _context;

        public WishlistRealEstateService(BdsDbContext context)
        {
            _context = context;
        }

        public async Task<int> DeleteRange(List<Data.Entities.WishlistRealEstate> list)
        {
            _context.WishlistRealEstate.RemoveRange(list);
            return await _context.SaveChangesAsync();
        }
    }
}