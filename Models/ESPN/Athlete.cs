using System.Collections.Generic; 
namespace FantasyPowersLeague.Models.ESPN{ 

    public class Athlete
    {
        public string id { get; set; }
        public string fullName { get; set; }
        public string displayName { get; set; }
        public string shortName { get; set; }
        public List<Link> links { get; set; }
        public string headshot { get; set; }
        public string jersey { get; set; }
        public Position position { get; set; }
        public Team team { get; set; }
        public bool active { get; set; }
    }

}