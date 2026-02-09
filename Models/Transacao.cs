using System;
using ControleGastosResidencial.Common;
using ControleGastosResidencial.ValueObjects;
using Microsoft.EntityFrameworkCore.Update;

namespace ControleGastosResidencial.Models;

public class Transacao
{
    public int Id { get; private set; }
    public string Descricao { get; private set; } = string.Empty;
    public ValorMonetario? Valor { get; private set; }
    public Tipo Tipo { get; private set; }
    public int CategoriaId { get; private set; }
    public Categoria? Categoria { get; private set;}
    public int PessoaId { get; private set; }
    public Pessoa? Pessoa { get; private set;}

    private Transacao() {}
    public static Result<Transacao> Criar(
        string descricao, 
        decimal valor, 
        int tipo, 
        int categoriaId,
        int pessoaId)
    {
        var transacao = new Transacao();

        var descricaoResult = transacao.AtualizarDescricao(descricao);
        if (!descricaoResult.IsSuccess)
            return Result<Transacao>.Failure(descricaoResult.Error!);

        var valorResult = transacao.AtualizarValor(valor);
        if (!valorResult.IsSuccess)
            return Result<Transacao>.Failure(valorResult.Error!);
        
        var tipoResult = transacao.AtualizarTipo(tipo);
        if (!tipoResult.IsSuccess)
            return Result<Transacao>.Failure(tipoResult.Error!);
        
        transacao.CategoriaId = categoriaId;
        transacao.PessoaId = pessoaId;

        return Result<Transacao>.Success(transacao);
    }
    public Result Atualizar(
        string descricao,
        decimal valor,
        int tipo,
        int categoriaId,
        int pessoaId) 
    {
        var r1 = AtualizarDescricao(descricao);
        if (!r1.IsSuccess) return r1;

        var r2 = AtualizarValor(valor);
        if (!r2.IsSuccess) return r2;

        var r3 = AtualizarTipo(tipo);
        if (!r3.IsSuccess) return r3;

        var r4 = AtualizarCategoria(categoriaId);
        if (!r4.IsSuccess) return r4;

        var r5 = AtualizarPessoa(pessoaId);
        if (!r5.IsSuccess) return r5;

        return Result.Success();
    }

    public Result AtualizarDescricao(string descricao)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            return Result.Failure("Descrição é obrigatória.");
        
        Descricao = descricao.Trim();

        return Result.Success();
    }
    public Result AtualizarValor(decimal valor)
    {
        var valorResult = ValorMonetario.FromReais(valor);
        if (!valorResult.IsSuccess)
            return Result.Failure(valorResult.Error!);

        Valor = valorResult.Value!;

        return Result.Success();
    }
    public Result AtualizarTipo(int tipo)
    {
        if (!Enum.IsDefined(typeof(Tipo), tipo))
            return Result.Failure("Tipo inválido.");
        
        Tipo = (Tipo) tipo;
        
        return Result.Success();
    }
    public Result AtualizarCategoria(int categoriaId)
    {
        if (categoriaId <= 0)
            return Result.Failure("Categoria inválida.");

        CategoriaId = categoriaId;
        Categoria = null;

        return Result.Success();
    }
    public Result AtualizarPessoa(int pessoaId)
    {
        if (pessoaId <= 0)
            return Result.Failure("Pessoa inválida.");

        PessoaId = pessoaId;
        Pessoa = null;

        return Result.Success();
    }
}
