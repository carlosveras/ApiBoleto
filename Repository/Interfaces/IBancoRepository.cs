using ApiBoleto.Models.Dto;
using ApiBoleto.Models.Entities;

namespace ApiBoleto.Repository.Interfaces;

public interface IBancoRepository : IBaseRepository
{
    Task<IEnumerable<BancoDto>> GetBancosAsync();
    Task<Banco> GetBancoByIdAsync(int id);
    Task<Banco> AddBanco(Banco banco);

}
