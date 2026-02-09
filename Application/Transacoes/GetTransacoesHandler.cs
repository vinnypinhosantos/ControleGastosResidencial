using System;
using ControleGastosResidencial.Application.Transacoes.DTOs;
using ControleGastosResidencial.Common;
using ControleGastosResidencial.Data;
using ControleGastosResidencial.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastosResidencial.Application.Transacoes;

public class GetTransacoesHandler (AppDbContext _context)
{
    public async Task<Result<IEnumerable<TransacaoResponse>>> HandleAsync (
        int? tipo
    )
    {
        IQueryable<Transacao> query = _context.Transacoes.AsNoTracking();

        if (tipo is not null)
        {
            if (!Enum.IsDefined(typeof(Finalidade), tipo))
                return Result<IEnumerable<TransacaoResponse>>.Failure("Tipo Inválido");
            
            var tipoAsEnum = (Tipo) tipo;

            query = query.Where(t =>
                t.Tipo == tipoAsEnum
            );
        }

        var transacoes = await query
            .Select(t => new TransacaoResponse(
                t.Id,
                t.Descricao,
                t.Valor!.ValorEmCentavos,
                (int) t.Tipo,
                t.CategoriaId,
                t.PessoaId
            ))
            .ToListAsync();
        
        return Result<IEnumerable<TransacaoResponse>>.Success(transacoes);
    }
}
