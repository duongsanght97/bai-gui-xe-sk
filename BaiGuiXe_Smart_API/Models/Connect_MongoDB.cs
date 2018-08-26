using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace BaiGuiXe_Smart_API.Models
{
    public class Connect_MongoDB<T>
    {
        public MongoClient mongoclient;
        public IMongoCollection<T> mongocollection;

        public Connect_MongoDB(string collection)
        {
            mongoclient = new MongoClient(ConfigurationManager.AppSettings["mongoDBHost"]);
            var db = mongoclient.GetDatabase(ConfigurationManager.AppSettings["mongoDBName"]);
            mongocollection = db.GetCollection<T>(collection);
         
            
        }
       

    }
}