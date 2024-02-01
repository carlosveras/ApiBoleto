using ApiBoleto.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiBoleto.Context;

public class ApiBoletoDataContext : DbContext
{
    public ApiBoletoDataContext(DbContextOptions<ApiBoletoDataContext> options) : base(options)
    {
    }

    public DbSet<Banco> Banco { get; set; }

    public DbSet<Boleto> Boletos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}


