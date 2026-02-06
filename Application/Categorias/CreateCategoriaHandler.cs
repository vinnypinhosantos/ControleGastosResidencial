using System;
using ControleGastosResidencial.Application.Categorias.DTOs;
using ControleGastosResidencial.Common;
using ControleGastosResidencial.Data;
using ControleGastosResidencial.Models;

namespace ControleGastosResidencial.Application.Categorias;

public class CreateCategoriaHandler (AppDbContext _context)
{
    public async Task<Result<CategoriaResponse>> HandleAsync(
        CreateCategoriaRequest request
    )
    {
        var categoriaResult = Categoria.Criar(request.Descricao, request.Finalidade);

        if (!categoriaResult.IsSuccess)
            return Result<CategoriaResponse>.Failure(categoriaResult.Error!);
        
        _context.Add(categoriaResult.Value!);
        await _context.SaveChangesAsync();

        var categoria = categoriaResult.Value!;

        return Result<CategoriaResponse>.Success(
            new CategoriaResponse(
                categoria.Id,
                categoria.Descricao,
                (int) categoria.Finalidade
            )
        );
    }
}
