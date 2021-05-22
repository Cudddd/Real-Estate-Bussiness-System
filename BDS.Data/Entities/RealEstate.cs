namespace BDS.Data.Entities
{
    public class RealEstate
    {
        private long id;
        private long areaID;
        private bool sell;
        private double length;
        private double width;
        private string orientation; // hướng
        private double acreage; // diện tích
        private double price;
        private string location;
        private string type; // loại nhà
        private int facade; // mặt tiền
        private string mainLine; // đường chính
        private int floor; // số tầng
        private int bedRoom; // số phòng ngủ
        private int bathRoom; // số phòng tắm

        public long Id
        {
            get => id;
            set => id = value;
        }

        public long AreaId
        {
            get => areaID;
            set => areaID = value;
        }

        public bool Sell
        {
            get => sell;
            set => sell = value;
        }

        public double Length
        {
            get => length;
            set => length = value;
        }

        public double Width
        {
            get => width;
            set => width = value;
        }

        public string Orientation
        {
            get => orientation;
            set => orientation = value;
        }

        public double Acreage
        {
            get => acreage;
            set => acreage = value;
        }

        public double Price
        {
            get => price;
            set => price = value;
        }

        public string Location
        {
            get => location;
            set => location = value;
        }

        public string Type
        {
            get => type;
            set => type = value;
        }

        public int Facade
        {
            get => facade;
            set => facade = value;
        }

        public string MainLine
        {
            get => mainLine;
            set => mainLine = value;
        }

        public int Floor
        {
            get => floor;
            set => floor = value;
        }

        public int BedRoom
        {
            get => bedRoom;
            set => bedRoom = value;
        }

        public int BathRoom
        {
            get => bathRoom;
            set => bathRoom = value;
        }
    }
}