using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDS.Data.EF;
using BDS.Services.Common;
using Microsoft.EntityFrameworkCore;

namespace BDS.Services.Project
{
    using Data.Entities;
    
    public class ProjectService : IProjectService
    {
        private readonly BdsDbContext _context;
        
        public ProjectService(BdsDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Project p)
        {
            _context.Project.Add(p);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(Project p)
        {
            Project entity = _context.Project.FirstOrDefault(t => t.id == p.id);
            
            if (entity != null)
            {
                _context.Project.Update(p);
                return await _context.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<int> Delete(long projectId)
        {
            Project entity = _context.Project.FirstOrDefault(t => t.id == projectId);
            
            if (entity != null)
            {
                _context.Project.Remove(entity);
                return await _context.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<List<Project>> GetHighlightProject()
        {
            List<Project> result = new List<Project>();
            var list = await _context.Project.ToListAsync();

            foreach (var item in list)
            {
                if(item.highlight)
                    result.Add(item);
            }

            return result;

        }

        public async Task<Project> GetById(long projectId)
        {
            Project entity = await _context.Project.FirstOrDefaultAsync(t => t.id == projectId);
            return entity;
        }

        public async Task<PageResult<Project>> GetAll()
        {
            PageResult<Project> result = new PageResult<Project>();
            result.Items = await _context.Project.ToListAsync();
            result.totalRecord = result.Items.Count;
            return result;
        }

        public Task<PageResult<Project>> GetAllPaging(string keyword, Page page)
        {
            throw new System.NotImplementedException();
        }
    }
}