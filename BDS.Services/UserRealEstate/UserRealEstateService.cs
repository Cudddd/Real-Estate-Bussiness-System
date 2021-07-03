using System;
using System.Threading.Tasks;
using BDS.Data.EF;
using BDS.Data.Entities;
using BDS.Services.Request;

namespace BDS.Services.UserRealEstate
{
    using Data.Entities;
    public class UserRealEstateService : IUserRealEstateService
    {
        private readonly BdsDbContext _context;

        public UserRealEstateService(BdsDbContext context)
        {
            _context = context;
        }
        public async Task<int> Create(RealEstateCreateRequest request)
        {
            // UserRealEstate userRealEstate = new UserRealEstate();
            // userRealEstate.id = Utilities.UtilitiesService.GenerateID();
            // userRealEstate.name = request.name;
            // userRealEstate.acreage = request.acreage;
            // userRealEstate.description = request.description;
            // userRealEstate.facade = request.facade;
            // userRealEstate.floor = request.floor;
            // userRealEstate.length = request.length;
            // userRealEstate.location = request.location;
            // userRealEstate.orientation = request.orientation;
            // userRealEstate.price = request.price;
            // if(request.sell.Contains("ban"))
            //     userRealEstate.sell = true;
            // else userRealEstate.sell = false;
            // userRealEstate.width = request.width;
            // userRealEstate.bathRoom = request.bathRoom;
            // userRealEstate.bedRoom = request.bedRoom;
            // userRealEstate.mainLine = request.mainLine;
            // userRealEstate.typeId = request.typeId;
            // userRealEstate.DateCreated = DateTime.Now;
            // userRealEstate.DateModify =DateTime.Now;
            // userRealEstate.UserId = user.Id;
            // userRealEstate.address = request.address;
            //
            //
            // foreach (var item in request.realEstateImgs)
            // {
            //     var productImage = new UserRealEstateMedia()
            //     {
            //         UserRealEstateId = userRealEstate.id,
            //         id = 123,
            //         Type = MediaType.NormalImg,
            //     };
            //     
            //     if (item != null)
            //     {
            //         productImage.Path = await _storageService.SaveFile(item);
            //         await _context.UserRealEstateMedia.AddAsync(productImage);
            //     }
            // }
            //
            // await _context.UserRealEstate.AddAsync(userRealEstate);
            return await _context.SaveChangesAsync();

            
        }
    }
}