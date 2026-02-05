using System;

namespace ControleGastosResidencial.Models;

public class Pessoa
{
    public int Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public int Idade { get; private set; }
    public IReadOnlyCollection<Transacao> Transacoes => _transacoes;
    private readonly List<Transacao> _transacoes = new();
}
