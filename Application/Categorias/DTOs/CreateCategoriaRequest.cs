using System;

namespace ControleGastosResidencial.Application.Categorias.DTOs;

public sealed record CreateCategoriaRequest
(
    string Descricao,
    int Finalidade
);
