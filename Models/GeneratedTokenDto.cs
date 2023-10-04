using System.Reflection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FantasyPowersLeague.Models
{
    public class GeneratedTokenDto
    {
        public string token { get; set; }
        public Guid jti { get; set; }
        public Boolean isRefresh { get; set; }
        public long expiration { get;set; }
    }
}