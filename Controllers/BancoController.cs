using ApiBoleto.Models.Dto;
using ApiBoleto.Models.Entities;
using ApiBoleto.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiBoleto.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BancoController : ControllerBase
{
    private readonly IBancoRepository _repository;
    private readonly IMapper _mapper;

    public BancoController(IBancoRepository banco, IMapper mapper)
    {
        _repository = banco;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var bancos = await _repository.GetBancosAsync();

        return bancos.Any()
                ? Ok(bancos)
                : BadRequest("Banco não encontrado.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var banco = await _repository.GetBancoByIdAsync(id);

        var bancoRetorno = _mapper.Map<BancoDto>(banco);

        return bancoRetorno != null
                ? Ok(bancoRetorno)
                : BadRequest("Banco não encontrado.");
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] BancoAdicionarDto banco)
    {
        if (banco == null) return BadRequest("Dados Inválidos");

        if (banco.Id < 1)
        {
            ModelState.AddModelError(nameof(banco.Id), "O campo Id deve estar no intervalo de 1 a 99999.");
            return BadRequest(ModelState);
        }

        var bancoRetorno = await _repository.GetBancoByIdAsync(banco.Id);

        if (bancoRetorno != null && bancoRetorno.Id == banco.Id)
            return BadRequest("Banco ja incluso");

        if (banco.CodigoBanco < 1 || banco.CodigoBanco > 99999)
        {
            ModelState.AddModelError(nameof(banco.CodigoBanco), "O campo CodigoBanco deve estar no intervalo de 1 a 99999.");
            return BadRequest(ModelState);
        }
        else if (banco.PercentualJuros is < (decimal)1.0 or > (decimal)99999.0)
        {
            ModelState.AddModelError(nameof(banco.PercentualJuros), "O campo PercentualJuros deve estar no intervalo de 0.1 a 99999.0");
            return BadRequest(ModelState);
        }

        var bancoAdicionar = _mapper.Map<Banco>(banco);

        _repository.Add(bancoAdicionar);

        return await _repository.SaveChangesAsync()
            ? Ok("Banco adicionado com sucesso")
            : BadRequest("Erro ao salvar o Banco");
    }
}
