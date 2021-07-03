using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDS.Data.EF;
using BDS.Data.Enum;
using BDS.Services.Common;
using BDS.Services.Model;
using BDS.Services.NewsMedia;
using BDS.Services.Request.News;
using BDS.Services.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BDS.Services.News
{
    using Data.Entities;
    public class NewsService : INewsService
    {
        private readonly BdsDbContext _context;
        private readonly INewsMediaService _newsMediaService;
        private readonly IStorageService _storageService;

        public NewsService(BdsDbContext context,INewsMediaService newsMediaService,
            IStorageService storageService)
        {
            _context = context;
            _newsMediaService = newsMediaService;
            _storageService = storageService;
        }
        public async Task<int> Create(NewsCreateRequest request)
        {
            News news = new News()
            {
                id = UtilitiesService.GenerateID(),
                title = request.title,
                content = request.content,
                description = request.description,
                dateCreated = DateTime.Now,
                dateModify = DateTime.Now,
            };
            await _context.News.AddAsync(news);

            foreach (var item in request.newsMedia)
            {
                if (item != null)
                {
                    NewsMedia media = new NewsMedia()
                    {
                        id = UtilitiesService.GenerateID(),
                        NewsId = news.id,
                        Type = MediaType.ThumnailImg,
                        Path = await _storageService.SaveFile(item),
                    };

                    await _newsMediaService.Create(media);
                }
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(Data.Entities.News news)
        {
            var entity = await _context.News.FirstOrDefaultAsync(x=>x.id == news.id);
            if (entity != null)
            {
                entity.title = news.title;
                entity.content = news.content;
                entity.description = news.description;
                entity.dateModify = DateTime.Now;

                _context.News.Update(entity);
                return await _context.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<int> Delete(long newsID)
        {
            var entity = await _context.News.FirstOrDefaultAsync(x=>x.id == newsID);
            if (entity != null)
            {

                List<NewsMedia> media = await _context.NewsMedia.Where(x => x.NewsId == entity.id).ToListAsync();

                foreach (var item in media)
                {
                    await _storageService.DeleteFileAsync(item.Path);
                    
                    await _newsMediaService.Detele(item.id);
                }
                
                _context.News.Remove(entity);
                return await _context.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<NewsModel> GetById(long newsID)
        {
            var entity = await _context.News.FirstOrDefaultAsync(t => t.id == newsID);
            NewsModel newsModel = new NewsModel();
            if (entity != null)
            {
                newsModel.id = entity.id;
                newsModel.title = entity.title;
                newsModel.content = entity.content;
                newsModel.dateCreated = entity.dateCreated;
                newsModel.dateModify = entity.dateModify;
                newsModel.description = entity.description;
                newsModel.newsMedia = _context.NewsMedia.Where(x => x.NewsId == entity.id).ToList();
            }
            
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