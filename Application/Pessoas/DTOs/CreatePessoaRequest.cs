using System;

namespace ControleGastosResidencial.Application.Pessoas.DTOs;

public sealed record CreatePessoaRequest
(
    string Nome,
    int Idade
);
