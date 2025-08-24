using CadastroPessoas.Context;
using CadastroPessoas.Interfaces;
using CadastroPessoas.Models;
using CadastroPessoas.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adicione CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.EnsureCreated();
        
        if (!await context.Pessoas.AnyAsync())
        {
            var pessoaPadrao = new Pessoa
            {
                Nome = "Usuário Padrão",
                TipoPessoa = (Pessoa.Tipo)0,
                Documento = "37.186.889/0001-53",
                DataNascimento = new DateTime(1990, 1, 1),
                Celular = "(11) 98765-4321",
                Email = "usuario.padrao@exemplo.com",
                Cep = "01001-000",
                Logradouro = "Praça da Sé",
                Numero = "1",
                Bairro = "Sé",
                Cidade = "São Paulo",
                Estado = "SP",
                Complemento = "Lado ímpar",
                Ativo = true,
                DataInclusao = DateTime.Now,
                DataAlteracao = DateTime.Now
            };

            context.Pessoas.Add(pessoaPadrao);
            await context.SaveChangesAsync();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao inicializar o banco de dados com o usuário padrão.");
    }
}

app.Run();
