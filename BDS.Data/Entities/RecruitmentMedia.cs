namespace BDS.Data.Entities
{
    public class RecruitmentMedia : Media
    {
        private long recruitmentID;

        public long RecruitmentId
        {
            get => recruitmentID;
            set => recruitmentID = value;
        }
    }
}