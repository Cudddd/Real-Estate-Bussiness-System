using System.Collections.Generic;
using System.Threading.Tasks;
using BDS.Data.EF;
using BDS.Services.Area;
using BDS.Services.Common;
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

        public Task<Data.Entities.RealEstate> GetById(long realEstateID)
        {
            throw new System.NotImplementedException();
        }

        public Task<PageResult<Data.Entities.RealEstate>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<PageResult<Data.Entities.RealEstate>> GetAllPaging(string keyword, Page page)
        {
            throw new System.NotImplementedException();
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