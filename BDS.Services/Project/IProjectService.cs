using System.Collections.Generic;
using System.Threading.Tasks;
using BDS.Services.Common;
using BDS.Services.Request;
using Microsoft.AspNetCore.Http;

namespace BDS.Services.Project
{
    using Data.Entities;
    
    public interface IProjectService
    {
        Task<int> Create(ProjectCreateRequest request);
        Task<int> Update(Project p);
        Task<int> Delete(long projectId);
        Task<List<Project>> GetHighlightProject();
        Task<Project> GetById(long projectId);
        Task<List<Project>> GetAll();
        Task<PageResult<Project>> GetAllPaging(string keyword, Page page);

        Task<List<Project>> FilterByInvesloper(string invesloper);
        Task<List<Project>> FilterOtherInvesloper();
        

        Task<List<ProjectMedia>> GetProjectMedia(long projectId);

        Task<List<ProjectBanner>> GetProjectBanner();

        Task<int> SetHighlightProject(long id, bool highlight);

    }
}