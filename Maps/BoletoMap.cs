using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApiBoleto.Models.Entities;

namespace ApiBoleto.Map;

public class BoletoMap : IEntityTypeConfiguration<Boleto>
{
    public void Configure(EntityTypeBuilder<Boleto> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.NomePagador).IsRequired();
        builder.Property(x => x.NomeBeneficiario).IsRequired();
        builder.Property(x => x.CPF_CNPJ_Pagador);
        builder.Property(x => x.CPF_CNPJ_Beneficiario);
        builder.Property(x => x.Valor).IsRequired();
        builder.Property(x => x.BancoId).IsRequired();
        builder.Property(x => x.Vencimento).IsRequired();
        builder.Property(x => x.Observacao);
    }
}
