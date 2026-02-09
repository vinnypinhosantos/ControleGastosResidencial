using System;
using ControleGastosResidencial.Application.Transacoes.DTOs;
using ControleGastosResidencial.Common;
using ControleGastosResidencial.Data;
using ControleGastosResidencial.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastosResidencial.Application.Transacoes;

public class GetTransacaoByIdHandler (AppDbContext _context)
{
    public async Task<Result<TransacaoResponse>> HandleAsync (
        int id
    )
    {
        var transacao = await _context.Transacoes
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id);
        
        if (transacao is null) 
            return Result<TransacaoResponse>.Failure("Transação não encontrada");
        
        return Result<TransacaoResponse>.Success(new TransacaoResponse(
            transacao.Id,
            transacao.Descricao,
            transacao.Valor?.ValorEmReais,
            (int) transacao.Tipo,
            transacao.CategoriaId,
            transacao.PessoaId
        ));
    }
}
