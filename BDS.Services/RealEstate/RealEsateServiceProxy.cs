using BDS.Data.Entities;
using BDS.Services.Common;
using BDS.Services.Model;
using BDS.Services.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Services.RealEstate
{
    public class RealEstateServiceProxy : IRealEstateService
    {
        private readonly IRealEstateService _realEstateService;
        private Dictionary<int, List<Data.Entities.RealEstate>> _cachedRealEstates;

        public RealEstateServiceProxy(IRealEstateService realEstateService)
        {
            _realEstateService = realEstateService;
            _cachedRealEstates = new Dictionary<int, List<Data.Entities.RealEstate>>();
        }

        public async Task<int> Create(RealEstateCreateRequest request)
        {
            return await _realEstateService.Create(request);
        }

        public async Task<int> Delete(long realEstateID)
        {
            return await _realEstateService.Delete(realEstateID);
        }

        public async Task<int> DeleteRange(List<Data.Entities.RealEstate> realEstates)
        {
            return await _realEstateService.DeleteRange(realEstates);
        }

        public async Task<PageResult<Data.Entities.RealEstate>> GetAll()
        {
            return await _realEstateService.GetAll();
        }

        public async Task<List<RealEstateType>> GetAllRealEstateType()
        {
            return await _realEstateService.GetAllRealEstateType();
        }

        public async Task<List<RealEstateModel>> GetByAreaId(long areaID, int pageIndex, int pageSize)
        {
            return await _realEstateService.GetByAreaId(areaID, pageIndex, pageSize);
        }

        public async Task<RealEstateModel> GetById(long realEstateID)
        {
            return await _realEstateService.GetById(realEstateID);
        }

        public async Task<int> Update(RealEstateUpdateRequest request)
        {
            return await _realEstateService.Update(request);
        }

        public async Task<List<RealEstateModel>> GetAllPaging(int pageIndex, int pageSize)
        {
            return await _realEstateService.GetAllPaging(pageIndex, pageSize);
        }
    }
}
