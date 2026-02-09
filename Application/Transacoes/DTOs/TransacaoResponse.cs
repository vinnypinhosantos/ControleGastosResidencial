using System;

namespace ControleGastosResidencial.Application.Transacoes.DTOs;

public sealed record TransacaoResponse
(
    int Id,
    string Descricao,
    decimal? Valor,
    int Tipo,
    int CategoriaId,
    int PessoaId
);

