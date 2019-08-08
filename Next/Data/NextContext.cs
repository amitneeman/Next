using Next.Models;
using Microsoft.EntityFrameworkCore;

namespace Next.Data
{
    public class NextContext : DbContext
    {
        public NextContext(DbContextOptions<NextContext> options) : base(options)
        {
        }

        public DbSet<Server> Servers { get; set; }
        public DbSet<User> Users { get; set; }
    }
}