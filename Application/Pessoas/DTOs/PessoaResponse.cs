using System;

namespace ControleGastosResidencial.Application.Pessoas.DTOs;

public sealed record PessoaResponse
(
    int Id,
    string Nome,
    int Idade
);
