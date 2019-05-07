using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAB3_SocialNetwork.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace DAB3_SocialNetwork.Services
{
    public class UserService
    {

        private readonly IMongoCollection<User> _user;

        public UserService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("StoreDb"));
            var database = client.GetDatabase("StoreDb");
            _user = database.GetCollection<User>("Users");
        }

        public List<User> Get()
        {
            return _user.Find(user => true).ToList();
        }

        public User Get(string id)
        {
            return _user.Find<User>(user => user.Id == id).FirstOrDefault();
        }

        public User Create(User user)
        {
            _user.InsertOne(user);
            return user;
        }

        public void Update(string id, User userIn)
        {
            _user.ReplaceOne(user => user.Id == id, userIn);
        }

        public void Remove(User userIn)
        {
            _user.DeleteOne(user => user.Id == userIn.Id);
        }

        public void Remove(string id)
        {
            _user.DeleteOne(user => user.Id == id);
        }
    }
}
