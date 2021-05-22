namespace BDS.Data.Entities
{
    public class Project
    {
        private long id;
        private string name;
        private string invesloper; // chủ đầu tư
        private int district;
        private string introduce;
        private string info;
        private string customerBenefits;
        private string procedure; // tiến độ

        public long Id
        {
            get => id;
            set => id = value;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public string Invesloper
        {
            get => invesloper;
            set => invesloper = value;
        }

        public int District
        {
            get => district;
            set => district = value;
        }

        public string Introduce
        {
            get => introduce;
            set => introduce = value;
        }

        public string Info
        {
            get => info;
            set => info = value;
        }

        public string CustomerBenefits
        {
            get => customerBenefits;
            set => customerBenefits = value;
        }

        public string Procedure
        {
            get => procedure;
            set => procedure = value;
        }
    }
}