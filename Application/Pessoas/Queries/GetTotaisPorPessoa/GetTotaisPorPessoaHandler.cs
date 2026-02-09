using System;
using ControleGastosResidencial.Application.Pessoas.Queries.GetTotaisPorPessoa.DTOs;
using ControleGastosResidencial.Data;
using ControleGastosResidencial.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastosResidencial.Application.Pessoas.Queries.GetTotaisPorPessoa;

public sealed class GetTotaisPorPessoaHandler (AppDbContext _context)
{
    public async Task<TotaisPorPessoaResponse> HandleAsync(int? pessoaId)
    {
        IQueryable<Pessoa> query = _context.Pessoas.AsNoTracking();

        if (pessoaId is not null)
            query = query.Where(p => p.Id == pessoaId);

        var pessoasRaw = await query
            .Select(p => new
            {
                p.Id,
                p.Nome,
                TotalReceitas = p.Transacoes
                    .Where(t => t.Tipo == Tipo.Receita)
                    .Sum(t => t.Valor.ValorEmCentavos),
                TotalDespesas = p.Transacoes
                    .Where(t => t.Tipo == Tipo.Despesa)
                    .Sum(t => t.Valor.ValorEmCentavos)
            })
            .ToListAsync();

        var pessoas = pessoasRaw
            .Select(p => new TotaisPorPessoa(
                p.Id,
                p.Nome,
                p.TotalReceitas,
                p.TotalDespesas,
                p.TotalReceitas - p.TotalDespesas
            ))
            .ToList();

        return new TotaisPorPessoaResponse(
            pessoas,
            pessoas.Sum(p => p.TotalReceitas),
            pessoas.Sum(p => p.TotalDespesas),
            pessoas.Sum(p => p.Saldo)
        );
    }
}
