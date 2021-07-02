using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BDS.Data.EF;
using BDS.Data.Enum;
using BDS.Services.Common;
using BDS.Services.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ContentDispositionHeaderValue = System.Net.Http.Headers.ContentDispositionHeaderValue;

namespace BDS.Services.Project
{
    using Data.Entities;
    
    public class ProjectService : IProjectService
    {
        private readonly BdsDbContext _context;
        private readonly IStorageService _storageService;

        public ProjectService(BdsDbContext context,IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<int> Create(ProjectCreateRequest request)
        {
            Project project = new Project()
            {
                id = Utilities.UtilitiesService.GenerateID(),
                name = request.name,
                invesloper = request.invesloper,
                introduce = request.introduce,
                info = request.info,
                procedure = request.procedure,
                customerBenefits = request.customerBenefits,
                highlight = false,
            };

            _context.Project.Add(project);
            
            
            ProjectMedia banner = new ProjectMedia()
            {
                id = Utilities.UtilitiesService.GenerateID(),
                ProjectId = project.id,
                Type = MediaType.BannerImg,
                Path = await _storageService.SaveFile(request.BannerImg),
            };
            
            ProjectMedia introImg = new ProjectMedia()
            {
                id = Utilities.UtilitiesService.GenerateID(),
                ProjectId = project.id,
                Type = MediaType.IntroduceImg,
                Path = await _storageService.SaveFile(request.IntroImg),
            };
            ProjectMedia map = new ProjectMedia()
            {
                id = Utilities.UtilitiesService.GenerateID(),
                ProjectId = project.id,
                Type = MediaType.MapImg,
                Path = await _storageService.SaveFile(request.MapImg),
            };
            ProjectMedia procedureVid = new ProjectMedia()
            {
                id = Utilities.UtilitiesService.GenerateID(),
                ProjectId = project.id,
                Type = MediaType.ProcedureVideo,
                Path = request.ProcedureVid,
            };
            ProjectMedia introduceVid = new ProjectMedia()
            {
                id = Utilities.UtilitiesService.GenerateID(),
                ProjectId = project.id,
                Type = MediaType.IntroduceVideo,
                Path = request.IntroduceVid,
            };

            await _context.ProjectMedia.AddAsync(banner);
            await _context.ProjectMedia.AddAsync(introImg);
            await _context.ProjectMedia.AddAsync(map);
            await _context.ProjectMedia.AddAsync(procedureVid);
            await _context.ProjectMedia.AddAsync(introduceVid);
            
           
            
            
            
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(Project p)
        {
            Project entity = _context.Project.FirstOrDefault(t => t.id == p.id);

            if (entity != null)
            {
                entity.name = p.name;
                entity.invesloper = p.invesloper;
                entity.info = p.info;
                entity.procedure = p.procedure;
                entity.introduce = p.introduce;
                entity.customerBenefits = p.customerBenefits;

                _context.Project.Update(entity);
                return await _context.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<int> Delete(long projectId)
        {
            Project entity = _context.Project.FirstOrDefault(t => t.id == projectId);
            
            if (entity != null)
            {
                var areas = _context.Area.Where(x => x.projectID == entity.id).ToList();
                
                var media = _context.ProjectMedia.Where(x => x.ProjectId == entity.id).ToList();
                
                _context.ProjectMedia.RemoveRange(media);

                foreach (var area in areas)
                {
                    if(area != null)
                    {
                        List<RealEstate> realEstates = new List<RealEstate>();
                        realEstates = _context.RealEstate.Where(x => x.areaID == area.id).ToList();
                        
                        _context.RealEstate.RemoveRange(realEstates);
                    }
                }

                _context.Area.RemoveRange(areas);
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

        public async Task<List<Project>> GetAll()
        {
            var  result = await _context.Project.ToListAsync();

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

        public async Task<List<Project>> FilterOtherInvesloper()
        {
            var entities = await _context.Project.ToListAsync();
            
            List<Project> result = new List<Project>();
            foreach (var item in entities)
            {
                if(item.invesloper != "Vinhomes")
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

        public async Task<int> SetHighlightProject(long id, bool highlight)
        {
            await using (_context)
            {
                var entity = await _context.Project.FirstOrDefaultAsync(x=>x.id == id);
                if (entity != null)
                {
                    entity.highlight = highlight;

                    _context.Project.Update(entity);
                
                    this._context.Entry(entity).State=EntityState.Modified;
                    return await _context.SaveChangesAsync();
                }
            }

            return 0;
        }
    }
}