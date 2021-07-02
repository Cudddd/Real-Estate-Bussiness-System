using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDS.Data.EF;
using BDS.Services.Model;
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

        public async Task<RecruitmentModel> GetById(long recruitmentID)
        {
            var entity = await _context.Recruitment.FirstOrDefaultAsync(t => t.id == recruitmentID);

            RecruitmentModel result = new RecruitmentModel();

            if (entity != null)
            {
                RecruitmentModel recruitmentModel = new RecruitmentModel()
                {
                    id = entity.id,
                    title = entity.title,
                    description = entity.description,
                    detail = entity.detail,
                    dateCreated = entity.dateCreated,
                    dateModify = entity.dateModify,
                    recruitmentMedia =
                        await _context.RecruitmentMedia.Where(x => x.RecruitmentId == entity.id).ToListAsync(),
                };
                result = recruitmentModel;
            }
            
            return result;
        }

        public Task<List<Data.Entities.Recruitment>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<RecruitmentModel>> GetAllPaging(int pageIndex, int pageSize)
        {
            var data = await _context.Recruitment.OrderByDescending(r => r.dateModify).ToListAsync();

            List<RecruitmentModel> recruitmentModels = new List<RecruitmentModel>();
            foreach (var item in data)
            {
                RecruitmentModel recruitmentModel = new RecruitmentModel();
                recruitmentModel.id = item.id;
                recruitmentModel.title = item.title;
                recruitmentModel.detail = item.detail;
                recruitmentModel.description = item.description;
                recruitmentModel.dateCreated = item.dateCreated;
                recruitmentModel.dateModify = item.dateModify;
                recruitmentModel.recruitmentMedia = _context.RecruitmentMedia.Where(x => x.RecruitmentId == item.id).ToList();
                
                recruitmentModels.Add(recruitmentModel);
            }
            
            recruitmentModels = recruitmentModels.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return recruitmentModels;
        }
    }
}