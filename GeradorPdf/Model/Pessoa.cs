using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeradorPdf.Model
{
  
    public class Pessoa
    {
        [Key]
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }  
        public string? Profissao { get; set; }
        public string? Habilidade { get; set; }
    }
}
