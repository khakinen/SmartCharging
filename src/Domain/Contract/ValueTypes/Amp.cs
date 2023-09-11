using SmartCharging.Domain.Contract.Exceptions;

namespace SmartCharging.Domain.Contract.ValueTypes;

public readonly struct Amp : IComparable<Amp>, IComparable
{
    private readonly int _amp;

    public Amp(int amp)
    {
        if (amp < 0)
        {
            throw new ValidationException($"{nameof(amp)} value can not be lower than zero");
        }

        _amp = amp;
    }

    public static implicit operator int(Amp a) => a._amp;
    public static implicit operator Amp(int b) => new Amp(b);

    public override string ToString() => $"{_amp}";
    
    public int CompareTo(object obj)
    {
        return this._amp.CompareTo((int)obj);
    }

    public int CompareTo(Amp other)
    {
        return _amp.CompareTo(other._amp);
    }
}