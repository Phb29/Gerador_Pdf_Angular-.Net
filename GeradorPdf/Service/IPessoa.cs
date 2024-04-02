using GeradorPdf.Model;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace GeradorPdf.Service
{
    public interface IPessoa
    {
        Task<List<Pessoa>> ObterTodasPessoasAsync(); 
        Task AdicionarPessoaAsync(Pessoa pessoa);
        Task<Pessoa> ObterPessoaPorIdAsync(int id);
        Task AtualizarPessoaAsync(Pessoa pessoa);
        Task DeletarPessoaAsync(int id);
        Task<byte[]> GerarPdfAsync(List<Pessoa> pessoas);
        Task<byte[]> GerarPdfPessoaAsync(int pessoaId);


    }
}
