using System;
using ControleGastosResidencial.Application.Categorias.DTOs;
using ControleGastosResidencial.Common;
using ControleGastosResidencial.Data;

namespace ControleGastosResidencial.Application.Categorias;

public class UpdateCategoriaHandler (AppDbContext _context)
{
    public async Task<Result<CategoriaResponse>> HandleAsync(
        int id,
        UpdateCategoriaRequest request
    )
    {
        var categoria = await _context.Categorias.FindAsync(id);

        if (categoria is null)
            return Result<CategoriaResponse>.Failure("Categoria não encontrada");
        
        var descricaoResult = categoria.AtualizarDescricao(request.Descricao);
        if (!descricaoResult.IsSuccess)
            return Result<CategoriaResponse>.Failure(descricaoResult.Error);

        var finalidadeResult = categoria.AtualizarFinalidade(request.Finalidade);
        if (!descricaoResult.IsSuccess)
            return Result<CategoriaResponse>.Failure(finalidadeResult.Error);
        
        await _context.SaveChangesAsync();

        return Result<CategoriaResponse>.Success(
            new CategoriaResponse(
                categoria.Id,
                categoria.Descricao,
                (int) categoria.Finalidade
            )
        );
    }
}
