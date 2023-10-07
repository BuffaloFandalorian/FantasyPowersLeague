using System.Collections.Generic; 
namespace FantasyPowersLeague.Models.ESPN{ 

    public class Logo
    {
        public string href { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string alt { get; set; }
        public List<string> rel { get; set; }
        public string lastUpdated { get; set; }
    }

}