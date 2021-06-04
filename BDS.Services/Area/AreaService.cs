using System.Collections.Generic;
using System.Threading.Tasks;
using BDS.Data.EF;
using BDS.Services.Common;
using Microsoft.EntityFrameworkCore;

namespace BDS.Services.Area
{
    using Data.Entities;
    public class AreaService : IAreaService
    {
        private readonly BdsDbContext _context;

        public AreaService(BdsDbContext context)
        {
            _context = context;
        }
        public Task<int> Create(Data.Entities.Area a)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> Update(Data.Entities.Area a)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> Delete(long areaID)
        {
            throw new System.NotImplementedException();
        }

        public Task<Data.Entities.Area> GetById(long areaID)
        {
            throw new System.NotImplementedException();
        }
        
        public Task<PageResult<Data.Entities.Area>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<PageResult<Data.Entities.Area>> GetAllPaging(string keyword, Page page)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Area>> GetByProjectId(long projectId)
        {
            var data = await _context.Area.ToListAsync();
            List<Area> areas = new List<Area>();

            foreach (var item in data)
            {
                if (item.projectID == projectId)
                    areas.Add(item);
                    
            }

            return areas;
        }
    }
}