using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDS.Data.EF;
using BDS.Services.Model;
using Microsoft.EntityFrameworkCore;

namespace BDS.Services.Wishlist
{
    public class WishlistService : IWishlistService
    {
        private readonly BdsDbContext _context;
        public WishlistService(BdsDbContext context)
        {
            _context = context;
        }
        public Task<int> Create(Data.Entities.Wishlist wishlist)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> Update(Data.Entities.Wishlist wishlist)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> Delete(long wishlistId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Data.Entities.Wishlist> GetById(long wishlistId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Data.Entities.Wishlist>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public async Task<WishlistModel> GetByUserId(long userId)
        {
            var wishlist = await _context.Wishlist.FirstOrDefaultAsync(x => x.UserId == userId);

            var wishlistEstate = await _context.WishlistRealEstate.Where(x => x.WishlistId == wishlist.id).ToListAsync();

            WishlistModel wishlistModel = new WishlistModel();
            wishlistModel.realEstates = new List<RealEstateModel>();

            foreach (var item in wishlistEstate)
            {
                RealEstateModel realEstateModel = new RealEstateModel();
                
                Data.Entities.RealEstate data = _context.RealEstate.FirstOrDefault(x=>x.id == item.RealEstateId);

                if (data != null)
                {
                    realEstateModel.id = data.id;
                    realEstateModel.areaID = data.areaID;
                    realEstateModel.name = data.name;
                    realEstateModel.acreage = data.acreage;
                    realEstateModel.bathRoom = data.bathRoom;
                    realEstateModel.bedRoom = data.bedRoom;
                    realEstateModel.DateCreated = data.DateCreated;
                    realEstateModel.DateModify = data.DateModify;
                    realEstateModel.facade = data.facade;
                    realEstateModel.floor = data.floor;
                    realEstateModel.length = data.length;
                    realEstateModel.width = data.width;
                    realEstateModel.orientation = data.orientation;
                    realEstateModel.price = data.price;
                    realEstateModel.location = data.location;
                    realEstateModel.mainLine = data.mainLine;
                    realEstateModel.sell = data.sell;
                    realEstateModel.type = _context.RealEstateType.FirstOrDefault(x => x.id == data.typeID)?.name;
                    realEstateModel.realEstateMedia = _context.RealEstateMedia
                        .Where(x => x.RealEstateId == realEstateModel.id).ToList();
                }

                wishlistModel.realEstates.Add(realEstateModel);
            }



            return wishlistModel;
        }
    }
}