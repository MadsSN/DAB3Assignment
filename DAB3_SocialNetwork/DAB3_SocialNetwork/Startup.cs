using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAB3_SocialNetwork.Controllers;
using DAB3_SocialNetwork.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DAB3_SocialNetwork
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var serviceProvider = services.BuildServiceProvider();
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("StoreDb");
            var _users = database.GetCollection<User>("Users");
            var _posts = database.GetCollection<Post>("Posts");

            UserController userController = new UserController();

            var users = _users.Find(x => true).ToList();
            if (users.Count != 0)
            {
                return;
            }

            //Create users
            userController.Create(new User()
            {
                Age = 23,
                Gender = "Mand",
                Name = "Mads"
            });

            userController.Create(new User()
            {
                Age = 23,
                Gender = "Mand",
                Name = "Frank"
            });
             users = _users.Find(x => true).ToList();
            var user1 = users.First(x => x.Name == "Mads");
            var user2 = users.First(x => x.Name == "Frank");
            //Mads follow Frank
            userController.Follow(new FollowRequest()
            {
                userFollowing = user1.Id,
                userToFollow = user2.Id
            });


            userController.Create(new User()
            {
                Age = 23,
                Gender = "Mand",
                Name = "Michael"
            });

            users = _users.Find(x => true).ToList();
            var user3ToBeBlocked = users.First(x => x.Name == "Michael");

            //Both user 1 and 2 block Michael. Michael should only be able to see his own posts
            userController.Block(new BlockRequest()
            {
                userToBlockId = user3ToBeBlocked.Id,
                userBlockingId = user1.Id
            });

            userController.Block(new BlockRequest()
            {
                userToBlockId = user3ToBeBlocked.Id,
                userBlockingId = user2.Id
            });

            //Even though they are in a circle together

            var circleController = new CircleController();
            circleController.Create(new Circle()
            {
                Name = "Circle of Frank, Michael and Mads.",
                UsersId = new List<string> {user1.Id, user2.Id, user3ToBeBlocked.Id}
            });

            var postController = new PostController();
            //Create public post
            postController.CreatePost(new Post()
            {
                AuthorId = user1.Id,
                AuthorName = user1.Name,
                CircleId = "", // Public
                Detail = new Dictionary<string, string>() {["Text"] = "This is Mads's public post"},
                TypeOfDetails = "Text"
            });

            var publicPostUser1 = _posts.Find(x=>true).ToList().FirstOrDefault(x=>x.AuthorId == user1.Id);


            var publicPostUser2 = postController.CreatePost(new Post()
            {
                AuthorId = user2.Id,
                AuthorName = user2.Name,
                CircleId = "", // Public
                Detail = new Dictionary<string, string>() { ["Image"] = "This is Frank's public Image" },
                TypeOfDetails = "Image"
            }).Value;

            var publicPostUser3 = postController.CreatePost(new Post()
            {
                AuthorId = user3ToBeBlocked.Id,
                AuthorName = user3ToBeBlocked.Name,
                CircleId = "", // Public
                Detail = new Dictionary<string, string>() { ["Image"] = "This is Michaels's public Image" },
                TypeOfDetails = "Image"
            }).Value;

            //Comment on first post
            postController.CreateComment(new CommentRequest()
            {
                Comment = new Comment()
                {
                    AuthorId = user1.Id,
                    AuthorName = user1.Name,
                    Text = "Mads has commented on his own post"
                },
                PostId = publicPostUser1.Id
            });

            //Comment on first post from Frank
            postController.CreateComment(new CommentRequest()
            {
                Comment = new Comment()
                {
                    AuthorId = user2.Id,
                    AuthorName = user2.Name,
                    Text = "Frank has commented on Mads'spost"
                },
                PostId = publicPostUser1.Id
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
