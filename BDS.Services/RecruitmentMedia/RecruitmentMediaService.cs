using System.Threading.Tasks;
using BDS.Data.EF;

namespace BDS.Services.RecruitmentMedia
{
    using Data.Entities;
    public class RecruitmentMediaService : IRecruitmentMediaService
    {
        private readonly BdsDbContext _context;

        public RecruitmentMediaService(BdsDbContext context)
        {
            _context = context;
        }

        public Task<int> Create(RecruitmentMedia recruitment)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> Detele(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}