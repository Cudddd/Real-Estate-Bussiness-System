using System.Collections.Generic;
using System.Threading.Tasks;
using BDS.Services.Common;
using Microsoft.AspNetCore.Http;

namespace BDS.Services.Project
{
    using Data.Entities;
    
    public interface IProjectService
    {
        Task<int> Create(Project p);
        Task<int> Update(Project p);
        Task<int> Delete(long projectId);
        Task<List<Project>> GetHighlightProject();
        Task<Project> GetById(long projectId);
        Task<PageResult<Project>> GetAll();
        Task<PageResult<Project>> GetAllPaging(string keyword, Page page);

        Task<List<Project>> FilterByInvesloper(string invesloper);

        Task<List<ProjectMedia>> GetProjectMedia(long projectId);

        Task<List<ProjectBanner>> GetProjectBanner();

    }
}