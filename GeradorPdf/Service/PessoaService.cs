using GeradorPdf.Context;
using GeradorPdf.Model;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.EntityFrameworkCore;

namespace GeradorPdf.Service
{
    public class PessoaService : IPessoa
    {
        private readonly ContextDb _context;

        public PessoaService(ContextDb context)
        {
            _context = context;

        }
        public async Task AdicionarPessoaAsync(Pessoa pessoa)
        {
            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarPessoaAsync(Pessoa pessoa)
        {
            _context.Entry(pessoa).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletarPessoaAsync(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id) ?? throw new InvalidOperationException("Pessoa não encontrada");
            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();
        }


        public async Task<Pessoa> ObterPessoaPorIdAsync(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            return pessoa! == null ? throw new InvalidOperationException("Pessoa não encontrada") : pessoa;
        }

        public async Task<byte[]> GerarPdfAsync(List<Pessoa> pessoas)
        {
            await using MemoryStream stream = new();
            PdfWriter writer = new(stream);
            PdfDocument pdf = new(writer);
            Document document = new(pdf);

            Table table = new(5);

            // Adicionando cabeçalhos à tabela
            table.AddHeaderCell("ID");
            table.AddHeaderCell("Nome");
            table.AddHeaderCell("Email");
            table.AddHeaderCell("Profissão");
            table.AddHeaderCell("Cidade");

            // Adicionando dados à tabela
            foreach (var pessoa in pessoas)
            {
                Cell cellId = new Cell().Add(new Paragraph(pessoa.Id.ToString()));
                Cell cellNome = new Cell().Add(new Paragraph(pessoa.Nome));
                Cell cellEmail = new Cell().Add(new Paragraph(pessoa.Email));
                Cell cellProfissao = new Cell().Add(new Paragraph(pessoa.Profissao));
                Cell cellHabilidade = new Cell().Add(new Paragraph(pessoa.Habilidade));

                // Definindo largura das células
                cellId.SetWidth(UnitValue.CreatePercentValue(10)); // 10% da largura total
                cellNome.SetWidth(UnitValue.CreatePercentValue(30)); // 30% da largura total
                cellEmail.SetWidth(UnitValue.CreatePercentValue(30)); // 30% da largura total
                cellProfissao.SetWidth(UnitValue.CreatePercentValue(15)); // 15% da largura total
                cellHabilidade.SetWidth(UnitValue.CreatePercentValue(15)); // 15% da largura total

                // Adicionando células à tabela
                table.AddCell(cellId);
                table.AddCell(cellNome);
                table.AddCell(cellEmail);
                table.AddCell(cellProfissao);
                table.AddCell(cellHabilidade);
            }

            document.Add(table);

            document.Close();
            return stream.ToArray();
        }


        public Task<List<Pessoa>> ObterTodasPessoasAsync()
        {
            return _context.Pessoas.ToListAsync();
        }

        public async Task<byte[]> GerarPdfPessoaAsync(int pessoaId)
        {
            var pessoa = await ObterPessoaPorIdAsync(pessoaId);

            if (pessoa == null)
            {
                throw new InvalidOperationException("Pessoa não encontrada");
            }

            using MemoryStream stream = new();
            PdfWriter writer = new(stream);
            PdfDocument pdf = new(writer);
            Document document = new(pdf);

            // Definindo margens do documento
            document.SetMargins(50, 50, 50, 50);

            // Estilo do parágrafo
            Style estiloParagrafo = new Style()
                .SetFontColor(ColorConstants.BLACK)
                .SetFontSize(14)
                .SetTextAlignment(TextAlignment.CENTER); // Alinhamento centralizado

            // Criando parágrafo com os dados da pessoa
            Paragraph paragraph = new Paragraph()
                .Add("ID: ").Add(pessoa.Id.ToString()).Add("\n")
                .Add("Nome: ").Add(pessoa.Nome).Add("\n")
                .Add("Email: ").Add(pessoa.Email).Add("\n")
                .Add("Profissão: ").Add(pessoa.Profissao).Add("\n")
                .Add("Cidade: ").Add(pessoa.Habilidade)
                .AddStyle(estiloParagrafo);

            // Espaçamento entre os itens
            paragraph.SetMarginBottom(10);

            // Adicionando borda ao parágrafo
            paragraph.SetBorder(new SolidBorder(ColorConstants.BLACK, 1));

            // Adicionando parágrafo ao documento
            document.Add(paragraph);

            // Centralizando verticalmente o parágrafo
            paragraph.SetHorizontalAlignment(HorizontalAlignment.CENTER);
            paragraph.SetVerticalAlignment(VerticalAlignment.MIDDLE);

            document.Close();
            return stream.ToArray();
        }
    }
}
