using System;

namespace BDS.Data.Entities
{
    public class Recruitment
    {
        private long id;
        private string title;
        private string description;
        private string detail;
        private DateTime postDate;

        public long Id
        {
            get => id;
            set => id = value;
        }

        public string Title
        {
            get => title;
            set => title = value;
        }

        public string Description
        {
            get => description;
            set => description = value;
        }

        public string Detail
        {
            get => detail;
            set => detail = value;
        }

        public DateTime PostDate
        {
            get => postDate;
            set => postDate = value;
        }
    }
}