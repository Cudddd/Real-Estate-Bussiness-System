using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace BDS.Services.Request.News
{
    public class NewsCreateRequest
    {
        public long id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime dateModify { get; set; }
        public string description { get; set; }
        public List<IFormFile> newsMedia { get; set; }
    }
}