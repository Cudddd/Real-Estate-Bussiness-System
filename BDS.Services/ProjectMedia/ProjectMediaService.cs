using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDS.Data.EF;
using BDS.Data.Enum;
using Microsoft.EntityFrameworkCore;

namespace BDS.Services.ProjectMedia
{
    using Data.Entities;
    public class ProjectMediaService : IProjectMediaService
    {
        private readonly BdsDbContext _context;

        public ProjectMediaService(BdsDbContext context)
        {
            _context = context;
        }
        public async Task<int> Create(Data.Entities.ProjectMedia projectMedia)
        {
            projectMedia.id = Utilities.UtilitiesService.GenerateID();

            await _context.ProjectMedia.AddAsync(projectMedia);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> CreateRange(List<ProjectMedia> projectMedia)
        {
            foreach (var item in projectMedia)
            {
                item.id = Utilities.UtilitiesService.GenerateID();
            }

            await _context.ProjectMedia.AddRangeAsync(projectMedia);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(Data.Entities.ProjectMedia p)
        {
            _context.ProjectMedia.Update(p);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(long projectMediaId)
        {
            var entity = await _context.ProjectMedia.FirstOrDefaultAsync(x=>x.id == projectMediaId);

            if (entity != null)
            {
                _context.ProjectMedia.Remove(entity);
                return await _context.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<int> DeleteRange(List<Data.Entities.ProjectMedia> projectMedia)
        {
            _context.ProjectMedia.RemoveRange(projectMedia);
            return await _context.SaveChangesAsync();
        }

        public Task<Data.Entities.ProjectMedia> GetById(long projectId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<ProjectMedia>> GetByProjectId(long projectId)
        {
            var entity = await _context.ProjectMedia.Where(x=>x.ProjectId == projectId).ToListAsync();
            return entity;
        }

        public Task<List<Data.Entities.ProjectMedia>> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}