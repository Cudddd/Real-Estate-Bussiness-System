using System;

namespace BDS.Data.Entities
{
    public class News
    {
        public long id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime dateModify { get; set; }
        public string description { get; set; }
        
    }
}