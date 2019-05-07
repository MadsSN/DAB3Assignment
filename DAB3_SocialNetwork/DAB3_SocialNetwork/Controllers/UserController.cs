using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAB3_SocialNetwork.Models;
using DAB3_SocialNetwork.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace DAB3_SocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IMongoCollection<User> _users;
        private IMongoCollection<Post> _posts;

        public UserController(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("StoreDb"));
            var database = client.GetDatabase("StoreDb");
            _users = database.GetCollection<User>("Users");
            _posts = database.GetCollection<Post>("Posts");
        }

        // GET api/values
        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            return _users.Find(user=>true).ToList();
        }

        [HttpGet]
        [Route("[action]")]
        public ActionResult<List<Post>> Feed([FromBody] FeedRequest request)
        {
            var owner = _users.Find(userFilter => userFilter.Id == request.OwnerId).FirstOrDefault();
            var viewer = _users.Find(userFilter => userFilter.Id == request.ViewerId).FirstOrDefault();
            if (viewer == null)
            {
                return BadRequest("Invalid Viewer ID");
            }
            if(owner == null)
            {
                return BadRequest("Invalid owner ID");
            }
            //Post where owner is author. 
            //CircleId == "" if public or circleName is contained in MemberOf of viewer
            //Limit to first 10
            var posts = _posts.Find(post =>
                post.AuthorId == owner.Id && 
                viewer.MemberOf.Contains(post.CircleId))
                .Limit(10).ToList();

            //Limit of comments maybe?

            return Ok(posts);
        }

        [HttpPut]
        [Route("[action]")]
        public ActionResult<User> Block([FromBody] BlockRequest request)
        {
            try
            {
                //The own user
                var updateBlocks = Builders<User>.Update.AddToSet(user => user.Blocks, request.userToBlockId);
                _users.FindOneAndUpdate(user => user.Id == request.userBlockingId, updateBlocks);

                //The one to block
                var updateBlocked = Builders<User>.Update.AddToSet(user => user.BlockedBy, request.userBlockingId);
                _users.FindOneAndUpdate(user => user.Id == request.userToBlockId, updateBlocked);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"No user with given id to block/blocking {e.Message}");
            }
        }

        // POST api/values
        [HttpPost]
        [Route("[action]")]
        public ActionResult<User> Create([FromBody] User user)
        {
            _users.InsertOne(user);
            return Ok(user);
        }
    }
}
