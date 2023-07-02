using BDS.Data.EF;
using BDS.Services.Common;
using BDS.Services.NewsMedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Services.News
{
    public class NewsServiceAbstractFactory : INewsServiceAbstractFactory
    {
        private readonly BdsDbContext _context;
        private readonly INewsMediaService _newsMediaService;
        private readonly IStorageService _storageService;

        public NewsServiceAbstractFactory(BdsDbContext context, INewsMediaService newsMediaService,
            IStorageService storageService)
        {
            _context = context;
            _newsMediaService = newsMediaService;
            _storageService = storageService;
        }
        public INewsService CreateNewService()
        {
           return new NewsService(_context, _newsMediaService, _storageService);
        }
    }
}
