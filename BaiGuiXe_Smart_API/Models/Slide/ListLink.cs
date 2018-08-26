using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaiGuiXe_Smart_API.Models.Slide
{
    public class ListLink
    {
        [BsonId]
        public ObjectId Id
        {
            get;
            set;
        }
        [BsonElement("Id_Slide")]
        public ObjectId Id_Slide
        {
            get;
            set;
        }
        [BsonElement("Ten")]
        public string Ten { set; get; }
        [BsonElement("Link")]
        public string Link { set; get; }

    }
}