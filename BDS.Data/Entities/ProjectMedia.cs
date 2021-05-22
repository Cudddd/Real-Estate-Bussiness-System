namespace BDS.Data.Entities
{
    public class ProjectMedia : Media
    {
        public long projectID;

        public long ProjectId
        {
            get => projectID;
            set => projectID = value;
        }
    }
}