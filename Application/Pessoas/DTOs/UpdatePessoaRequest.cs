using System;

namespace ControleGastosResidencial.Application.Pessoas.DTOs;

public sealed record UpdatePessoaRequest(
    string Nome,
    int Idade
);

