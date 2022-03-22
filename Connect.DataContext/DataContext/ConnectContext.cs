using Connect.Domain;
using Microsoft.EntityFrameworkCore;

namespace Connect.Infrastructure.DataContext
{
    public class ConnectContext : DbContext
    {
        public ConnectContext(DbContextOptions<ConnectContext> options)
            : base(options)
        {
        }
        public DbSet<Club> Clubs => Set<Club>();
        public DbSet<Event> Events => Set<Event>();
        public DbSet<Registration> Registrations => Set<Registration>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Club>().Property(x => x.Name).HasMaxLength(512);
            modelBuilder.Entity<Club>().Property(x => x.Description).HasMaxLength(10240);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseSqlServer(
                @"Server=localhost;Database=Clubs.Connect;Trusted_Connection=True");
    }
}