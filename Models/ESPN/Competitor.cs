using System.Collections.Generic; 
namespace FantasyPowersLeague.Models.ESPN{ 

    public class Competitor
    {
        public string id { get; set; }
        public string uid { get; set; }
        public string type { get; set; }
        public int order { get; set; }
        public string homeAway { get; set; }
        public bool winner { get; set; }
        public Team team { get; set; }
        public string score { get; set; }
        public List<Linescore> linescores { get; set; }
        public List<object> statistics { get; set; }
        public List<Record> records { get; set; }
    }

}