using System.ComponentModel.DataAnnotations;

namespace ApiBoleto.Models.Dto;

public class BoletoAdicionarDto
{

    [Required(ErrorMessage = "O {0} é requirido")]
    public int Id { get; set; }

    [Required(ErrorMessage = "O {0} é requirido")]
    public string? NomePagador { get; set; }

    [Required(ErrorMessage = "O {0} é requirido")]
    public string? CPF_CNPJ_Pagador { get; set; }

    [Required(ErrorMessage = "O {0} é requirido")]
    public string? NomeBeneficiario { get; set; }

    [Required(ErrorMessage = "O {0} é requirido")]
    public string? CPF_CNPJ_Beneficiario { get; set; }

    [Required(ErrorMessage = "O {0} é requirido")]
    public decimal Valor { get; set; }

    [Required(ErrorMessage = "O {0} é requirido")]
    public DateTime Vencimento { get; set; }

    public string? Observacao { get; set; }

    [Required(ErrorMessage = "O {0} é requirido")]
    public int BancoId { get; set; }

}
