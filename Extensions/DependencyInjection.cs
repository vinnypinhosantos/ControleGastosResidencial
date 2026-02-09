using System;
using ControleGastosResidencial.Application.Categorias;
using ControleGastosResidencial.Application.Pessoas;
using ControleGastosResidencial.Application.Pessoas.Queries.GetTotaisPorPessoa;
using ControleGastosResidencial.Application.Transacoes;

namespace ControleGastosResidencial.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationHandlers(this IServiceCollection services)
    {
        services.AddScoped<GetPessoasHandler>();
        services.AddScoped<GetPessoaByIdHandler>();
        services.AddScoped<CreatePessoaHandler>();
        services.AddScoped<UpdatePessoaHandler>();
        services.AddScoped<DeletePessoaHandler>();

        services.AddScoped<GetCategoriasHandler>();
        services.AddScoped<GetCategoriaByIdHandler>();
        services.AddScoped<CreateCategoriaHandler>();
        services.AddScoped<UpdateCategoriaHandler>();
        services.AddScoped<DeleteCategoriaHandler>();

        services.AddScoped<GetTransacoesHandler>();
        services.AddScoped<GetTransacaoByIdHandler>();
        services.AddScoped<CreateTransacaoHandler>();
        services.AddScoped<UpdateTransacaoHandler>();
        services.AddScoped<DeleteTransacaoHandler>();

        services.AddScoped<GetTotaisPorPessoaHandler>();

        return services;
    }
}
