using System.Collections.Generic;
using System.Threading.Tasks;
using BDS.Services.Common;
using BDS.Services.Model;

namespace BDS.Services.Area
{
    using Data.Entities;
    public interface IAreaService
    {
        Task<int> Create(Area a);
        Task<int> Update(Area a);
        Task<int> Delete(long areaID);
        Task<int> DeleteRange(List<Area> areas);
        Task<Area> GetById(long areaID);
        Task<List<Area>> GetAll();
        Task<List<AreaModel>> GetAllPaging(int pageIndex,int pageSize);

        Task<List<Area>> GetByProjectId(long projectId);
        
    }
}