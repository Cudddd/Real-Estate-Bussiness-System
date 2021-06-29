using System.Collections.Generic;
using BDS.Data.Entities;
using BDS.Services.Model;

namespace BDS.WebApp.Models.Admin
{
    public class RealEstateDetailViewModel
    {
        public RealEstateModel realEstateModel { get; set; }
        public List<RealEstateMedia> Media { get; set; }
    }
}