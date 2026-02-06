using System;
using ControleGastosResidencial.Application.Categorias.DTOs;
using ControleGastosResidencial.Common;
using ControleGastosResidencial.Data;
using Microsoft.EntityFrameworkCore;

namespace ControleGastosResidencial.Application.Categorias;

public class GetCategoriasHandler (AppDbContext _context)
{
    public async Task<Result<IEnumerable<CategoriaResponse>>> HandleAsync()
    {
        var categorias = await _context.Categorias
            .AsNoTracking()
            .Select(c => new CategoriaResponse(
                c.Id,
                c.Descricao,
                (int) c.Finalidade
            ))
            .ToListAsync();

        return Result<IEnumerable<CategoriaResponse>>.Success(categorias);
    }
}
