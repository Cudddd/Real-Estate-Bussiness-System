using System.Collections.Generic;

namespace BDS.WebApp.Models.Project
{
    using Data.Entities;
    public class ProjectDetailViewModel
    {
        public Project project { get; set; }

        public List<Area> areas { get; set; }
    }
}