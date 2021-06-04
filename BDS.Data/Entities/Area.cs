using System;

namespace BDS.Data.Entities
{
    public class Area
    {
        public long id { get; set; }
        public long projectID { get; set; }
        public string name { get; set; }
        public int totalApartment { get; set; }
    }
}