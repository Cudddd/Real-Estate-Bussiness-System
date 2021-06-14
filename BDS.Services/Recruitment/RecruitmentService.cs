using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDS.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace BDS.Services.Recruitment
{
    public class RecruitmentService : IRecruitmentService
    {
        private readonly BdsDbContext _context;

        public RecruitmentService(BdsDbContext context)
        {
            _context = context;
        }
        
        public Task<int> Create(Data.Entities.Recruitment r)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> Update(Data.Entities.Recruitment r)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> Delete(long recruitmentID)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Data.Entities.Recruitment> GetById(long recruitmentID)
        {
            var entity = await _context.Recruitment.FirstOrDefaultAsync(t => t.id == recruitmentID);
            
            return entity;
        }

        public Task<List<Data.Entities.Recruitment>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Data.Entities.Recruitment>> GetAllPaging(int pageIndex, int pageSize)
        {
            var data = await _context.Recruitment.OrderByDescending(r => r.dateModify).ToListAsync();

            var recruitments = data.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return recruitments;
        }
    }
}