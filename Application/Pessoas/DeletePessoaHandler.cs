using System;
using Microsoft.EntityFrameworkCore;
using ControleGastosResidencial.Application.Pessoas.DTOs;
using ControleGastosResidencial.Common;
using ControleGastosResidencial.Data;
using ControleGastosResidencial.Models;

namespace ControleGastosResidencial.Application.Pessoas;

public class DeletePessoaHandler
{
    private readonly AppDbContext _context;

    public DeletePessoaHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result> HandleAsync(int id)
    {
        var pessoa = await _context.Pessoas
            .FirstOrDefaultAsync(p => p.Id == id);
        
        if (pessoa is null)
            return Result.Failure("Pessoa não encontrada");
        
        _context.Pessoas.Remove(pessoa);
        await _context.SaveChangesAsync();

        return Result.Success();
    }
}
