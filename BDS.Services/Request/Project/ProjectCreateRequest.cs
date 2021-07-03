using Microsoft.AspNetCore.Http;

namespace BDS.Services.Request
{
    public class ProjectCreateRequest
    {
        public long id { get; set; }
        public string name { get; set; }
        public string invesloper { get; set; } // chủ đầu tư
        public string introduce { get; set; }
        public string info { get; set; }
        public string customerBenefits { get; set; }
        public string procedure { get; set; } // tiến độ
        public bool highlight { get; set; }
        public IFormFile BannerImg { get; set; }
        public IFormFile IntroImg { get; set; }
        public IFormFile MapImg { get; set; }
        public string ProcedureVid { get; set; }
        public string IntroduceVid { get; set; }
    }
}