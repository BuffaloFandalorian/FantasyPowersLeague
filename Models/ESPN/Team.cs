using System.Collections.Generic; 
namespace FantasyPowersLeague.Models.ESPN{ 

    public class Team
    {
        public string id { get; set; }
        public string uid { get; set; }
        public string location { get; set; }
        public string name { get; set; }
        public string abbreviation { get; set; }
        public string displayName { get; set; }
        public string shortDisplayName { get; set; }
        public string color { get; set; }
        public string alternateColor { get; set; }
        public bool isActive { get; set; }
        public Venue venue { get; set; }
        public List<Link> links { get; set; }
        public string logo { get; set; }
    }

}