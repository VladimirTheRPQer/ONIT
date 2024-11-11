
using Microsoft.EntityFrameworkCore;

namespace WebONIT.Data
{
    public class OnitDbContext : DbContext
    {

        public OnitDbContext(DbContextOptions<OnitDbContext> options)
      : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }
        public DbSet<Student> Students { get; set; }
    }
}
