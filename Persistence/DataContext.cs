using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        // Represents table in db called Activities, with properties matching Activity Class
        public DbSet<Activity> Activities { get; set; }
    }
}



