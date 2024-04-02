using GeradorPdf.Model;
using GeradorPdf.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeradorPdf.Controller
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {

        private readonly IPessoa _pessoa;

        public PessoasController(IPessoa pessoa)
        {
            _pessoa = pessoa;
        }
        [HttpGet]
        public async Task<ActionResult<List<Pessoa>>> GetTodasPessoas()
        {
            var pessoas = await _pessoa.ObterTodasPessoasAsync();
            return Ok(pessoas);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoa>> GetPessoaPorId(int id)
        {
            try
            {
                var pessoa = await _pessoa.ObterPessoaPorIdAsync(id);
                return Ok(pessoa);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverPessoaPorId(int id)
        {
            try
            {
                await _pessoa.DeletarPessoaAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AdicionarPessoa(Pessoa pessoa)
        {
            await _pessoa.AdicionarPessoaAsync(pessoa);
            return CreatedAtAction(nameof(GetPessoaPorId), new { id = pessoa.Id }, pessoa);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualziarPessoa(int id, Pessoa pessoa)
        {
            if (id != pessoa.Id)
            {
                return BadRequest("O ID da pessoa no corpo da requisição não coincide com o ID fornecido na URL.");
            }
            try
            {
                await _pessoa.AtualizarPessoaAsync(pessoa);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex);
            }
        }
        [HttpGet("baixar-pdf")]
        public async Task<IActionResult> BaixarPdf()
        {
            var pessoas = await _pessoa.ObterTodasPessoasAsync();
            var pdfBytes = await _pessoa.GerarPdfAsync(pessoas);

            return File(pdfBytes, "application/pdf", "pessoas.pdf");
        }
        [HttpGet("{id}/gerar-pdf")]
        public async Task<IActionResult> GerarPdfPessoa(int id)
        {
            try
            {
                var pdfBytes = await _pessoa.GerarPdfPessoaAsync(id);
                return File(pdfBytes, "application/pdf", "informacoes_pessoa.pdf");
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }

        }
    }
}
