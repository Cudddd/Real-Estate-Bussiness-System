using System.Collections.Generic;
using BDS.Data.Entities;

namespace BDS.WebApp.Models.Admin
{
    public class NewRealEstateViewModel
    {
         public List<RealEstateType> realEstateTypes { get; set; }
                
         public List<Area> areas { get; set; }
    }
}