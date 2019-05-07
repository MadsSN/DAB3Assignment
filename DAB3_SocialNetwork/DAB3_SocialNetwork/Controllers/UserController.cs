using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAB3_SocialNetwork.Models;
using DAB3_SocialNetwork.Services;
using Microsoft.AspNetCore.Mvc;

namespace DAB3_SocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly PostService _postService;

        public UserController(UserService userService, PostService postService)
        {
            _userService = userService;
            _postService = postService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            _userService.Create(new User()
            {
                Age = 1
            });
            return _userService.Find();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public ActionResult<User> Feed(string id)
        {
            var userReturned = _userService.FindFirst(user=>user.Id == id);
            return userReturned;
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
