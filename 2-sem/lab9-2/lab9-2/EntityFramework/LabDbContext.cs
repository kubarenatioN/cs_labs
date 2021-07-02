using System.Data.Entity;

namespace lab9.EntityFramework
{
    public class LabDbContext : DbContext
    {
        public LabDbContext() : base("eflab9Connection")
        {

        }
        //public DbSet<Team> Teams { get; set; }
        //public DbSet<Player> Players { get; set; }
    }
}
