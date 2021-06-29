using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BDS.Data.EF;
using BDS.Data.Enum;
using BDS.Services.Area;
using BDS.Services.Common;
using BDS.Services.Model;
using BDS.Services.Request;
using BDS.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace BDS.Services.RealEstate
{
    using Data.Entities;
    public class RealEstateService : IRealEstateService
    {
        private readonly BdsDbContext _context;
        private readonly IStorageService _storageService;

        public RealEstateService(BdsDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }
        
        public async Task<int> Create(RealEstateCreateRequest request,User user)
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
            if(request.sell.Contains("ban"))
                userRealEstate.sell = true;
            else userRealEstate.sell = false;
            userRealEstate.width = request.width;
            userRealEstate.bathRoom = request.bathRoom;
            userRealEstate.bedRoom = request.bedRoom;
            userRealEstate.mainLine = request.mainLine;
            userRealEstate.typeId = request.typeId;
            userRealEstate.DateCreated = DateTime.Now;
            userRealEstate.DateModify =DateTime.Now;
            userRealEstate.UserId = user.Id;
            userRealEstate.address = request.address;

            
            foreach (var item in request.realEstateImgs)
            {
                var productImage = new UserRealEstateMedia()
                {
                    UserRealEstateId = userRealEstate.id,
                    id = 123,
                    Type = MediaType.NormalImg,
                };
                
                if (item != null)
                {
                    productImage.Path = await _storageService.SaveFile(item);
                    await _context.UserRealEstateMedia.AddAsync(productImage);
                }
            }
            
            await _context.UserRealEstate.AddAsync(userRealEstate);
            return await _context.SaveChangesAsync();

        }

        public Task<int> Update(Data.Entities.RealEstate r)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> Delete(long realEstateID)
        {
            throw new System.NotImplementedException();
        }

        public async Task<RealEstateModel> GetById(long realEstateID)
        {
            var entity = await _context.RealEstate.Join(
                _context.RealEstateType,
                realEstate => realEstate.typeID,
                realEstateType => realEstateType.id,
                (realEstate,realEstateType) => new RealEstateModel()
                {
                    id = realEstate.id,
                    areaID = realEstate.areaID,
                    name = realEstate.name,
                    type = realEstateType.name,
                    acreage = realEstate.acreage,
                    bathRoom = realEstate.bathRoom,
                    bedRoom = realEstate.bedRoom,
                    DateCreated = realEstate.DateCreated,
                    DateModify = realEstate.DateModify,
                    facade = realEstate.facade,
                    floor = realEstate.floor,
                    length = realEstate.length,
                    width = realEstate.width,
                    orientation = realEstate.orientation,
                    price = realEstate.price,
                    location = realEstate.location,
                    mainLine = realEstate.mainLine,
                    sell = realEstate.sell,
                    description = realEstate.description,
                    areaName = _context.Area.FirstOrDefault(x=>x.id == realEstate.areaID).name,
                    realEstateMedia = _context.RealEstateMedia
                        .Where(x => x.RealEstateId == realEstate.id).ToList()
                }
            ).FirstOrDefaultAsync(t => t.id == realEstateID);
            
            
            return entity;
        }

        public Task<PageResult<Data.Entities.RealEstate>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<RealEstateModel>> GetAllPaging(int pageIndex, int pageSize)
        {
            var data = await _context.RealEstate.OrderBy(r => r.DateModify).ToListAsync();
            
            List<RealEstateModel> realEstates = new List<RealEstateModel>();
            foreach (var item in data)
            {
                
                //if (item.areaID == areaID)
                {
                    
                    RealEstateModel realEstateModel = new RealEstateModel();
                    realEstateModel.id = item.id;
                    realEstateModel.areaID = item.areaID;
                    realEstateModel.name = item.name;
                    realEstateModel.acreage = item.acreage;
                    realEstateModel.bathRoom = item.bathRoom;
                    realEstateModel.bedRoom = item.bedRoom;
                    realEstateModel.DateCreated = item.DateCreated;
                    realEstateModel.DateModify = item.DateModify;
                    realEstateModel.facade = item.facade;
                    realEstateModel.floor = item.floor;
                    realEstateModel.length = item.length;
                    realEstateModel.width = item.width;
                    realEstateModel.orientation = item.orientation;
                    realEstateModel.price = item.price;
                    realEstateModel.location = item.location;
                    realEstateModel.mainLine = item.mainLine;
                    realEstateModel.sell= item.sell;
                    realEstateModel.type = _context.RealEstateType.FirstOrDefault(x => x.id == item.typeID)?.name;
                    realEstateModel.realEstateMedia = _context.RealEstateMedia
                        .Where(x => x.RealEstateId == realEstateModel.id).ToList();
                    
                    realEstates.Add(realEstateModel);
                }
                    
            }

            realEstates = realEstates.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return realEstates;
        }

        public async Task<List<RealEstateModel>> GetByAreaId(long areaID,int pageIndex,int pageSize)
        {
            var data = await _context.RealEstate.ToListAsync();
            var realEstateTypes = await _context.RealEstateType.ToListAsync();

            List<RealEstateModel> realEstates = new List<RealEstateModel>();
            foreach (var item in data)
            {
                
                if (item.areaID == areaID)
                {
                    
                    RealEstateModel realEstateModel = new RealEstateModel();
                    realEstateModel.id = item.id;
                    realEstateModel.areaID = item.areaID;
                    
                    foreach (var realEstateType in realEstateTypes)
                    {
                        if (realEstateType.id == item.typeID)
                        {
                            realEstateModel.type = realEstateType.name;
                            break;
                        }
                    }
                    
                    realEstateModel.name = item.name;
                    realEstateModel.acreage = item.acreage;
                    realEstateModel.bathRoom = item.bathRoom;
                    realEstateModel.bedRoom = item.bedRoom;
                    realEstateModel.DateCreated = item.DateCreated;
                    realEstateModel.DateModify = item.DateModify;
                    realEstateModel.facade = item.facade;
                    realEstateModel.floor = item.floor;
                    realEstateModel.length = item.length;
                    realEstateModel.width = item.width;
                    realEstateModel.orientation = item.orientation;
                    realEstateModel.price = item.price;
                    realEstateModel.location = item.location;
                    realEstateModel.mainLine = item.mainLine;
                    realEstateModel.sell= item.sell;
                    realEstateModel.description = item.description;
                    realEstateModel.realEstateMedia = _context.RealEstateMedia
                        .Where(x => x.RealEstateId == realEstateModel.id).ToList();
                    
                    realEstates.Add(realEstateModel);
                }
                    
            }
            
            realEstates = realEstates.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return realEstates;
        }

        public async Task<List<RealEstateType>> GetAllRealEstateType()
        {
            var data = await _context.RealEstateType.ToListAsync();

            return data;
            
            throw new NotImplementedException();
        }
    }
}