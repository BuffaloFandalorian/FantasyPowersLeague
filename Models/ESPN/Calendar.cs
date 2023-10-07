using System.Collections.Generic; 
namespace FantasyPowersLeague.Models.ESPN{ 

    public class Calendar
    {
        public string label { get; set; }
        public string value { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public List<Entry> entries { get; set; }
    }

}