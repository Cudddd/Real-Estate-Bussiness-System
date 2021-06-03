using System.Collections.Generic;
using System.Threading.Tasks;
using BDS.Services.Common;

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
        

    }
}