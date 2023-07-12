using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Services.Facades.Models
{
    using BDS.Services.Model;
    using Data.Entities;
    public class HomeFacadeModel
    {
        public List<Project> ListProjects { get; set; }
        public WishlistModel ListWishList { get; set; }
    }
    
}
