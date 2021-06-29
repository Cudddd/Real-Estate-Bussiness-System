using System.Collections.Generic;
using BDS.Data.Entities;

namespace BDS.WebApp.Models.Admin
{
    public class ProjectDetailViewModel
    {
        public Data.Entities.Project Project { get; set; }
        
        public List<ProjectMedia> Media { get; set; }
    }
}