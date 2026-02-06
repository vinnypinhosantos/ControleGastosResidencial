using System;
using ControleGastosResidencial.Application.Categorias;
using ControleGastosResidencial.Application.Pessoas;

namespace ControleGastosResidencial.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationHandlers(this IServiceCollection services)
    {
        services.AddScoped<CreatePessoaHandler>();
        services.AddScoped<GetPessoasHandler>();
        services.AddScoped<GetPessoaByIdHandler>();
        services.AddScoped<UpdatePessoaHandler>();
        services.AddScoped<DeletePessoaHandler>();

        services.AddScoped<CreateCategoriaHandler>();
        services.AddScoped<GetCategoriasHandler>();
        services.AddScoped<GetCategoriaByIdHandler>();
        services.AddScoped<UpdateCategoriaHandler>();
        services.AddScoped<DeleteCategoriaHandler>();

        return services;
    }
}
