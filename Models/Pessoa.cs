using System;
using ControleGastosResidencial.Common;

namespace ControleGastosResidencial.Models;

public class Pessoa
{
    public int Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public int Idade { get; private set; }
    public IReadOnlyCollection<Transacao> Transacoes => _transacoes;
    private readonly List<Transacao> _transacoes = new();

    private Pessoa() { }

    public static Result<Pessoa> Criar(string nome, int idade)
    {
        var pessoa = new Pessoa();

        var nomeResult = pessoa.AtualizarNome(nome);
        if (!nomeResult.IsSuccess)
            return Result<Pessoa>.Failure(nomeResult.Error!);

        var idadeResult = pessoa.AtualizarIdade(idade);
        if (!idadeResult.IsSuccess)
            return Result<Pessoa>.Failure(idadeResult.Error!);

        return Result<Pessoa>.Success(pessoa);
    }

    public Result AtualizarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            return Result.Failure("Nome não pode ser vazio.");

        if (nome.Length < 2)
            return Result.Failure("Nome deve ter pelo menos 2 caracteres.");
        
        if (nome.Length > 200)
            return Result.Failure("Nome deve ter no máximo 200 caracteres.");

        Nome = nome.Trim();
        return Result.Success();
    }

    public Result AtualizarIdade(int idade)
    {
        if (idade < 0)
            return Result.Failure("Idade não pode ser negativa.");

        if (idade > 130)
            return Result.Failure("Idade inválida.");

        Idade = idade;
        return Result.Success();
    }
}
