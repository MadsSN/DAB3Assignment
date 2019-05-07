﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAB3_SocialNetwork.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Linq.Expressions;
using DAB3_SocialNetwork.Services.Api.DataAccessLayer.Repositories;

namespace DAB3_SocialNetwork.Services
{
    public class PostService : GenericService<Post>
    {
        public PostService(IConfiguration config) : base(config)
        {
        }
    }
}
