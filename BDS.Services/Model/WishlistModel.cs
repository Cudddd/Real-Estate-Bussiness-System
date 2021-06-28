using System.Collections.Generic;

namespace BDS.Services.Model
{
    public class WishlistModel
    {
        public long id { get; set; }
        public long UserId { get; set; }
        public List<RealEstateModel> realEstates { get; set; }
    }
}