using System.Collections.Generic;
using System.Threading.Tasks;

namespace BDS.Services.Recruitment
{
    using Data.Entities;
    public interface IRecruitmentService
    {
        Task<int> Create(Recruitment r);
        Task<int> Update(Recruitment r);
        Task<int> Delete(long recruitmentID );
        Task<Recruitment> GetById(long recruitmentID);
        Task<List<Recruitment>> GetAll();
        Task<List<Recruitment>> GetAllPaging(int pageIndex, int pageSize);
    }
}