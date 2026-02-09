using System;

namespace ControleGastosResidencial.Application.Transacoes.DTOs;

public sealed record UpdateTransacaoRequest
(
    string Descricao,
    decimal Valor,
    int Tipo,
    int CategoriaId,
    int PessoaId
);
