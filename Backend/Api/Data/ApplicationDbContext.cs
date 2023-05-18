using Microsoft.EntityFrameworkCore;
using DGIIAPP.API.Models;

namespace DGIIAPP.API.Data;

public class ApplicationDbContext : DbContext {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {

    }

    public DbSet<Contribuyente> Contribuyentes { get; set; }
    public DbSet<ComprobanteFiscal> ComprobantesFiscales { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ComprobanteFiscal>()
            .Property(c => c.Monto)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<ComprobanteFiscal>()
            .Property(c => c.Itbis18)
            .HasColumnType("decimal(18,2)");

        base.OnModelCreating(modelBuilder);
    }
}
