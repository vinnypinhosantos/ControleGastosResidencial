using System;
using ControleGastosResidencial.Application.Pessoas.DTOs;
using ControleGastosResidencial.Common;
using ControleGastosResidencial.Data;
using ControleGastosResidencial.Models;

namespace ControleGastosResidencial.Application.Pessoas;

public sealed class CreatePessoaHandler (AppDbContext _context)
{
    public async Task<Result<PessoaResponse>> HandleAsync(
        CreatePessoaRequest request
    )
    {
        var pessoaResult = Pessoa.Criar(request.Nome, request.Idade);

        if (!pessoaResult.IsSuccess)
            return Result<PessoaResponse>.Failure(pessoaResult.Error!);

        _context.Add(pessoaResult.Value!);
        await _context.SaveChangesAsync();

        var pessoa = pessoaResult.Value!;

        return Result<PessoaResponse>.Success(
            new PessoaResponse(
                pessoa.Id,
                pessoa.Nome,
                pessoa.Idade
            )
        );
    }
}
