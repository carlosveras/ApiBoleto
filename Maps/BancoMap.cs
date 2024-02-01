using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApiBoleto.Models.Entities;
using ApiBoleto.Maps;

namespace ApiBoleto.Map;

public class BancoMap : BaseMap<Banco>
{
    public BancoMap() : base("Banco")
    { }
    public override void Configure(EntityTypeBuilder<Banco> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.NomeDoBanco).IsRequired();
        builder.Property(x => x.CodigoBanco).IsRequired();
        builder.Property(x => x.PercentualJuros).IsRequired(); 
    }
}


