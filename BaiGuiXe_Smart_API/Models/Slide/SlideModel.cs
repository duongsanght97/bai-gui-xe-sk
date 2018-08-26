using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
namespace BaiGuiXe_Smart_API.Models.Slide
{
    public class SlideModel
    {
        Connect_MongoDB<Slide> db;
        public SlideModel()
        {
            db = new Connect_MongoDB<Slide>("Slide");
        }

        public List<Slide> FindAll()
        {
           
            return db.mongocollection.AsQueryable<Slide>().ToList();
        }

        public List<Slide> FindWithTitle(string TieuDe)
        {
            //db.mongocollection.Indexes.CreateOne(Builders<Slide>.IndexKeys.Text(x => x.TieuDe));
            //// https://stackoverflow.com/questions/41356544/full-text-search-in-mongodb-in-net
            return db.mongocollection.Find(Builders<Slide>.Filter.Text(TieuDe)).ToList();

        }
        public void Create(Slide sl)
        {
            
            db.mongocollection.InsertOne(sl);
        }

       
        

        public void Update(Slide sl)
        {
            db.mongocollection.UpdateOne(
                Builders<Slide>.Filter.Eq("_id", sl.Id),
                Builders<Slide>.Update
                .Set("TieuDe", sl.TieuDe)
                .Set("NoiDung", sl.NoiDung)
                .Set("HinhAnh", sl.HinhAnh)
                .Set("ListButton", sl.ListButton)
                );
        }

        public void Delete(string id)
        {
            db.mongocollection.DeleteOne(
                Builders<Slide>.Filter.Eq("Id", id)
                );
        }



    }
}