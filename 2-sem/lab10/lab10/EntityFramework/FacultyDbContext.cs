using System.Data.Entity;

namespace lab10
{
    public class FacultyDbContext : DbContext
    {
        public FacultyDbContext() : base("lab10dbConnection")
        {

        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Spec> Specs { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
