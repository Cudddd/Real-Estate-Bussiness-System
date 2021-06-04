using System.Collections.Generic;
using System.Threading.Tasks;
using BDS.Services.Common;

namespace BDS.Services.RealEstate
{
    using BDS.Data.Entities;
    public interface IRealEstateService
    {
        Task<int> Create(RealEstate r);
        Task<int> Update(RealEstate r);
        Task<int> Delete(long realEstateID);
        Task<RealEstate> GetById(long realEstateID);
        Task<PageResult<RealEstate>> GetAll();
        Task<PageResult<RealEstate>> GetAllPaging(string keyword, Page page);

        Task<List<RealEstate>> GetByAreaId(long areaID);

    }
}