using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaiGuiXe_Smart_API.Models.XacMinh
{
    public class XacMinh
    {
        [BsonId]
        public ObjectId Id
        {
            get;
            set;
        }
        [Required]
        [BsonElement("Code")]
        public string Code
        {
            get;
            set;
        }
        [BsonElement("Timer")]
        public DateTime Timer
        {
            get;
            set;
        }
        [BsonElement("Id_User")]
        public ObjectId Id_User
        {
            get;
            set;
        }


    }
}