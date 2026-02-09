using System;

namespace ControleGastosResidencial.Application.Pessoas.Queries.GetTotaisPorPessoa.DTOs;

public sealed record TotaisPorPessoa
(
    int PessoaId,
    string Nome,
    decimal TotalReceitas,
    decimal TotalDespesas,
    decimal Saldo
);
