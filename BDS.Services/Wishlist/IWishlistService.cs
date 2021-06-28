using System.Collections.Generic;
using System.Threading.Tasks;
using BDS.Services.Model;

namespace BDS.Services.Wishlist
{
    public interface IWishlistService
    {
        Task<int> Create(Data.Entities.Wishlist wishlist);
        Task<int> Update(Data.Entities.Wishlist wishlist);
        Task<int> Delete(long wishlistId);
        Task<Data.Entities.Wishlist> GetById(long wishlistId);
        Task<List<Data.Entities.Wishlist>> GetAll();
        Task<WishlistModel> GetByUserId(long userId);
    }
}