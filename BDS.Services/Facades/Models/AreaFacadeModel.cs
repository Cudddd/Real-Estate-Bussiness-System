using BDS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Services.Facades.Models
{
    using BDS.Services.Model;
    using Data.Entities;
    public class AreaFacadeModel
    {
        public List<Project> ListProject { get; set; }
        public WishlistModel ListWishList { get; set; }
        public List<RealEstateModel> ListRealEstate { get; set; }
    }
}
