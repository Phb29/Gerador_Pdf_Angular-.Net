using GeradorPdf.Context;
using GeradorPdf.Service;
using Microsoft.EntityFrameworkCore;

namespace GeradorPdf
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Adicionar serviços ao contêiner.
            builder.Services.AddAuthorization();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ContextDb>(options =>
                 options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IPessoa, PessoaService>();

            // Configurar o CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

          
            app.UseRouting();
            app.UseHttpsRedirection();

            // Adicionar o middleware CORS ao pipeline de middleware
            app.UseCors("AllowAllOrigins");

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints?.MapControllers();
            });

            app.Run();
        }
    }
}
