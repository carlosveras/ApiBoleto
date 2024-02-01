using System.ComponentModel.DataAnnotations;

namespace ApiBoleto.Models.Dto
{
    public class BancoAdicionarDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Campo requerido")]
        public string? NomeDoBanco { get; set; }

        public int CodigoBanco { get; set; }

        public decimal PercentualJuros { get; set; }
    }
}
