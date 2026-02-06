using System;
using Azure;
using ControleGastosResidencial.Application.Categorias;
using ControleGastosResidencial.Application.Categorias.DTOs;
using ControleGastosResidencial.Application.Pessoas;
using ControleGastosResidencial.Common;

namespace ControleGastosResidencial.Endpoints;

public static class CategoriasEndpoints
{
    public static void MapCategoriasEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/categorias")
            .WithTags("Categorias");
        
        group.MapPost("", 
            async (
                CreateCategoriaRequest request,
                CreateCategoriaHandler handler,
                HttpContext http
            ) =>
            {
                var result = await handler.HandleAsync(request);

                if (!result.IsSuccess)
                    return Results.BadRequest(ApiResponse<CategoriaResponse>.Fail(result.Error!));

                return Results.Created(
                    $"/categorias/{result.Value!.Id}",
                    ApiResponse<CategoriaResponse>.Ok(result.Value)
                );
            }
        );
        group.MapGet("",
            async (
                int? tipo,
                GetCategoriasHandler handler
            ) =>
            {
                var result = await handler.HandleAsync(tipo);

                if (!result.IsSuccess)
                    return Results.BadRequest(ApiResponse<CategoriaResponse>.Fail(result.Error!));

                return Results.Ok(ApiResponse<IEnumerable<CategoriaResponse>>.Ok(result.Value!));
            }
        );
        group.MapPut("{id:int}", 
            async (
                int id,
                UpdateCategoriaRequest request,
                UpdateCategoriaHandler handler
            ) =>
            {
                var result = await handler.HandleAsync(id, request);

                if (!result.IsSuccess)
                    return Results.BadRequest(ApiResponse<CategoriaResponse>.Fail(result.Error!));
                
                return Results.Ok(ApiResponse<CategoriaResponse>.Ok(result.Value!));
            }
        );
        group.MapGet("{id:int}",
            async (
                int id,
                GetCategoriaByIdHandler handler
            ) =>
            {
                var result = await handler.HandleAsync(id);

                if (!result.IsSuccess)
                    return Results.BadRequest(ApiResponse<CategoriaResponse>.Fail(result.Error!));
                
                return Results.Ok(ApiResponse<CategoriaResponse>.Ok(result.Value!));
            }
        );     
        group.MapDelete("{id:int}",
            async (
                int id,
                DeleteCategoriaHandler handler
            ) =>
            {
                var result = await handler.HandleAsync(id);

                if (!result.IsSuccess)
                    return Results.BadRequest(ApiResponse<CategoriaResponse>.Fail(result.Error!));
                
                return Results.Ok();
            }
        );
    }
}
