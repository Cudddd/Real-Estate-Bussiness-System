using System.Collections.Generic;
using System.Threading.Tasks;
using BDS.Services.Model;
using BDS.Services.Request.Recruitment;

namespace BDS.Services.Recruitment
{
    using Data.Entities;
    public interface IRecruitmentService
    {
        Task<int> Create(RecruitmentCreateRequest r);
        Task<int> Update(Recruitment r);
        Task<int> Delete(long recruitmentID );
        Task<RecruitmentModel> GetById(long recruitmentID);
        Task<List<Recruitment>> GetAll();
        Task<List<RecruitmentModel>> GetAllPaging(int pageIndex, int pageSize);
    }
}