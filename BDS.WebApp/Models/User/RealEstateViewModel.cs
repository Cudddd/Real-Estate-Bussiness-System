using System.Collections.Generic;
using BDS.Data.Entities;
using BDS.Services.Model;

namespace BDS.WebApp.Models.User
{
    public class RealEstateViewModel
    {
        public Data.Entities.User user { get; set; }
        public List<UserRealEstateModel> realEstates { get; set; }
        
    }
}