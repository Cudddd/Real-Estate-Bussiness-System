using System.Collections.Generic;
using BDS.Services.Model;


namespace BDS.WebApp.Models.Project
{
    using Data.Entities;
    public class ProjectViewModel
    {
        public List<ProjectModel> Projects { get; set; }
        
        public List<ProjectBanner> projectBanners { get; set; }
    }
}