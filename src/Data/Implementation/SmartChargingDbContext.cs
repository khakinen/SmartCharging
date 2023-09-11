using Microsoft.EntityFrameworkCore;
using SmartCharging.Data.Contract.Models;

namespace Data.Implementation;

public class SmartChargingDbContext : DbContext
{
    public SmartChargingDbContext(DbContextOptions<SmartChargingDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Group> Groups { get; set; }
    public DbSet<ChargeStation> ChargeStations { get; set; }
    public DbSet<Connector> Connectors { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Group>(e =>
        {
            e.HasMany(g => g.ChargeStations).WithOne(c => c.Group);
            e.HasKey(b => b.Id);
        });
            
        modelBuilder.Entity<ChargeStation>(e =>
        {
            e.HasKey(cs => cs.Id);
            e.Property(cs => cs.GroupId).IsRequired();
            
            e.HasOne(cs => cs.Group).WithMany(g => g.ChargeStations);
        });

        modelBuilder.Entity<Connector>(e =>
        {
            e.HasKey(p=>p.Id);
            e.Property(c => c.ChargeStationId).IsRequired();
            e.Property(c => c.ConnectorNumber).IsRequired();
            
            e.HasOne(c => c.ChargeStation).WithMany(cs => cs.Connectors);
        });
    }
}
