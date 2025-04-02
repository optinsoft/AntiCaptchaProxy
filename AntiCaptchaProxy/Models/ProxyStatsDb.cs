using Microsoft.EntityFrameworkCore;

namespace AntiCaptchaProxy.Models
{
    public class ProxyStatsDb : DbContext
    {
        public ProxyStatsDb(DbContextOptions options) : base(options) { }
        public DbSet<ProxyStats> AntiCaptchaStats { get; set; } = null!;
    }
}
