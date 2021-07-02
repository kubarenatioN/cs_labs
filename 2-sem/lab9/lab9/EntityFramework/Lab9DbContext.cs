using System.Data.Entity;

namespace lab9
{
    public class Lab9DbContext : DbContext
    {
        public Lab9DbContext() : base("eflab9Connection")
        {
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
    }
}
