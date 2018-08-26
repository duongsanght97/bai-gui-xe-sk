using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace BaiGuiXe_Smart_API.Models.Slide
{
    public class LinkSlide_Model
    {
        Connect_MongoDB<ListLink> db;
        public LinkSlide_Model()
        {
            db = new Connect_MongoDB<ListLink>("UrlSlide");
        }

        public List<ListLink> Find(ObjectId id)
        {

            return db.mongocollection.AsQueryable().Where(x => x.Id_Slide == id).ToList();
        }
        public List<ListLink> FindAll()
        {

            return db.mongocollection.AsQueryable<ListLink>().ToList();
        }
        public void Create(ListLink listlink, ObjectId id)
        {
            listlink.Id_Slide = id;
            db.mongocollection.InsertOne(listlink);

        }
        public void Update(ListLink listlink)
        {
            db.mongocollection.UpdateOne(
                Builders<ListLink>.Filter.Eq("_id", listlink.Id),
                Builders<ListLink>.Update
                .Set("Ten", listlink.Ten)
                .Set("Link", listlink.Link)
                );
        }
        public void Delete(ObjectId id)
        {
            db.mongocollection.DeleteOne(
                Builders<ListLink>.Filter.Eq("id", id)
                );
        }
    }
}