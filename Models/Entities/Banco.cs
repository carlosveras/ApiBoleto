using System.ComponentModel.DataAnnotations;

namespace ApiBoleto.Models.Entities;

public class Banco : Base
{
    public string? NomeDoBanco { get; set; }

    public int CodigoBanco { get; set; }

    public decimal PercentualJuros { get; set; }
}
