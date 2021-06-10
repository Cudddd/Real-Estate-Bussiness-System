using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDS.Data.EF;
using BDS.Services.Area;
using BDS.Services.Common;
using BDS.Services.Model;
using Microsoft.EntityFrameworkCore;

namespace BDS.Services.RealEstate
{
    using Data.Entities;
    public class RealEstateService : IRealEstateService
    {
        private readonly BdsDbContext _context;

        public RealEstateService(BdsDbContext context)
        {
            _context = context;
        }
        public Task<int> Create(Data.Entities.RealEstate r)
        {
            throw new System.NotImplementedException();
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
                    sell = realEstate.sell
                }
            ).FirstOrDefaultAsync(t => t.id == realEstateID);
            
            return entity;
        }

        public Task<PageResult<Data.Entities.RealEstate>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<RealEstate>> GetAllPaging(int pageIndex, int pageSize)
        {
            var data = await _context.RealEstate.OrderBy(r => r.DateModify).ToListAsync();

            var realEstates = data.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return realEstates;
        }

        public async Task<List<Data.Entities.RealEstate>> GetByAreaId(long areaID)
        {
            var data = await _context.RealEstate.ToListAsync();

            List<RealEstate> realEstates = new List<RealEstate>();
            foreach (var item in data)
            {
                if(item.areaID == areaID)
                    realEstates.Add(item);
            }

            return realEstates;
        }
    }
}