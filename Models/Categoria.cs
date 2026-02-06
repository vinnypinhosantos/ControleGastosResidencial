using System;
using ControleGastosResidencial.Common;

namespace ControleGastosResidencial.Models;

public class Categoria
{
    public int Id { get; private set; }
    public string Descricao { get; private set; } = string.Empty;
    public Finalidade Finalidade { get; private set; }
    public IReadOnlyCollection<Transacao> Transacoes => _transacoes;
    private readonly List<Transacao> _transacoes = new();
    private Categoria() {}
    public static Result<Categoria> Criar(string descricao, int finalidade)
    {
        var categoria = new Categoria();

        var descricaoResult = categoria.AtualizarDescricao(descricao);
        if (!descricaoResult.IsSuccess)
            return Result<Categoria>.Failure(descricaoResult.Error);

        var finalidadeResult = categoria.AtualizarFinalidade(finalidade);
        if (!descricaoResult.IsSuccess)
            return Result<Categoria>.Failure(finalidadeResult.Error);
        
        return Result<Categoria>.Success(categoria);
    }
    public Result AtualizarDescricao(string descricao)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            return Result.Failure("Descrição é obrigatória.");

        Descricao = descricao.Trim();
        
        return Result.Success();
    }

    public Result AtualizarFinalidade(int finalidade)
    {
        if (!Enum.IsDefined(typeof(Finalidade), finalidade))
            return Result.Failure("Finalidade inválida.");
        
        Finalidade = (Finalidade) finalidade;
        
        return Result.Success();
    }
}
