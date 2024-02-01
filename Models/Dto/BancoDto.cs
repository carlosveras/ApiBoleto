using System.ComponentModel.DataAnnotations;

namespace ApiBoleto.Models.Dto;

public class BancoDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo requerido")]
    public string? NomeDoBanco { get; set; }

    [Required]
    [Range(1, 99999, ErrorMessage = "O Codigo do Banco deve estar no intervalo de 1 a 99999.")]
    public int CodigoBanco { get; set; }

    [Required]
    [Range(1.0, 99999.0, ErrorMessage = "Os Juros devem estar no intervalo de 1.0 a 99999.0")]
    public decimal PercentualJuros { get; set; }
}
