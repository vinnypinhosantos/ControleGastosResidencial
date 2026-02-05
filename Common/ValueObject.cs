using System.Collections.Generic;
using System.Linq;

namespace ControleGastosResidencial.Common;

public abstract class ValueObject
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (obj.GetType() != GetType())
            return false;

        var other = (ValueObject)obj;

        return GetEqualityComponents()
            .SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Aggregate(
                1,
                (current, obj) =>
                {
                    unchecked
                    {
                        return current * 23 + (obj?.GetHashCode() ?? 0);
                    }
                });
    }

    public static bool operator ==(ValueObject? one, ValueObject? two)
    {
        if (ReferenceEquals(one, two))
            return true;

        if (one is null || two is null)
            return false;

        return one.Equals(two);
    }

    public static bool operator !=(ValueObject? one, ValueObject? two)
        => !(one == two);
}
