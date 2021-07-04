using System.Collections.Generic;
using System.Threading.Tasks;

namespace BDS.Services.WishlistRealEstate
{
    public interface IWishlistRealEstateService
    {
        Task<int> DeleteRange(List<Data.Entities.WishlistRealEstate> list);
    }
}