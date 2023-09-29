namespace FantasyPowersLeague.Models
{
    public class LoginResultDto
    {
        public string userId;
        public string token { get;set; }
        public long? expiration { get; set; }
        public string refreshToken { get;set; }
    }
}