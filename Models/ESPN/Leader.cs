using System.Collections.Generic; 
namespace FantasyPowersLeague.Models.ESPN{ 

    public class Leader
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string shortDisplayName { get; set; }
        public string abbreviation { get; set; }
        public List<Leader> leaders { get; set; }
        public string displayValue { get; set; }
        public double value { get; set; }
        public Athlete athlete { get; set; }
        public Team team { get; set; }
    }

}