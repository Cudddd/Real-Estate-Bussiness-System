namespace BDS.Data.Entities
{
    public class WishlistRealEstate
    {
        public long id { get; set; }

        public long WishlistId { get; set; }
        public long RealEstateId { get; set; }
    }
}