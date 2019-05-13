using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using DAB3_SocialNetwork.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace DAB3_SocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CircleController : ControllerBase
    {
        private readonly IMongoCollection<Circle> _circles;
        private readonly IMongoCollection<User> _users;

        public CircleController()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("StoreDb");
            _circles = database.GetCollection<Circle>("Circles");
            _users = database.GetCollection<User>("Users");
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<Circle>> Get()
        {
            return _circles.Find(x=>true).ToList();
        }

        /// <summary>
        /// Create a Circle.
        /// </summary>
        /// <param name="circle"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult<Circle> Create([FromBody] Circle circle)
        {
            List<string> userToRemoveFromList = new List<string>();
            //Add to each user
            //Insert to get token
            _circles.InsertOne(circle);

            foreach (var userId in circle.UsersId)
            {
                //If user doesn't exist, throw exception
                try
                {
                    var filter = Builders<User>.Filter.Eq(user=>user.Id,userId);
                    var update = Builders<User>.Update.AddToSet(user=>user.MemberOf, circle.Id);
                    _users.FindOneAndUpdate(user=>user.Id == userId, update);
                }
                catch (Exception e)
                {
                    userToRemoveFromList.Add(userId);
                } 
            }

            //Remove invalid users from collection. Another approach could be to deny all changes if all users doesn't exist. 
            //Then it need to find all first, and then update them. 
            foreach (var userId in userToRemoveFromList)
            {
                circle.UsersId.Remove(userId);
            }

            //Refresh and update
            _circles.FindOneAndReplace(c => c.Id == circle.Id, circle);

            return Ok(circle);
        }

    }
}