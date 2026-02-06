using System;

namespace ControleGastosResidencial.Application.Categorias.DTOs;

public sealed record CategoriaResponse
(
    int Id,
    string Descricao,
    int Finalidade
);
