namespace BDS.Data.Entities
{
    public class RealEstateMedia : Media
    {
        private long realEstateID;

        public long RealEstateId
        {
            get => realEstateID;
            set => realEstateID = value;
        }
    }
}