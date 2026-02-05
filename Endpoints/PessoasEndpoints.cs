using System;
using ControleGastosResidencial.Application.Pessoas;
using ControleGastosResidencial.Application.Pessoas.DTOs;
using ControleGastosResidencial.Common;

namespace ControleGastosResidencial.Endpoints;

public static class PessoasEndpoints
{
    public static void MapPessoasEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/pessoas")
            .RequireAuthorization()
            .WithTags("Pessoas");

        group.MapPost("",
            async (
                CreatePessoaRequest request,
                CreatePessoaHandler handler,
                HttpContext http
            ) =>
            {
                var result = await handler.HandleAsync(request);

                if (!result.IsSuccess)
                    return Results.BadRequest(ApiResponse<PessoaResponse>.Fail(result.Error!)
                );

                return Results.Created(
                    $"/pessoas/{result.Value!.Id}",
                    ApiResponse<PessoaResponse>.Ok(result.Value)
                );
            });

        group.MapGet("",
            async (
                GetPessoasHandler handler
            ) =>
            {
                var result = await handler.HandleAsync();

                if (!result.IsSuccess)
                    return Results.BadRequest(ApiResponse<PessoaResponse>.Fail(result.Error!));

                return Results.Ok(ApiResponse<IEnumerable<PessoaResponse>>.Ok(result.Value!));
            });

        group.MapPut("{id:int}",
            async (
                int id,
                UpdatePessoaRequest request,
                UpdatePessoaHandler handler
            ) =>
            {
                var result = await handler.HandleAsync(id, request);

                if (!result.IsSuccess)
                    return Results.BadRequest(ApiResponse<PessoaResponse>.Fail(result.Error!));

                return Results.Ok(ApiResponse<PessoaResponse>.Ok(result.Value!));
            });

        group.MapGet("{id:int}",
            async (
                int id,
                GetPessoaByIdHandler handler
            ) =>
            {
                var result = await handler.HandleAsync(id);

                if (!result.IsSuccess)
                    return Results.BadRequest(ApiResponse<PessoaResponse>.Fail(result.Error!));

                return Results.Ok(ApiResponse<PessoaResponse>.Ok(result.Value!));
            });

        group.MapDelete("{id:int}",
            async (
                int id,
                DeletePessoaHandler handler
            ) =>
            {
                var result = await handler.HandleAsync(id);

                if (!result.IsSuccess)
                    return Results.BadRequest(ApiResponse<PessoaResponse>.Fail(result.Error!));

                return Results.Ok();
            });
    }
}
