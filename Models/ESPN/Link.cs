using System.Collections.Generic; 
namespace FantasyPowersLeague.Models.ESPN{ 

    public class Link
    {
        public List<string> rel { get; set; }
        public string href { get; set; }
        public string text { get; set; }
        public bool isExternal { get; set; }
        public bool isPremium { get; set; }
        public string language { get; set; }
        public string shortText { get; set; }
    }

}