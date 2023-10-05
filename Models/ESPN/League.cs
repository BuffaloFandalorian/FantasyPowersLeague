using System.Collections.Generic; 
namespace FantasyPowersLeague.Models.ESPN{ 

    public class League
    {
        public string id { get; set; }
        public string uid { get; set; }
        public string name { get; set; }
        public string abbreviation { get; set; }
        public string slug { get; set; }
        public LeagueSeason season { get; set; }
        public List<Logo> logos { get; set; }
        public string calendarType { get; set; }
        public bool calendarIsWhitelist { get; set; }
        public string calendarStartDate { get; set; }
        public string calendarEndDate { get; set; }
        public List<Calendar> calendar { get; set; }
    }

}