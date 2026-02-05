using System;
using ControleGastosResidencial.Application.Pessoas.DTOs;
using ControleGastosResidencial.Common;
using ControleGastosResidencial.Data;
using ControleGastosResidencial.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastosResidencial.Application.Pessoas;

public class GetPessoasHandler
{
    private readonly AppDbContext _context;

    public GetPessoasHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<PessoaResponse>>> HandleAsync()
    {
        var pessoas = await _context.Pessoas
            .AsNoTracking()
            .Select(p => new PessoaResponse
            (
                p.Id,
                p.Nome,
                p.Idade
            ))
            .ToListAsync();
                
        return Result<IEnumerable<PessoaResponse>>.Success(pessoas);
    }

}
