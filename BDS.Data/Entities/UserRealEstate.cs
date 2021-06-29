using System;
using BDS.Data.Enum;
using Microsoft.AspNetCore.Http;

namespace BDS.Data.Entities
{
    public class UserRealEstate
    {
        public long id { get; set; }
        public long UserId { get; set; }
        public  string name { get; set; }
        public bool sell { get; set; }
        public double length { get; set; }
        public double width { get; set; }
        public Orientation orientation { get; set; } // hướng
        public double acreage { get; set; } // diện tích
        public double price { get; set; }
        public string location { get; set; }
        public long typeId { get; set; } // loại nhà
        public int facade { get; set; }// mặt tiền
        public string mainLine { get; set; }// đường chính
        public int floor { get; set; } // số tầng
        public int bedRoom { get; set; } // số phòng ngủ
        public int bathRoom { get; set; } // số phòng tắm
        public DateTime DateCreated { get; set; }
        public DateTime DateModify { get; set; }
        public string address { get; set; }

        public  string description { get; set; }
    }
}