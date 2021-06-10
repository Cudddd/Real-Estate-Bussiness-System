using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDS.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace BDS.Services.News
{
    public class NewsService : INewsService
    {
        private readonly BdsDbContext _context;

        public NewsService(BdsDbContext context)
        {
            _context = context;
        }
        public Task<int> Create(Data.Entities.News n)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> Update(Data.Entities.News n)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> Delete(long newsID)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Data.Entities.News> GetById(long newsID)
        {
            var entity = await _context.News.FirstOrDefaultAsync(t => t.id == newsID);
            
            return entity;
        }

        public Task<List<Data.Entities.News>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Data.Entities.News>> GetAllPaging(int pageIndex, int pageSize)
        {
            var data = await _context.News.OrderByDescending(n => n.dateModify).ToListAsync();

            var news = data.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return news;
        }

    }
}