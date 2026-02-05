using System;
using ControleGastosResidencial.ValueObjects;

namespace ControleGastosResidencial.Models;

public class Transacao
{
    public int Id { get; private set; }
    public string Descricao { get; private set; } = string.Empty;
    public ValorMonetario? Valor { get; private set; }
    public Tipo Tipo { get; private set; }
    public int CategoriaId { get; private set; }
    public Categoria Categoria { get; private set; } = new Categoria();
    public int PessoaId { get; private set; }
    public Pessoa Pessoa {get; private set; } = new Pessoa();
}
