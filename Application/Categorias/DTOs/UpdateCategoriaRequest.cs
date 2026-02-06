using System;

namespace ControleGastosResidencial.Application.Categorias.DTOs;

public sealed record UpdateCategoriaRequest
(
    string Descricao,
    int Finalidade
);
