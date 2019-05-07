using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAB3_SocialNetwork.Models;
using DAB3_SocialNetwork.Services.Api.DataAccessLayer.Repositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace DAB3_SocialNetwork.Services
{
    public class UserService : GenericService<User>
    {
        public UserService(IConfiguration config) : base(config)
        {

        }
    }
}
