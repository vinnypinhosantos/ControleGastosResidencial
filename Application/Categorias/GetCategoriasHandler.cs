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
        var categorias = new List<CategoriaResponse>();

        if (tipo is null)
        {
            categorias = await _context.Categorias
            .AsNoTracking()
            .Select(c => new CategoriaResponse(
                c.Id,
                c.Descricao,
                (int) c.Finalidade
            ))
            .ToListAsync();
        } else
        {
            if (!Enum.IsDefined(typeof(Finalidade), tipo))
                return Result<IEnumerable<CategoriaResponse>>.Failure("Tipo Inválido");

            var tipoComoFinalidade = (Finalidade) tipo;
            categorias = await _context.Categorias
                .AsNoTracking()
                .Where(c => c.Finalidade == tipoComoFinalidade || c.Finalidade == Finalidade.Ambos)
                .Select(c => new CategoriaResponse(
                    c.Id,
                    c.Descricao,
                    (int) c.Finalidade
                ))
                .ToListAsync();
        }

        return Result<IEnumerable<CategoriaResponse>>.Success(categorias);
    }
}
