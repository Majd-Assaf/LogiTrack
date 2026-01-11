using Microsoft.EntityFrameworkCore;
using TransportService.Domain;

namespace TransportService.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<Transport> Transports => Set<Transport>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transport>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.OrderNumber).IsRequired().HasMaxLength(100);
            b.Property(x => x.ReceiverAddress).IsRequired().HasMaxLength(500);
            b.Property(x => x.Status).IsRequired();
            b.Property(x => x.CreatedAt).IsRequired();
        });
    }
}
