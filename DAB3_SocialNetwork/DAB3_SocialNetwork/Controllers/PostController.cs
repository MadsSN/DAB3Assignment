using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAB3_SocialNetwork.Models;
using DAB3_SocialNetwork.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace DAB3_SocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private IMongoCollection<User> _users;
        private IMongoCollection<Post> _posts;
        private IMongoCollection<Circle> _circle;


        public PostController(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("StoreDb"));
            var database = client.GetDatabase("StoreDb");
            _users = database.GetCollection<User>("Users");
            _posts = database.GetCollection<Post>("Posts");
            _circle = database.GetCollection<Circle>("Circles");
        }

        
        [HttpGet]
        public ActionResult<List<Post>> Get()
        {
            try
            {
                return _posts.Find(x => true).ToList();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        
        [Route("[action]")]
        [HttpPost]
        public ActionResult<Post> CreatePost([FromBody] Post post)
        {
            //Currently details are sort of fake.. 
            var user = _users.Find(x => x.Id == post.AuthorId).FirstOrDefault();
            if (user == null)
            {
                return BadRequest("Invalid author ID");
            }
            post.AuthorName = user.Name;

            //Check if circle exist
            var circle = _circle.Find(c => c.Id == post.CircleId);
            if (post.CircleId!= "" && circle == null)
            {
                return BadRequest("Invalid circleId");
            }


            _posts.InsertOne(post);
            return Ok(post);
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult<Comment> CreateComment([FromBody] CommentRequest request)
        {
            var user = _users.Find(x => x.Id == request.Comment.AuthorId).FirstOrDefault();

            if (user == null)
            {
                return BadRequest("Invalid author ID");
            }

            request.Comment.AuthorName = user.Name;

            try
            {
                //Add comment to set
                var updateBlocks = Builders<Post>.Update.AddToSet(post => post.Comments, request.Comment);
                _posts.FindOneAndUpdate(post => post.Id == request.PostId, updateBlocks);
            }
            catch (Exception e)
            {
                return BadRequest("Post does not exist");
            }
            return Ok(request.Comment);
        }



    }
}
