namespace BDS.Data.Entities
{
    public class Project
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

    }
}