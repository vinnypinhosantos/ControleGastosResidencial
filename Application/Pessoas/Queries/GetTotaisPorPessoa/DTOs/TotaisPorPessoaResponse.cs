using System;

namespace ControleGastosResidencial.Application.Pessoas.Queries.GetTotaisPorPessoa.DTOs;

public record TotaisPorPessoaResponse
(
    IEnumerable<TotaisPorPessoa> Pessoas,
    decimal TotalReceitasGeral,
    decimal TotalDespesasGeral,
    decimal SaldoGeral
);