using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDS.Data.EF;
using BDS.Services.Model;
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

        public async Task<NewsModel> GetById(long newsID)
        {
            var entity = await _context.News.FirstOrDefaultAsync(t => t.id == newsID);
            
            NewsModel newsModel = new NewsModel();
            newsModel.id = entity.id;
            newsModel.title = entity.title;
            newsModel.content = entity.content;
            newsModel.dateCreated = entity.dateCreated;
            newsModel.dateModify = entity.dateModify;
            newsModel.description = entity.description;
            newsModel.newsMedia = _context.NewsMedia.Where(x => x.NewsId == entity.id).ToList();
            
            return newsModel;
        }

        public Task<List<Data.Entities.News>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<NewsModel>> GetAllPaging(int pageIndex, int pageSize)
        {
            var data = await _context.News.OrderByDescending(n => n.dateModify).ToListAsync();
            
            //var newsMedia = _context.NewsMedia.Where(x=>x.NewsId ==)

            List<NewsModel> newsModels = new List<NewsModel>();
            foreach (var item in data)
            {
                NewsModel newsModel = new NewsModel();
                newsModel.id = item.id;
                newsModel.title = item.title;
                newsModel.content = item.content;
                newsModel.dateCreated = item.dateCreated;
                newsModel.dateModify = item.dateModify;
                newsModel.description = item.description;
                newsModel.newsMedia = _context.NewsMedia.Where(x => x.NewsId == item.id).ToList();
                
                newsModels.Add(newsModel);
            }

            newsModels = newsModels.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return newsModels;
        }

    }
}