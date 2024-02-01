using ApiBoleto.Models.Dto;
using ApiBoleto.Models.Entities;

namespace ApiBoleto.Repository.Interfaces;

public interface IBoletoRepository : IBaseRepository
{
    Task<IEnumerable<BoletoAdicionarDto>> GetBoletosAsync();
    Task<Boleto> GetBoletoByIdAsync(int id);
    Task<Boleto> AddBoleto(Boleto boleto);

}
