using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace BDS.Services.Request.Recruitment
{
    public class RecruitmentCreateRequest
    {
        public long id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string detail { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime dateModify { get; set; }
        public List<IFormFile> recruitmentMedia { get; set; }
    }
}