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
            CircleId = "";
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("TypeOfDetails")]
        public string TypeOfDetails { get; set; }

        [BsonElement("Detail")]
        public Dictionary<string,string> Detail { get; set; }

        [BsonElement("AuthorId")]
        public string AuthorId { get; set; }

        [BsonElement("AuthorName")]
        public string AuthorName { get; set; }

        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("CircleId")]
        public string CircleId { get; set; }

        [BsonElement("Comments")]
        public List<Comment> Comments { get; set; }
    }
}
