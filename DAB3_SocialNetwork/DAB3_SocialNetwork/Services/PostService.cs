using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAB3_SocialNetwork.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Linq.Expressions;
namespace DAB3_SocialNetwork.Services
{
    public class PostService
    {

        private readonly IMongoCollection<Post> _post;

        public PostService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("StoreDb"));
            var database = client.GetDatabase("StoreDb");
            _post = database.GetCollection<Post>("Posts");
        }

        public List<Post> Get()
        {
            return _post.Find(post => true).ToList();
        }

        public Post Get(string id)
        {
            return _post.Find<Post>(post => post.Id == id).FirstOrDefault();
        }

        public List<Post> Find(Expression<Func<Post, bool>> filter)
        {
            return _post.Find<Post>(filter).ToList();
        }

        public Post Create(Post post)
        {
            _post.InsertOne(post);
            return post;
        }

        public void Update(string id, Post postIn)
        {
            
            _post.ReplaceOne(post => post.Id == id, postIn);
        }

        public void Remove(Post postIn)
        {
            _post.DeleteOne(post => post.Id == postIn.Id);
        }

        public void Remove(string id)
        {
            _post.DeleteOne(post => post.Id == id);
        }
    }
}
