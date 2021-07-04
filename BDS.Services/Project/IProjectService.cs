using System.Collections.Generic;
using System.Threading.Tasks;
using BDS.Services.Common;
using BDS.Services.Model;
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
        Task<List<Project>> GetAllPaging(int pageIndex,int pageSize);

        Task<List<ProjectModel>> FilterByInvesloper(string invesloper);
        Task<List<ProjectModel>> FilterOtherInvesloper();
        

        Task<List<ProjectMedia>> GetProjectMedia(long projectId);

        Task<List<ProjectBanner>> GetProjectBanner();

        Task<int> SetHighlightProject(long id, bool highlight);

    }
}