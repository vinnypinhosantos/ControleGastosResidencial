using System;
using System.Reflection.Metadata;
using ControleGastosResidencial.Application.Transacoes;
using ControleGastosResidencial.Application.Transacoes.DTOs;
using ControleGastosResidencial.Common;

namespace ControleGastosResidencial.Endpoints;

public static class TransacoesEndpoints
{
    public static void MapTransacoesEndpoint(this WebApplication app)
    {
        var group = app.MapGroup("/transacoes")
            .WithTags("Transacoes");
        
        group.MapPost("", 
            async (
                CreateTransacaoRequest request,
                CreateTransacaoHandler handler,
                HttpContext http
            ) =>
            {
                var result = await handler.HandleAsync(request);

                if (!result.IsSuccess)
                    return Results.BadRequest(ApiResponse<TransacaoResponse>.Fail(result.Error!));
                
                return Results.Created(
                    $"/transacoes/{result.Value!.Id}",
                    ApiResponse<TransacaoResponse>.Ok(result.Value)
                );
            }
        );
        group.MapGet("",
            async (
                int? tipo,
                GetTransacoesHandler handler
            ) =>
            {
                var result = await handler.HandleAsync(tipo);

                if (!result.IsSuccess)
                    return Results.BadRequest(ApiResponse<TransacaoResponse>.Fail(result.Error!));
                
                return Results.Ok(ApiResponse<IEnumerable<TransacaoResponse>>.Ok(result.Value!));
            }
        );
        group.MapGet("{id:int}", 
            async (
                int id,
                GetTransacaoByIdHandler handler
            ) =>
            {
                var result = await handler.HandleAsync(id);

                if (!result.IsSuccess)
                    return Results.BadRequest(ApiResponse<TransacaoResponse>.Fail(result.Error!));

                return Results.Ok(ApiResponse<TransacaoResponse>.Ok(result.Value!));   
            }
        );
        group.MapPut("{id:int}",
            async (
                int id,
                UpdateTransacaoRequest request,
                UpdateTransacaoHandler handler
            ) =>
            {
                var result = await handler.HandleAsync(id, request);

                if (!result.IsSuccess)
                    return Results.BadRequest(ApiResponse<TransacaoResponse>.Fail(result.Error!));
                
                return Results.Ok(ApiResponse<TransacaoResponse>.Ok(result.Value!));
            }
        );
        group.MapDelete("{id:int}", 
            async (
                int id,
                DeleteTransacaoHandler handler
            ) =>
            {
                var result = await handler.HandleAsync(id);

                if (!result.IsSuccess)
                    return Results.BadRequest(ApiResponse<TransacaoResponse>.Fail(result.Error!));
                
                return Results.Ok();
            }
        );
    }
}
