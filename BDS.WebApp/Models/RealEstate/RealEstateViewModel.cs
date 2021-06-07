using System.Collections.Generic;

namespace BDS.WebApp.Models.RealEstate
{
    using Data.Entities;
    public class RealEstateViewModel
    {
        public List<RealEstate> realEstates { get; set; }

        public int pageSize = 2;
    }
}