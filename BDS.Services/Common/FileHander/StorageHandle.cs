using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Services.Common.FileHander
{
    public class MiniStorage : IFileHander
    {
        private const string USER_CONTENT_FOLDER_NAME_MINI = "assets/img/mini";
        public IFileHander Successor { get; set; }

        public async Task ProcessFile(Stream mediaBinaryStream, string fileName, string userContentFolder)
        {
            if (mediaBinaryStream.Length < 1000)
            {
                var path = Path.Combine(userContentFolder, USER_CONTENT_FOLDER_NAME_MINI);
                var filePath = Path.Combine(path, fileName);
                await using var output = new FileStream(filePath, FileMode.Create);
                await mediaBinaryStream.CopyToAsync(output);
            }
            else
            {
                await Successor.ProcessFile(mediaBinaryStream, fileName, userContentFolder);
            }
        }
    }

    public class MediumStorage : IFileHander
    {
        private const string USER_CONTENT_FOLDER_NAME_MEDIUM = "assets/img/medium";
        public IFileHander Successor { get; set; }

        public async Task ProcessFile(Stream mediaBinaryStream, string fileName, string userContentFolder)
        {
            if (mediaBinaryStream.Length < 10000)
            {
                var path = Path.Combine(userContentFolder, USER_CONTENT_FOLDER_NAME_MEDIUM);
                var filePath = Path.Combine(path, fileName);
                await using var output = new FileStream(filePath, FileMode.Create);
                await mediaBinaryStream.CopyToAsync(output);
            }
            else
            {
                await Successor.ProcessFile(mediaBinaryStream, fileName, userContentFolder);
            }
        }
    }

    public class BigStorage : IFileHander
    {
        private const string USER_CONTENT_FOLDER_NAME_BIG = "assets/img/big";
        public IFileHander Successor { get; set; }

        public async Task ProcessFile(Stream mediaBinaryStream, string fileName, string userContentFolder)
        {
            var path = Path.Combine(userContentFolder, USER_CONTENT_FOLDER_NAME_BIG);
            var filePath = Path.Combine(path, fileName);
            await using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
        }


        public class ChainOfHandlers
        {
            readonly IFileHander _mini = new MiniStorage();
            readonly IFileHander _medium = new MediumStorage();
            readonly IFileHander _big = new BigStorage();
            public ChainOfHandlers()
            {
                _mini.Successor = _medium;
                _medium.Successor = _big;
                _big.Successor = null;
            }

            public void Handle(Stream mediaBinaryStream, string fileName, string userContentFolder)
            {
                _mini.ProcessFile(mediaBinaryStream, fileName, userContentFolder);
            }
        }
    }

}
