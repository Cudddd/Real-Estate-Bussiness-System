using System.Collections.Generic;
using BDS.Data.Entities;
using BDS.Services.Model;

namespace BDS.WebApp.Models.Admin
{
    public class UpdateRealEstateViewModel
    {
        public List<RealEstateType> realEstateTypes { get; set; }
        
        public List<Area> areas { get; set; }
        
        public RealEstateModel realEstateModel { get; set; }
    }
}