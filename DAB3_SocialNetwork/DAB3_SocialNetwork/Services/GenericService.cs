using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAB3_SocialNetwork.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace DAB3_SocialNetwork.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    namespace Api.DataAccessLayer.Repositories
    {
        /// <summary>
        /// This class is a generic repository which contains methods that all repositories often use.
        /// </summary>
        /// <remarks>
        /// See https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
        /// </remarks>
        /// <typeparam name="TEntity"></typeparam>
        public class GenericService<TEntity> where TEntity : class
        {
            public readonly IMongoCollection<TEntity> _entity;


            public GenericService(IConfiguration config)
            {
                var client = new MongoClient(config.GetConnectionString("StoreDb"));
                var database = client.GetDatabase("StoreDb");
                _entity = database.GetCollection<TEntity>(typeof(TEntity)+"s");
            }


            public virtual List<TEntity> AllAsync()
            {
                return Find();
            }

            public virtual List<TEntity> Find(
                Expression<Func<TEntity, bool>> filter = null)
            {
                if (filter == null)
                {
                    return _entity.Find(x => true).ToList();
                }
                return _entity.Find(filter).ToList();
            }

            public virtual TEntity FindFirst(
                Expression<Func<TEntity, bool>> filter)
            {
                return _entity.Find(filter).FirstOrDefault();
            }

            public TEntity Create(TEntity entity)
            {
                _entity.InsertOne(entity);
                return entity;
            }

            public void Update(TEntity postIn, Expression<Func<TEntity, bool>> filter)
            {
                _entity.ReplaceOne(filter, postIn);
            }


        }
    }

}
