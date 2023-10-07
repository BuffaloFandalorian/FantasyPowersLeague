using System.Collections.Generic; 
namespace FantasyPowersLeague.Models.ESPN{ 

    public class Event
    {
        public string id { get; set; }
        public string uid { get; set; }
        public string date { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public EventSeason season { get; set; }
        public Week week { get; set; }
        public List<Competition> competitions { get; set; }
        public List<Link> links { get; set; }
        public Status status { get; set; }
    }

}