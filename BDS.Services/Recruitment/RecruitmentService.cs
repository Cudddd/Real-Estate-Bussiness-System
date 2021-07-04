using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDS.Data.EF;
using BDS.Data.Enum;
using BDS.Services.Common;
using BDS.Services.Model;
using BDS.Services.RecruitmentMedia;
using BDS.Services.Request.Recruitment;
using BDS.Services.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BDS.Services.Recruitment
{
    using Data.Entities;
    public class RecruitmentService : IRecruitmentService
    {
        private readonly BdsDbContext _context;
        private readonly IStorageService _storageService;
        private readonly IRecruitmentMediaService _recruitmentMediaService;
        public RecruitmentService(BdsDbContext context,IStorageService storageService,
            IRecruitmentMediaService recruitmentMediaService)
        {
            _context = context;
            _storageService = storageService;
            _recruitmentMediaService = recruitmentMediaService;
        }
        
        public async Task<int> Create(RecruitmentCreateRequest request)
        {
            Recruitment recruitment = new Recruitment()
            {
                id = UtilitiesService.GenerateID(),
                title = request.title,
                description = request.description,
                detail = request.detail,
                dateCreated = DateTime.Now,
                dateModify = DateTime.Now,
            };
            await _context.Recruitment.AddAsync(recruitment);

            foreach (var item in request.recruitmentMedia)
            {
                if (item != null)
                {
                    RecruitmentMedia media = new RecruitmentMedia()
                    {
                        id = UtilitiesService.GenerateID(),
                        RecruitmentId = recruitment.id,
                        Type = MediaType.ThumnailImg,
                        Path = await _storageService.SaveFile(item),
                    };

                    await _recruitmentMediaService.Create(media);

                }
            }

            return await _context.SaveChangesAsync();
            
        }

        public async Task<int> Update(Data.Entities.Recruitment recruitment)
        {
            var entity = await _context.Recruitment.FirstOrDefaultAsync(x=>x.id == recruitment.id);

            if (entity != null)
            {
                entity.title = recruitment.title;
                entity.description = recruitment.description;
                entity.detail = recruitment.detail;
                entity.dateModify = DateTime.Now;

                _context.Recruitment.Update(entity);
                return await _context.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<int> Delete(long recruitmentID)
        {
            var entity = await _context.Recruitment.FirstOrDefaultAsync(x=>x.id == recruitmentID);

            if (entity != null)
            {
                var media = await _context.RecruitmentMedia.Where(x=>x.RecruitmentId == entity.id).ToListAsync();

                foreach (var item in media)
                {
                    await _recruitmentMediaService.Detele(item.id);
                }

                _context.Recruitment.Remove(entity);
                return await _context.SaveChangesAsync();
            }

            return 0;
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