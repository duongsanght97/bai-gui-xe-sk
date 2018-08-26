using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BaiGuiXe_Smart_API.Models
{
    public class User
    {
        [BsonId]
        public ObjectId Id
        {
            get;
            set;
        }
        [Required]
        [BsonElement("Email")]
        public string Email
        {
            get;
            set;
        }

        [Required]
        [BsonElement("PassWord")]
        public string PassWord
        {
            get;
            set;
        }
        [BsonElement("HoTenDem")]
        public string HoTenDem
        {
            get;
            set;
        }

        [BsonElement("Ten")]
        public string Ten
        {
            get;
            set;
        }
        [BsonElement("DiaChi")]
        public string DiaChi
        {
            get;
            set;
        }

        [BsonElement("SDT")]
        public string SDT
        {
            get;
            set;
        }
        [BsonElement("NgaySinh")]
        public DateTime NgaySinh
        {
            get;
            set;
        }
        [BsonElement("GioiTinh")]
        public bool GioiTinh
        {
            get;
            set;
        }
  
        [BsonElement("XacThucEmail")]
        public bool XacThucEmail
        {
            get;
            set;
        }
        [BsonElement("LoaiTaiKhoan")]
        public int LoaiTaiKhoan
        {
            get;
            set;
        }
        [BsonElement("ThoiGianDangKy")]
        public DateTime ThoiGianDangKy
        {
            get;
            set;
        }



    }
}