using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FantasyPowersLeague.Models
{
    public class UserDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? id { get;set; }
        public string username { get;set; }
    }
}