using EventMessageWall.Models;
using Microsoft.EntityFrameworkCore;

namespace EventMessageWall.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Message> Messages => Set<Message>();
        public DbSet<Reply> Replies => Set<Reply>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
