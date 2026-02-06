using System;
using ControleGastosResidencial.Application.Categorias.DTOs;
using ControleGastosResidencial.Common;
using ControleGastosResidencial.Data;
using ControleGastosResidencial.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastosResidencial.Application.Categorias;

public class DeleteCategoriaHandler (AppDbContext _context)
{
    public async Task<Result> HandleAsync(int id)
    {
        var categoria = await _context.Categorias
            .FirstOrDefaultAsync(c => c.Id == id);
        
        if (categoria is null)
            return Result.Failure("Categoria não encontrada");
        
        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();

        return Result.Success();
    }
}
