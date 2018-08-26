using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace BaiGuiXe_Smart_API.Models.Slide
{
    public class Slide
    { 
    
        [BsonId]
        public ObjectId Id
        {
            get;
            set;
        }
      
        [BsonElement("TieuDe")]
        public string TieuDe
        {
            get;
            set;
        }
        [BsonElement("NoiDung")]
        public string NoiDung
        {
            get;
            set;
        }
        [BsonElement("HinhAnh")]
        public string HinhAnh
        {
            get;
            set;
        }
        [BsonElement("ListButton")]
        public string[] ListButton
        {
            get;
            set;
        }

    }
}