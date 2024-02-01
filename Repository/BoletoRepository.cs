using ApiBoleto.Context;
using ApiBoleto.Models.Dto;
using ApiBoleto.Models.Entities;
using ApiBoleto.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiBoleto.Repository;

public class BoletoRepository : BaseRepository, IBoletoRepository
{
    private readonly ApiBoletoDataContext _apiBol;

    public BoletoRepository(ApiBoletoDataContext apiPag) : base(apiPag)
    {
        _apiBol = apiPag;
    }

    public async Task<IEnumerable<BoletoAdicionarDto>> GetBoletosAsync()
    {
        return await _apiBol.Boletos
            .Select(x => new BoletoAdicionarDto { Id = x.Id, BancoId = x.BancoId, CPF_CNPJ_Beneficiario = x.CPF_CNPJ_Beneficiario, 
                                         NomeBeneficiario = x.NomeBeneficiario, Valor = x.Valor, Vencimento = x.Vencimento })
            .ToListAsync();
    }

    public async Task<Boleto> GetBoletoByIdAsync(int id)
    {
        return await _apiBol.Boletos.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Boleto> AddBoleto(Boleto boleto)
    {
        await _apiBol.Boletos.AddAsync(boleto);
        await _apiBol.SaveChangesAsync();

        return boleto;
    }
}