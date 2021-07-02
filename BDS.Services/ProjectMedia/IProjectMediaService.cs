using System.Collections.Generic;
using System.Threading.Tasks;

namespace BDS.Services.ProjectMedia
{
    using Data.Entities;
    public interface IProjectMediaService
    {
        Task<int> Create(ProjectMedia projectMedia);
        Task<int> CreateRange(List<ProjectMedia> projectMedia);
        Task<int> Update(ProjectMedia projectMedia);
        Task<int> Delete(long projectMediaId);
        Task<int> DeleteRange(List<ProjectMedia> projectMedia);
        Task<ProjectMedia> GetById(long projectMediaId);
        Task<List<ProjectMedia>> GetByProjectId(long projectId);
        Task<List<ProjectMedia>> GetAll();
    }
}