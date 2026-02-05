using System;
using ControleGastosResidencial.Application.Pessoas.DTOs;
using ControleGastosResidencial.Common;
using ControleGastosResidencial.Data;

namespace ControleGastosResidencial.Application.Pessoas;

public sealed class UpdatePessoaHandler
{
    private readonly AppDbContext _context;

    public UpdatePessoaHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PessoaResponse>> HandleAsync(
        int id,
        UpdatePessoaRequest request
    )
    {
        var pessoa = await _context.Pessoas.FindAsync(id);

        if (pessoa is null)
            return Result<PessoaResponse>.Failure("Pessoa não encontrada.");

        var nomeResult = pessoa.AtualizarNome(request.Nome);
        if (!nomeResult.IsSuccess)
            return Result<PessoaResponse>.Failure(nomeResult.Error!);

        var idadeResult = pessoa.AtualizarIdade(request.Idade);
        if (!idadeResult.IsSuccess)
            return Result<PessoaResponse>.Failure(idadeResult.Error!);

        await _context.SaveChangesAsync();

        return Result<PessoaResponse>.Success(
            new PessoaResponse(
                pessoa.Id,
                pessoa.Nome,
                pessoa.Idade
            )
        );
    }
}

