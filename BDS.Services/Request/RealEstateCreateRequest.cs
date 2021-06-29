
using System.ComponentModel.DataAnnotations;
using BDS.Data.Entities;
using BDS.Data.Enum;
using BDS.Services.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BDS.Services.Request
{
    public class RealEstateCreateRequest
    {
        public long id { get; set; }

   
        public string name { get; set; }
    
        public string sell { get; set; }
    
        //[DataType(DataType.Currency, ErrorMessage = "Data type must be double")]
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
        public  string description { get; set; }
        
        public string address { get; set; }

        public IFormFile[] realEstateImgs { get; set; }


        
    }
}