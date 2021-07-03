using System.Collections.Generic;
using System.Threading.Tasks;
using BDS.Services.Model;
using BDS.Services.Request.News;

namespace BDS.Services.News
{
    using Data.Entities;
    public interface INewsService
    {
        Task<int> Create(NewsCreateRequest n);
        Task<int> Update(News n);
        Task<int> Delete(long newsID);
        Task<NewsModel> GetById(long newsID);
        Task<List<News>> GetAll();
        Task<List<NewsModel>> GetAllPaging(int pageIndex, int pageSize);

    }
}