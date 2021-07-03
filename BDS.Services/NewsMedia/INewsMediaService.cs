using System.Threading.Tasks;

namespace BDS.Services.NewsMedia
{
    public interface INewsMediaService
    {
        Task<int> Create(Data.Entities.NewsMedia newsMedia);
        Task<int> Detele(long id);
    }
}