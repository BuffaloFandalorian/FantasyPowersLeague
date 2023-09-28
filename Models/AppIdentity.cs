namespace FantasyPowersLeague.Models
{
    public class AppIdentity
    {
        public string Credential { get;set; }
        public string SessionId { get; set; }
        public bool TokenIsValid { get;set; }
    }
}