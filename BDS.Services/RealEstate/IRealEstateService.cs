using System.Collections.Generic;
using System.Threading.Tasks;
using BDS.Services.Common;
using BDS.Services.Model;

namespace BDS.Services.RealEstate
{
    using BDS.Data.Entities;
    public interface IRealEstateService
    {
        Task<int> Create(RealEstate r);
        Task<int> Update(RealEstate r);
        Task<int> Delete(long realEstateID);
        Task<RealEstateModel> GetById(long realEstateID);
        Task<PageResult<RealEstate>> GetAll();
        Task<List<RealEstate>> GetAllPaging(int pageIndex, int pageSize);

        Task<List<RealEstate>> GetByAreaId(long areaID);

    }
}