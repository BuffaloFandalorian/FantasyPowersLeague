using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FantasyPowersLeague.Models
{
    public class RefreshTokenDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? id { get;set; }
        public string jti { get;set; }
        public string user { get; set; }
    }
}