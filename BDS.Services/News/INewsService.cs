using System.Collections.Generic;
using System.Threading.Tasks;

namespace BDS.Services.News
{
    using Data.Entities;
    public interface INewsService
    {
        Task<int> Create(News n);
        Task<int> Update(News n);
        Task<int> Delete(long newsID);
        Task<News> GetById(long newsID);
        Task<List<News>> GetAll();
        Task<List<News>> GetAllPaging(int pageIndex, int pageSize);

    }
}