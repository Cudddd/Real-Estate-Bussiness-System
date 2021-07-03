using System.Threading.Tasks;

namespace BDS.Services.RecruitmentMedia
{
    using Data.Entities;
    public interface IRecruitmentMediaService
    {
        Task<int> Create(RecruitmentMedia recruitment);
        Task<int> Detele(long id);
    }
}