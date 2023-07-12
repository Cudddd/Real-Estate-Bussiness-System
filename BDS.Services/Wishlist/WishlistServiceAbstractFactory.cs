using BDS.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Services.Wishlist
{
    public class WishlistServiceAbstractFactory : IWishlistServiceAbtractFactory
    {
        private readonly BdsDbContext _context;
        public WishlistServiceAbstractFactory(BdsDbContext context)
        {
            _context = context;
        }
        public IWishlistService CreateWishlistService()
        {
            return new WishlistService(_context);
        }
    }
}
