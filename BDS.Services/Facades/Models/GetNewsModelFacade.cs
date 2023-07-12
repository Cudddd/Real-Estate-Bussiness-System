using BDS.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Services.Facades.Models
{
    using BDS.Services.Model;
    using Data.Entities;
    public class GetNewsModelFacade
    {
        public List<Project> ListProjects { get; set; }
        public WishlistModel ListWishList { get; set; }
        public List<NewsModel> ListNews { get; set; }
    }
    public class NewModelDetail
    {
        public List<Project> ListProjects { get; set; }
        public WishlistModel ListWishList { get; set; }
        public NewsModel New { get; set; }
    }
}
