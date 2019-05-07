using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAB3_SocialNetwork.Models
{
    public class User
    {
        public User()
        {
            MemberOf = new List<string>();
            BlockedBy = new List<string>();
            Blocks = new List<string>();
            Follows = new List<string>();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Gender")]
        public string Gender { get; set; }

        [BsonElement("Age")]
        public int Age { get; set; }

        [BsonElement("MemberOf")]
        public List<string> MemberOf { get; set; }

        [BsonElement("Blocks")]
        public List<string> Blocks { get; set; }

        [BsonElement("BlockedBy")]
        public List<string> BlockedBy { get; set; }

        [BsonElement("Follows")]
        public List<string> Follows { get; set; }
    }
}
