namespace ApiBoleto.Models.Entities;

public class Boleto
{
    public int Id { get; set; }

    public string? NomePagador { get; set; }

    public string? CPF_CNPJ_Pagador { get; set; }

    public string? NomeBeneficiario { get; set; }

    public string? CPF_CNPJ_Beneficiario { get; set; }

    public decimal Valor { get; set; }

    public DateTime Vencimento { get; set; }

    public string? Observacao { get; set; }

    public int BancoId { get; set; }

}
