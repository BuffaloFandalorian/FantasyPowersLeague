using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FantasyPowersLeague.Models
{
    public class LeagueDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? id { get;set; }
        public int leagueType { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string? accessCode { get; set; }

    }
}