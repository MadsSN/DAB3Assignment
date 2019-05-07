using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAB3_SocialNetwork.Models
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Type")]
        public string Type { get; set; }

        [BsonElement("Details")]
        public Details Details { get; set; }

        [BsonElement("Author")]
        public string Author { get; set; }

        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("CircleName")]
        public string Blocks { get; set; }

        [BsonElement("Comments")]
        public List<Comment> Comments { get; set; }

        [BsonElement("Follows")]
        public List<string> Follows { get; set; }
    }
}
