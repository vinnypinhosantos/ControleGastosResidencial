using System;
using ControleGastosResidencial.Application.Categorias.DTOs;
using ControleGastosResidencial.Common;
using ControleGastosResidencial.Data;
using ControleGastosResidencial.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastosResidencial.Application.Categorias;

public class GetCategoriasHandler (AppDbContext _context)
{
    public async Task<Result<IEnumerable<CategoriaResponse>>> HandleAsync(int? tipo)
    {
        IQueryable<Categoria> query = _context.Categorias.AsNoTracking();

        if (tipo is not null)
        {
            if (!Enum.IsDefined(typeof(Finalidade), tipo))
                return Result<IEnumerable<CategoriaResponse>>.Failure("Tipo Inválido");
            
            var finalidade = (Finalidade) tipo;

            query = query.Where(c =>
                c.Finalidade == finalidade ||
                c.Finalidade == Finalidade.Ambos
            );
        }
        
        var categorias = await query
            .Select(c => new CategoriaResponse(
                c.Id,
                c.Descricao,
                (int)c.Finalidade
            ))
            .ToListAsync();

        return Result<IEnumerable<CategoriaResponse>>.Success(categorias);
    }
}
