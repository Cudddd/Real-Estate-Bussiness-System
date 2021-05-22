using System;

namespace BDS.Data.Entities
{
    public class News
    {
        private long id;
        private string title;
        private string content;
        private DateTime postDate;
        private string description;

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

        public string Content
        {
            get => content;
            set => content = value;
        }

        public DateTime PostDate
        {
            get => postDate;
            set => postDate = value;
        }

        public string Description
        {
            get => description;
            set => description = value;
        }
    }
}