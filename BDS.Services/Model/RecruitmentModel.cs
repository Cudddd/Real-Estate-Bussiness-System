using System;
using System.Collections.Generic;
using BDS.Data.Entities;

namespace BDS.Services.Model
{
    public class RecruitmentModel
    {
        public long id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string detail { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime dateModify { get; set; }
        public List<RecruitmentMedia> recruitmentMedia { get; set; }
        
    }
}