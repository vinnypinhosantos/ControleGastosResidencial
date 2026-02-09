using System;

namespace ControleGastosResidencial.Application.Transacoes.DTOs;

public sealed record CreateTransacaoRequest
(
    string Descricao,
    decimal Valor,
    int Tipo,
    int CategoriaId,
    int PessoaId
);
