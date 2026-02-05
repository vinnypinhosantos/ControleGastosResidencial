using ControleGastosResidencial.Application.Pessoas;
using ControleGastosResidencial.Data;
using ControleGastosResidencial.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<CreatePessoaHandler>();
builder.Services.AddScoped<UpdatePessoaHandler>();
builder.Services.AddScoped<GetPessoaByIdHandler>();
builder.Services.AddScoped<DeletePessoaHandler>();
builder.Services.AddScoped<GetPessoasHandler>();

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

app.Run();
