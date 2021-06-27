using System.Collections.Generic;
using BDS.Data.Entities;
using BDS.Services.Request;

namespace BDS.WebApp.Models.RealEstate
{
    public class RealEstateSellViewModel
    {
        public List<RealEstateType> realEstateTypes { get; set; }
        
        public RealEstateCreateRequest request { get; set; }
        
    }
}