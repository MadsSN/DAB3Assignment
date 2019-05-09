using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace DAB3_SocialNetwork.Models
{
    /// <summary>
    /// Inspiration from: https://www.tutorialdocs.com/article/webapi-data-binding.html
    /// </summary>
    [JsonConverter(typeof(DetailJsonConverter))]
    [BsonDiscriminator(Required = true)]
    [BsonKnownTypes(typeof(DetailImage), typeof(DetailText))]
    public class Detail
    {

    }
}
