using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BDS.Data.EF;
using BDS.Services.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ContentDispositionHeaderValue = System.Net.Http.Headers.ContentDispositionHeaderValue;

namespace BDS.Services.Project
{
    using Data.Entities;
    
    public class ProjectService : IProjectService
    {
        private readonly BdsDbContext _context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        
        public ProjectService(BdsDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
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

        public async Task<List<Project>> FilterByInvesloper(string invesloper)
        {
            var entities = await _context.Project.ToListAsync();
            
            List<Project> result = new List<Project>();
            foreach (var item in entities)
            {
                if(item.invesloper == invesloper)
                    result.Add(item);
            }

            return result;
        }

        public async Task<List<ProjectMedia>> GetProjectMedia(long projectId)
        {
            var data = await _context.ProjectMedia.Where(x => x.ProjectId == projectId).ToListAsync();

            return data;
        }

        public async Task<List<ProjectBanner>> GetProjectBanner()
        {
            var data = await _context.ProjectBanner.ToListAsync();

            return data;
        }
        
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }
    }
}