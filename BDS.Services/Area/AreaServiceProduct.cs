using BDS.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Services.Area
{
    public class AreaServiceProduct : IAreaService
    {
        public Task<int> Create(Data.Entities.Area a)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(long areaID)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteRange(List<Data.Entities.Area> areas)
        {
            throw new NotImplementedException();
        }

        public Task<List<Data.Entities.Area>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<AreaModel>> GetAllPaging(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<Data.Entities.Area> GetById(long areaID)
        {
            throw new NotImplementedException();
        }

        public Task<List<Data.Entities.Area>> GetByProjectId(long projectId)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(Data.Entities.Area a)
        {
            throw new NotImplementedException();
        }
    }
}
