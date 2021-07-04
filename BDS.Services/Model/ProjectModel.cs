using System.Collections.Generic;

namespace BDS.Services.Model
{
    public class ProjectModel
    {
        public long id { get; set; }
        public string name { get; set; }
        public string invesloper { get; set; } // chủ đầu tư
        public int district { get; set; }
        public string introduce { get; set; }
        public string info { get; set; }
        public string customerBenefits { get; set; }
        public string procedure { get; set; } // tiến độ
        public bool highlight { get; set; }
        public List<Data.Entities.ProjectMedia> projectMedia { get; set; }
    }
}