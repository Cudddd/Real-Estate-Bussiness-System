using System.Threading.Tasks;
using BDS.Data.EF;
using BDS.Services.Common;
using Microsoft.EntityFrameworkCore;

namespace BDS.Services.NewsMedia
{
    public class NewsMediaService : INewsMediaService
    {
        private readonly BdsDbContext _context;
        private readonly IStorageService _storageService;

        public NewsMediaService(BdsDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }
        public async Task<int> Create(Data.Entities.NewsMedia newsMedia)
        {
            await _context.NewsMedia.AddAsync(newsMedia);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Detele(long id)
        {
            var entity = await _context.NewsMedia.FirstOrDefaultAsync(x=>x.id == id);

            if (entity != null)
            {
                await _storageService.DeleteFileAsync(entity.Path);
                
                _context.NewsMedia.Remove(entity);
                return await _context.SaveChangesAsync();
            }

            return 0;
        }
    }
}