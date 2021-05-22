using System;

namespace BDS.Data.Entities
{
    public class Area
    {
        private long id;
        private long projectID;
        private string name;
        private int totalApartment;
        private int scale; //quy mô
        private string areaApartment;
        private DateTime handOverTime; // tgian bàn giao

        public long Id
        {
            get => id;
            set => id = value;
        }

        public long ProjectId
        {
            get => projectID;
            set => projectID = value;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public int TotalApartment
        {
            get => totalApartment;
            set => totalApartment = value;
        }

        public int Scale
        {
            get => scale;
            set => scale = value;
        }

        public string AreaApartment
        {
            get => areaApartment;
            set => areaApartment = value;
        }

        public DateTime HandOverTime
        {
            get => handOverTime;
            set => handOverTime = value;
        }
    }
}