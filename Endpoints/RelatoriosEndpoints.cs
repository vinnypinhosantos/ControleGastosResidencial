using System;
using ControleGastosResidencial.Application.Pessoas.Queries.GetTotaisPorPessoa;
using ControleGastosResidencial.Common;

namespace ControleGastosResidencial.Endpoints;

public static class RelatoriosEndpoints
{
    public static void MapRelatoriosEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/relatorios")
            .WithTags("Relatorios");
        
        group.MapGet("/totais-por-pessoa", 
            async (
                int? id,
                GetTotaisPorPessoaHandler handler
            ) =>
            {
                var result = await handler.HandleAsync(id);
                return Results.Ok(result);
            });
    }
}
