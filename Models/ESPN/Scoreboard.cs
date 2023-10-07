using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace FantasyPowersLeague.Models.ESPN{ 

    public class Scoreboard
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? id { get;set; }
        public List<League> leagues { get; set; }
        public List<Event> events { get; set; }
        public DateTime lastSync { get;set; }
        public bool isArchived { get;set; }
        public string year { get;set; }
    }

}