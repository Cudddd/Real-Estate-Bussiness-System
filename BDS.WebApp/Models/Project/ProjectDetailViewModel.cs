using System.Collections.Generic;

namespace BDS.WebApp.Models.Project
{
    using Data.Entities;
    public class ProjectDetailViewModel
    {
        public Project project { get; set; }

        public List<Area> areas { get; set; }
        
        public Media projectBanner { get; set; }
        
        public Media introduceImg { get; set; } 
        
        public Media introduceVideo { get; set; }
        
        public Media procedureVideo { get; set; }
        
        public Media mapImg { get; set; }
    }
}