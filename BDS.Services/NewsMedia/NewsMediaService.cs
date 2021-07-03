using System.Threading.Tasks;
using BDS.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace BDS.Services.NewsMedia
{
    public class NewsMediaService : INewsMediaService
    {
        private readonly BdsDbContext _context;

        public NewsMediaService(BdsDbContext context)
        {
            _context = context;
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
                _context.NewsMedia.Remove(entity);
                return await _context.SaveChangesAsync();
            }

            return 0;
        }
    }
}