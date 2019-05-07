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
        public Post()
        {
            Comments = new List<Comment>();
            CreatedAt = DateTime.Now;
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("TypeOfDetails")]
        public string Type { get; set; }

        [BsonElement("Detail")]
        public Detail Detail { get; set; }

        [BsonElement("Author")]
        public string Author { get; set; }

        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("CircleName")]
        public string Blocks { get; set; }

        [BsonElement("Comments")]
        public List<Comment> Comments { get; set; }
    }
}
