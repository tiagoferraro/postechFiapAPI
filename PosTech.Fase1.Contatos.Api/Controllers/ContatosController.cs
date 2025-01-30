using Microsoft.AspNetCore.Mvc;
using PosTech.Fase1.Contatos.Api.Extension;
using PosTech.Fase1.Contatos.Application.DTO;
using PosTech.Fase1.Contatos.Application.Interfaces;

namespace PosTech.Fase1.Contatos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatosController(IContatoService contatoService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Adicionar([FromBody] ContatoDto contatoRequestRequestDto)
        {
            var resultado = await contatoService.Adicionar(contatoRequestRequestDto);
            return resultado.IsSuccess ? Ok(resultado.Data) : BadRequest(resultado.Error);
        }

        [HttpPut]
        public async Task<ActionResult> Atualizar([FromBody] ContatoDto contatoRequestRequestDto)
        {
            var resultado = await contatoService.Atualizar(contatoRequestRequestDto);
            return resultado.IsSuccess ? NoContent() : BadRequest(resultado.Error);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Excluir(Guid id)
        {
            var resultado = await contatoService.Excluir(id);
            return resultado.IsSuccess ? NoContent() : BadRequest(resultado.Error);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContatoDto>>> Listar()
        {
            var resultado = await contatoService.Listar();
            return resultado.IsSuccess ? Ok(resultado.Data) : BadRequest(resultado.Error);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContatoDto>> Obter(Guid id)
        {
            var resultado = await contatoService.Obter(id);
            return resultado.IsSuccess ? Ok(resultado.Data) : BadRequest(resultado.Error);
        }

        [HttpGet("listarComDDD/{ddd}")]
        public async Task<ActionResult<IEnumerable<ContatoDto>>> ListarComDDD(int ddd)
        {
            var resultado = await contatoService.ListarComDdd(ddd);
            return resultado.IsSuccess ? Ok(resultado.Data) : BadRequest(resultado.Error);
        }
    }
}
