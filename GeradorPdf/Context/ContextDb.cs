using GeradorPdf.Model;
using Microsoft.EntityFrameworkCore;

namespace GeradorPdf.Context
{
    public class ContextDb : DbContext
    {
        public ContextDb(DbContextOptions<ContextDb> options) : base(options)
        {
        }
        public DbSet<Pessoa> Pessoas { get; set; }
    }
}
