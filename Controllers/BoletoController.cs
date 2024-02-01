using ApiBoleto.Models.Dto;
using ApiBoleto.Models.Entities;
using ApiBoleto.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiBoleto.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BoletoController : ControllerBase
{
    private readonly IBoletoRepository _repository;
    private readonly IBancoRepository _banco;
    private readonly IMapper _mapper;

    public BoletoController(IBoletoRepository boleto, IBancoRepository banco, IMapper mapper)
    {
        _repository = boleto;
        _banco = banco;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var boletos = await _repository.GetBoletosAsync();

        return boletos.Any()
                ? Ok(boletos)
                : BadRequest("Boleto não encontrado.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var boleto = await _repository.GetBoletoByIdAsync(id);

        if (boleto == null) return BadRequest("Boleto não encontrado.");

        DateTime DataBoleto = boleto.Vencimento;
        if (DateTime.Now.Date >= DataBoleto.Date)
        {
            Banco dadosBanco = await _banco.GetBancoByIdAsync(boleto.BancoId);
            decimal ValorCorrigidoJuros = boleto.Valor * (dadosBanco.PercentualJuros / 100);
            boleto.Valor += ValorCorrigidoJuros;
        }

        var boletoRetorno = _mapper.Map<BoletoDto>(boleto);

        return Ok (boletoRetorno);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] BoletoAdicionarDto boleto)
    {
        if (boleto == null) return BadRequest("Dados Inválidos");

        if (boleto.Id < 1)
        {
            ModelState.AddModelError(nameof(boleto.Id), "O campo Id deve estar no intervalo de 1 a 99999.");
            return BadRequest(ModelState);
        }

        var boletoRetorno = await _repository.GetBoletoByIdAsync(boleto.Id);

        if (boletoRetorno != null && boletoRetorno.Id == boleto.Id)
        {
            return BadRequest("Boleto ja incluso");
        }

        if (boleto.BancoId < 1 || boleto.BancoId > 99999)
        {
            ModelState.AddModelError(nameof(boleto.BancoId), "O campo BancoId deve estar no intervalo de 1 a 99999.");
            return BadRequest(ModelState);
        }
        else if (boleto.Valor is < (decimal)1.0 or > (decimal)99999.0)
        {
            ModelState.AddModelError(nameof(boleto.Valor), "O campo Valor deve estar no intervalo de 0.1 a 99999.0");
            return BadRequest(ModelState);
        }
        else if (string.IsNullOrEmpty(boleto.NomePagador))
        {
            ModelState.AddModelError(nameof(boleto.NomePagador), "O campo Nome do Pagador deve ser informado");
            return BadRequest(ModelState);
        }
        else if (string.IsNullOrEmpty(boleto.CPF_CNPJ_Pagador))
        {
            ModelState.AddModelError(nameof(boleto.CPF_CNPJ_Pagador), "O campo CPF_CNPJ_Pagador deve ser informado");
            return BadRequest(ModelState);
        }
        else if (string.IsNullOrEmpty(boleto.CPF_CNPJ_Beneficiario))
        {
            ModelState.AddModelError(nameof(boleto.CPF_CNPJ_Beneficiario), "O campo CPF_CNPJ_Beneficiario deve ser informado");
            return BadRequest(ModelState);
        }
        else if (boleto.Vencimento == null || boleto.Vencimento == DateTime.MinValue)
        {
            ModelState.AddModelError(nameof(boleto.Vencimento), "O campo Vencimento deve ser informado");
            return BadRequest(ModelState);
        }

        var boletoAdicionar = _mapper.Map<Boleto>(boleto);

        _repository.Add(boletoAdicionar);

        return await _repository.SaveChangesAsync()
            ? Ok("Boleto adicionado com sucesso")
            : BadRequest("Erro ao salvar o Boleto");
    }
}
