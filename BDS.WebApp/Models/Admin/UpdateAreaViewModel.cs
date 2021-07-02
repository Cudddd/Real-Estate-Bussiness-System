using System.Collections.Generic;
using BDS.Data.Entities;

namespace BDS.WebApp.Models.Admin
{
    public class UpdateAreaViewModel
    {
        public List<BDS.Data.Entities.Project> projects { get; set; }
        public Area area { get; set; }
    }
}