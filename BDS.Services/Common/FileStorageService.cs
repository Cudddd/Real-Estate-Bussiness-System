using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using static BDS.Services.Common.FileHander.BigStorage;

namespace BDS.Services.Common
{
    public class FileStorageService : IStorageService
    {
        private readonly string _userContentFolder;
        private const string USER_CONTENT_FOLDER_NAME = "assets/img";

        public FileStorageService(IWebHostEnvironment webHostEnvironment)
        {
            _userContentFolder = Path.Combine(webHostEnvironment.WebRootPath);
        }
        public string GetUserContentForderName()
        {
            return USER_CONTENT_FOLDER_NAME;
        }
        public async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await SaveFileAsync(file.OpenReadStream(), fileName);
            return USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }
        public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {
            var chain = new ChainOfHandlers();
            chain.Handle(mediaBinaryStream, fileName, _userContentFolder);
        }
        

        public string GetFileUrl(string fileName)
        {
            return $"/{USER_CONTENT_FOLDER_NAME}/{fileName}";
        }

        public async Task DeleteFileAsync(string path)
        {
            var filePath = Path.Combine(_userContentFolder, path);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }
    }
}