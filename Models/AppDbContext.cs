using Microsoft.EntityFrameworkCore;

namespace communityApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } // This maps to the Users table
        public DbSet<contact> Contacts { get; set; }

    }
}
