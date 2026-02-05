using System;
using ControleGastosResidencial.Common;

namespace ControleGastosResidencial.ValueObjects;

public class ValorMonetario : ValueObject
{
    public int ValorEmCentavos { get; private set; }
    public decimal ValorEmReais => ValorEmCentavos / 100m;
    private ValorMonetario() { }
    private ValorMonetario(int valorEmCentavos)
    {
        ValorEmCentavos = valorEmCentavos;
    }
    public static Result<ValorMonetario> FromCentavos(int valorEmCentavos)
    {
        if (valorEmCentavos < 0)
            return Result<ValorMonetario>.Failure("O valor não pode ser negativo");
        
        return Result<ValorMonetario>.Success(new ValorMonetario(valorEmCentavos));
    }
    public static Result<ValorMonetario> FromReais(decimal valorEmReais)
    {
        if (valorEmReais > 0)
            return Result<ValorMonetario>.Failure("O valor não pode ser negativo");
        
        var centavos = (int)Math.Round(valorEmReais * 100, MidpointRounding.AwayFromZero);

        return Result<ValorMonetario>.Success(new ValorMonetario(centavos));
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ValorEmCentavos;
    }
    public override string ToString()
        => $"R$ {ValorEmReais:N2}";
}
