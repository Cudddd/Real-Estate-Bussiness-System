using System.Threading.Tasks;
using BDS.Data.EF;
using BDS.Services.Common;
using Microsoft.EntityFrameworkCore;

namespace BDS.Services.RecruitmentMedia
{
    using Data.Entities;
    public class RecruitmentMediaService : IRecruitmentMediaService
    {
        private readonly BdsDbContext _context;
        private readonly IStorageService _storageService;

        public RecruitmentMediaService(BdsDbContext context,IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<int> Create(RecruitmentMedia recruitment)
        {
            await _context.RecruitmentMedia.AddAsync(recruitment);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Detele(long id)
        {
            var entity = await _context.RecruitmentMedia.FirstOrDefaultAsync(x=>x.id == id);

            if (entity != null)
            {
                await _storageService.DeleteFileAsync(entity.Path);
                
                _context.RecruitmentMedia.Remove(entity);
                return await _context.SaveChangesAsync();
            }

            return 0;
        }
    }
}