using System;
using ControleGastosResidencial.Application.Transacoes.DTOs;
using ControleGastosResidencial.Common;
using ControleGastosResidencial.Data;

namespace ControleGastosResidencial.Application.Transacoes;

public sealed class UpdateTransacaoHandler (AppDbContext _context)
{
    public async Task<Result<TransacaoResponse>> HandleAsync (
        int id,
        UpdateTransacaoRequest request
    )
    {
        var transacao = await _context.Transacoes.FindAsync(id);

        if (transacao is null)
            return Result<TransacaoResponse>.Failure("Transação não encontrada");

        var atualizacaoResult = transacao.Atualizar(
            request.Descricao,
            request.Valor,
            request.Tipo,
            request.CategoriaId,
            request.PessoaId
        );

        if (!atualizacaoResult.IsSuccess)
            return Result<TransacaoResponse>.Failure(atualizacaoResult.Error!);

        await _context.SaveChangesAsync();

        return Result<TransacaoResponse>.Success(new TransacaoResponse
        (
            transacao.Id,
            transacao.Descricao,
            transacao.Valor!.ValorEmReais,
            (int) transacao.Tipo,
            transacao.CategoriaId,
            transacao.PessoaId
        ));
    }
}
