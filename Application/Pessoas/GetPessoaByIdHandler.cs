using System;
using ControleGastosResidencial.Application.Pessoas.DTOs;
using ControleGastosResidencial.Common;
using ControleGastosResidencial.Data;
using ControleGastosResidencial.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastosResidencial.Application.Pessoas;

public class GetPessoaByIdHandler
{
    private readonly AppDbContext _context;

    public GetPessoaByIdHandler(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Result<PessoaResponse>> HandleAsync(int id)
    {
        var pessoa = await _context.Pessoas
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
        
        if (pessoa is null)
            return Result<PessoaResponse>.Failure("Pessoa não encontrada");
        
        return Result<PessoaResponse>.Success(
            new PessoaResponse(
                pessoa.Id,
                pessoa.Nome,
                pessoa.Idade
            )
        );
    }
}
