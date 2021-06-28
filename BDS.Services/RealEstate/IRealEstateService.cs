using System.Collections.Generic;
using System.Threading.Tasks;
using BDS.Services.Common;
using BDS.Services.Model;
using BDS.Services.Request;

namespace BDS.Services.RealEstate
{
    using BDS.Data.Entities;
    public interface IRealEstateService
    {
        Task<int> Create(RealEstateCreateRequest request,User user);
        Task<int> Update(RealEstate r);
        Task<int> Delete(long realEstateID);
        Task<RealEstateModel> GetById(long realEstateID);
        Task<PageResult<RealEstate>> GetAll();
        Task<List<RealEstateModel>> GetAllPaging(int pageIndex, int pageSize);

        Task<List<RealEstateModel>> GetByAreaId(long areaID,int pageIndex,int pageSize);
        Task<List<RealEstateType>> GetAllRealEstateType();

    }
}