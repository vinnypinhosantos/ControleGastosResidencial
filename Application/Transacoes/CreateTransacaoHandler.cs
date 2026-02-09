using System;
using ControleGastosResidencial.Application.Transacoes.DTOs;
using ControleGastosResidencial.Common;
using ControleGastosResidencial.Data;
using ControleGastosResidencial.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastosResidencial.Application.Transacoes;

public class CreateTransacaoHandler (AppDbContext _context)
{
    public async Task<Result<TransacaoResponse>> HandleAsync (
        CreateTransacaoRequest request
    )
    {
        var tipo = request.Tipo;
        if (!Enum.IsDefined(typeof(Finalidade), tipo))
                return Result<TransacaoResponse>.Failure("Tipo Inválido");
        var tipoComoFinalidade = (Finalidade) tipo;
        
        var categoriaValida = await _context.Categorias
            .AnyAsync(
                c => 
                c.Id == request.CategoriaId && 
                (c.Finalidade == Finalidade.Ambos || c.Finalidade == tipoComoFinalidade)
            );

        if (!categoriaValida)
            return Result<TransacaoResponse>.Failure("Categoria inválida");

        var pessoaExiste = await _context.Pessoas
            .AnyAsync(p => p.Id == request.PessoaId);

        var transacaoResult = Transacao.Criar(
            request.Descricao,
            request.Valor,
            request.Tipo,
            request.CategoriaId,
            request.PessoaId
        );

        if (!transacaoResult.IsSuccess)
            return Result<TransacaoResponse>.Failure(transacaoResult.Error!);
        
        _context.Add(transacaoResult.Value!);
        await _context.SaveChangesAsync();
        var transacao = transacaoResult.Value!;

        return Result<TransacaoResponse>.Success(
            new TransacaoResponse(
                transacao.Id,
                transacao.Descricao,
                transacao.Valor!.ValorEmReais,
                (int) transacao.Tipo,
                transacao.CategoriaId,
                transacao.PessoaId
            )
        );
    }
}
