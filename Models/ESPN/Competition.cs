using System.Collections.Generic; 
namespace FantasyPowersLeague.Models.ESPN{ 

    public class Competition
    {
        public string id { get; set; }
        public string uid { get; set; }
        public string date { get; set; }
        public int attendance { get; set; }
        public Type type { get; set; }
        public bool timeValid { get; set; }
        public bool neutralSite { get; set; }
        public bool conferenceCompetition { get; set; }
        public bool playByPlayAvailable { get; set; }
        public bool recent { get; set; }
        public Venue venue { get; set; }
        public List<Competitor> competitors { get; set; }
        public List<Note> notes { get; set; }
        public Status status { get; set; }
        public List<Broadcast> broadcasts { get; set; }
        public List<Leader> leaders { get; set; }
        public Format format { get; set; }
        public string startDate { get; set; }
        public List<GeoBroadcast> geoBroadcasts { get; set; }
        public List<Headline> headlines { get; set; }
    }

}