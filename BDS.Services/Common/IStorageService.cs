using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BDS.Services.Common
{
    public interface IStorageService
    {
        string GetFileUrl(string fileName);

        Task<string> SaveFile(IFormFile file);
        Task SaveFileAsync(Stream mediaBinaryStream, string fileName);

        Task DeleteFileAsync(string fileName);
        string GetUserContentForderName();
    }
}