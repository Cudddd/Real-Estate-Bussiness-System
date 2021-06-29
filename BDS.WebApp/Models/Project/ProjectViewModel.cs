using System.Collections.Generic;


namespace BDS.WebApp.Models.Project
{
    using Data.Entities;
    public class ProjectViewModel
    {
        public List<Project> Projects { get; set; }
        
        public List<ProjectBanner> projectBanners { get; set; }
    }
}