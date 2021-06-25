using BDS.Data.Enum;

namespace BDS.Data.Entities
{
    public class Media
    {
        public long id { get; set; }
        public MediaType Type { get; set; }
        public string Path { get; set; } = ""; 
    }
}