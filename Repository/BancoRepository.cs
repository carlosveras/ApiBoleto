using ApiBoleto.Context;
using ApiBoleto.Models.Dto;
using ApiBoleto.Models.Entities;
using ApiBoleto.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiBoleto.Repository;

public class BancoRepository : BaseRepository, IBancoRepository
{
    private readonly ApiBoletoDataContext _apiBol;

    public BancoRepository (ApiBoletoDataContext apiPag) : base(apiPag)
    {
        _apiBol = apiPag;
    }

    public async Task<IEnumerable<BancoDto>> GetBancosAsync()
    {
        return await _apiBol.Banco
            .Select(x => new BancoDto { Id = x.Id, NomeDoBanco = x.NomeDoBanco, CodigoBanco = x.CodigoBanco, PercentualJuros = x.PercentualJuros })
            .ToListAsync();
    }

    public async Task<Banco> GetBancoByIdAsync(int id)
    {
        return await _apiBol.Banco.Where(x => x.Id == id).FirstOrDefaultAsync();
    }


    public async Task<Banco> AddBanco(Banco banco)
    {
        await _apiBol.Banco.AddAsync(banco);
        await _apiBol.SaveChangesAsync();

        return banco;
    }
}