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
using BDS.Services.RealEstateMedia;
using BDS.Services.Request;
using BDS.Services.User;
using BDS.Services.Wishlist;
using BDS.Services.WishlistRealEstate;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;

namespace BDS.Services.RealEstate
{
    using Data.Entities;
    using System.Xml;

    public class RealEstateService : IRealEstateService
    {
        private readonly BdsDbContext _context;
        private readonly IStorageService _storageService;
        private readonly IRealEstateMediaService _realEstateMediaService;
        private readonly IWishlistRealEstateService _wishlistRealEstateService;

        public RealEstateService(BdsDbContext context, IStorageService storageService,
        IRealEstateMediaService realEstateMediaService, IWishlistRealEstateService wishlistRealEstateService)
        {
            _context = context;
            _storageService = storageService;
            _realEstateMediaService = realEstateMediaService;
            _wishlistRealEstateService = wishlistRealEstateService;
        }

        public async Task<int> Create(RealEstateCreateRequest request)
        {
            RealEstate realEstate = new RealEstate();
            realEstate.id = Utilities.UtilitiesService.GenerateID();
            realEstate.name = request.name;
            realEstate.acreage = request.acreage;
            realEstate.description = request.description;
            realEstate.facade = request.facade;
            realEstate.floor = request.floor;
            realEstate.length = request.length;
            realEstate.location = request.location;
            realEstate.orientation = request.orientation;
            realEstate.price = request.price;
            realEstate.sell = request.sell;
            realEstate.width = request.width;
            realEstate.bathRoom = request.bathRoom;
            realEstate.bedRoom = request.bedRoom;
            realEstate.mainLine = request.mainLine;
            realEstate.typeID = request.typeId;
            realEstate.DateCreated = DateTime.Now;
            realEstate.DateModify = DateTime.Now;
            realEstate.address = request.address;
            realEstate.areaID = request.areaId;

            await _context.RealEstate.AddAsync(realEstate);
            //await _context.SaveChangesAsync();


            foreach (var item in request.realEstateImgs)
            {
                if (item != null)
                {
                    var productImage = new RealEstateMedia()
                    {
                        RealEstateId = realEstate.id,
                        id = Utilities.UtilitiesService.GenerateID(),
                        Type = MediaType.NormalImg,
                        Path = await _storageService.SaveFile(item),
                    };

                    await _realEstateMediaService.Create(productImage);
                }
            }

            if (request.thumbnail != null)
            {
                var productImage = new RealEstateMedia()
                {
                    RealEstateId = realEstate.id,
                    id = Utilities.UtilitiesService.GenerateID(),
                    Type = MediaType.ThumnailImg,
                    Path = await _storageService.SaveFile(request.thumbnail),
                };

                await _realEstateMediaService.Create(productImage);
            }



            return await _context.SaveChangesAsync();

        }

        public async Task<int> Update(RealEstateUpdateRequest request)
        {
            var entity = await _context.RealEstate.FirstOrDefaultAsync(x => x.id == request.id);
            if (entity != null)
            {
                entity.name = request.name;
                entity.acreage = request.acreage;
                entity.bathRoom = request.bathRoom;
                entity.bedRoom = request.bedRoom;
                entity.DateModify = DateTime.Now;
                entity.facade = request.facade;
                entity.floor = request.floor;
                entity.length = request.length;
                entity.width = request.width;
                entity.orientation = request.orientation;
                entity.price = request.price;
                entity.location = request.location;
                entity.mainLine = request.mainLine;
                entity.sell = request.sell;
                entity.typeID = request.typeId;
                entity.address = request.address;
                entity.description = request.description;

                _context.RealEstate.Update(entity);
                return await _context.SaveChangesAsync();
            }


            return 0;
        }

        public async Task<int> Delete(long realEstateID)
        {
            var entity = await _context.RealEstate.FirstOrDefaultAsync(x => x.id == realEstateID);
            if (entity != null)
            {
                var media = await _context.RealEstateMedia.Where(x => x.RealEstateId == realEstateID).ToListAsync();

                await _realEstateMediaService.DeleteRange(media);

                var wishlist = await _context.WishlistRealEstate.Where(x => x.RealEstateId == entity.id).ToListAsync();

                await _wishlistRealEstateService.DeleteRange(wishlist);

                _context.RealEstate.Remove(entity);

                return await _context.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<int> DeleteRange(List<RealEstate> realEstates)
        {
            _context.RealEstate.RemoveRange(realEstates);
            return await _context.SaveChangesAsync();
        }

        public async Task<RealEstateModel> GetById(long realEstateID)
        {
            RealEstateModelBuilder modelBuilder = new ExtendedRealEstateModelBuilder(_context);
            var entity = await _context.RealEstate.Join(
                _context.RealEstateType,
                realEstate => realEstate.typeID,
                realEstateType => realEstateType.id,
                (realEstate, realEstateType) => modelBuilder.Build(realEstate)
            ).FirstOrDefaultAsync(t => t.id == realEstateID);


            return entity;
        }

        public Task<PageResult<Data.Entities.RealEstate>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        private RealEstateModelBuilder GetRealEstateModelBuilder(int pageIndex)
        {
            if (pageIndex % 2 == 0)
            {
                return new ExtendedRealEstateModelBuilder(_context);
            }
            else
            {
                return new BasicRealEstateModelBuilder(_context);
            }
        }

        public async Task<List<RealEstateModel>> GetAllPaging(int pageIndex, int pageSize)
        {
            var data = await _context.RealEstate.OrderBy(r => r.DateModify).ToListAsync();

            List<RealEstateModel> realEstates = new List<RealEstateModel>();
            RealEstateModelBuilder modelBuilder = GetRealEstateModelBuilder(pageIndex);

            foreach (var item in data)
            {
                RealEstateModel realEstateModel = modelBuilder.Build(item);
                realEstates.Add(realEstateModel);
            }

            realEstates = realEstates.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return realEstates;
        }

        public async Task<List<RealEstateModel>> GetByAreaId(long areaID, int pageIndex, int pageSize)
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
                    realEstateModel.sell = item.sell;
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