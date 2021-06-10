using System;

namespace BDS.Data.Entities
{
    public class Recruitment
    {
        public long id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string detail { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime dateModify { get; set; }
        
    }
}