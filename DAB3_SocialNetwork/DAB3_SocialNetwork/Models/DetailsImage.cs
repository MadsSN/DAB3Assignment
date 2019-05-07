using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace DAB3_SocialNetwork.Models
{
    public class DetailsImage
    {
        [BsonElement("PathToImage")]
        public string PathToImage { get; set; }
    }
}
