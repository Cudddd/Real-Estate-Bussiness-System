using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDS.Data.EF;
using BDS.Services.Common;
using BDS.Services.Model;
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

        public async Task<int> Delete(long areaId)
        {
            var entity = await _context.Area.FirstOrDefaultAsync(x => x.id == areaId);
            if (entity != null)
            {
                _context.Area.Remove(entity);
                return await _context.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<int> DeleteRange(List<Area> areas)
        {
            _context.Area.RemoveRange(areas);
            return await _context.SaveChangesAsync();
        }

        public Task<Data.Entities.Area> GetById(long areaID)
        {
            throw new System.NotImplementedException();
        }
        
        public Task<PageResult<Data.Entities.Area>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<AreaModel>> GetAllPaging(int pageIndex,int pageSize)
        {
            var data = await _context.Area.ToListAsync();
            List<AreaModel> areaModels = new List<AreaModel>();
            foreach (var item in data)
            {
                if (item != null)
                {
                    AreaModel areaModel = new AreaModel()
                    {
                        id = item.id,
                        name = item.name,
                        projectName = _context.Project.FirstOrDefault(x => x.id == item.projectID)?.name
                    };
                    areaModels.Add(areaModel);
                }

                
            }

            return areaModels;
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