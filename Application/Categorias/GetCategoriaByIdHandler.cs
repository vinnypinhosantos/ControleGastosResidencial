using System;
using ControleGastosResidencial.Application.Categorias.DTOs;
using ControleGastosResidencial.Common;
using ControleGastosResidencial.Data;
using Microsoft.EntityFrameworkCore;

namespace ControleGastosResidencial.Application.Categorias;

public class GetCategoriaByIdHandler (AppDbContext _context)
{
    public async Task<Result<CategoriaResponse>> HandleAsync(int id)
    {
        var categoria = await _context.Categorias
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
        
        if (categoria is null)
            return Result<CategoriaResponse>.Failure("Categoria não encontrada.");
        
        return Result<CategoriaResponse>.Success(
            new CategoriaResponse(
                categoria.Id,
                categoria.Descricao,
                (int) categoria.Finalidade
            )
        );
    }
}
