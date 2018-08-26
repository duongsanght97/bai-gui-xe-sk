using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;


namespace BaiGuiXe_Smart_API.Models
{
    public class UserModel
    {
        //public MongoClient mongoclient;
        //public IMongoCollection<User> mongocollection;
        Connect_MongoDB<User> db;
        public UserModel()
        {
            db = new Connect_MongoDB<User>("User");
        }

        public List<User> FindAll()
        {
            //Connect_MongoDB<User> db = new Connect_MongoDB<User>("User");
            return db.mongocollection.AsQueryable().ToList();
        }

        // tìm kiếm theo email
        public User Find(string email)
        {
            string output = email.ToLower();
            var results = db.mongocollection.Find(x => x.Email == output).FirstOrDefault();
            return results;

        }
        public User FindId(ObjectId id)
        {
            
            var results = db.mongocollection.Find(x => x.Id == id).FirstOrDefault();
            return results;

        }
        public User login(string email, string pass)
        {
            string output = email.ToLower();

            return db.mongocollection.AsQueryable<User>().Where(x => x.Email == output && x.PassWord == pass).FirstOrDefault();
        }

        public bool checkemail(string email)
        {
            string output = email.ToLower();
           
            if (Find(output) != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        // thêm 1 người dùng
        public void Create(User us)
        {

            var x = us.Email.ToLower();
            us.Email = x;
            db.mongocollection.InsertOne(us);
           
        }

        // cap nhat use
        public void Update(User us)
        {
            db.mongocollection.UpdateOne(
                Builders<User>.Filter.Eq("_id",us.Id),
                Builders<User>.Update
                .Set("PassWord",us.PassWord)
                .Set("HoTenDem",us.HoTenDem)
                .Set("Ten",us.Ten)
                .Set("DiaChi",us.DiaChi)
                .Set("SDT",us.SDT)
                .Set("NgaySinh",us.NgaySinh)
               
                .Set("GioiTinh",us.GioiTinh)
                .Set("XacThucEmail",us.XacThucEmail)
                 .Set("LoaiTaiKhoan", us.LoaiTaiKhoan)
                );
        }

        public void Delete(string Email)
        {
            db.mongocollection.DeleteOne(
                Builders<User>.Filter.Eq("Email",Email)
                );
        }

    }
}