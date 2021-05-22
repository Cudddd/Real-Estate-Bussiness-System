namespace BDS.Data.Entities
{
    public class Media
    {
        private long id;
        private int type;
        private string path;

        public long Id
        {
            get => id;
            set => id = value;
        }

        public int Type
        {
            get => type;
            set => type = value;
        }

        public string Path
        {
            get => path;
            set => path = value;
        }
    }
}