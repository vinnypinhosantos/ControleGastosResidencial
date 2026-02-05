using System;

namespace ControleGastosResidencial.Models;

public class Categoria
{
    public int Id { get; private set; }
    public string Descricao { get; private set; } = string.Empty;
    public Finalidade Finalidade { get; private set; }
    public IReadOnlyCollection<Transacao> Transacoes => _transacoes;
    private readonly List<Transacao> _transacoes = new();
}
