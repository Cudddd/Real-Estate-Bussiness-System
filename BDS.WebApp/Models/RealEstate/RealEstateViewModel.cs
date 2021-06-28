using System.Collections.Generic;
using BDS.Services.Model;

namespace BDS.WebApp.Models.RealEstate
{
    using Data.Entities;
    public class RealEstateViewModel
    {
        public List<RealEstateModel> realEstates { get; set; }
        
        public  int pageIndex { get; set; }

    }
}