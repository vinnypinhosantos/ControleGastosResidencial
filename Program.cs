using ControleGastosResidencial.Application.Categorias;
using ControleGastosResidencial.Application.Pessoas;
using ControleGastosResidencial.Data;
using ControleGastosResidencial.Endpoints;
using ControleGastosResidencial.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationHandlers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "GastosAPI V1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.MapPessoasEndpoints();
app.MapCategoriasEndpoints();
app.MapTransacoesEndpoint();

app.Run();
