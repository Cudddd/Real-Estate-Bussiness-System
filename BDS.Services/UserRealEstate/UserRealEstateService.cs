using System;
using System.Threading.Tasks;
using BDS.Data.EF;
using BDS.Data.Entities;
using BDS.Data.Enum;
using BDS.Services.Common;
using BDS.Services.Request;

namespace BDS.Services.UserRealEstate
{
    using Data.Entities;
    public class UserRealEstateService : IUserRealEstateService
    {
        private readonly BdsDbContext _context;
        private readonly IStorageService _storageService;

        public UserRealEstateService(BdsDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }
        public async Task<int> Create(RealEstateCreateRequest request, long userId)
        {
            UserRealEstate userRealEstate = new UserRealEstate();
            userRealEstate.id = Utilities.UtilitiesService.GenerateID();
            userRealEstate.name = request.name;
            userRealEstate.acreage = request.acreage;
            userRealEstate.description = request.description;
            userRealEstate.facade = request.facade;
            userRealEstate.floor = request.floor;
            userRealEstate.length = request.length;
            userRealEstate.location = request.location;
            userRealEstate.orientation = request.orientation;
            userRealEstate.price = request.price;
            userRealEstate.sell = request.sell;
            userRealEstate.width = request.width;
            userRealEstate.bathRoom = request.bathRoom;
            userRealEstate.bedRoom = request.bedRoom;
            userRealEstate.mainLine = request.mainLine;
            userRealEstate.typeId = request.typeId;
            userRealEstate.DateCreated = DateTime.Now;
            userRealEstate.DateModify =DateTime.Now;
            userRealEstate.UserId = userId;
            userRealEstate.address = request.address;
            
            await _context.UserRealEstate.AddAsync(userRealEstate);
            
            foreach (var item in request.realEstateImgs)
            {
                if (item != null)
                {
                    var productImage = new UserRealEstateMedia()
                    {
                        UserRealEstateId = userRealEstate.id,
                        id = Utilities.UtilitiesService.GenerateID(),
                        Type = MediaType.NormalImg,
                    };
                    productImage.Path = await _storageService.SaveFile(item);
                    await _context.UserRealEstateMedia.AddAsync(productImage);
                }
            }
            
            
            return await _context.SaveChangesAsync();
            
        }
    }
}