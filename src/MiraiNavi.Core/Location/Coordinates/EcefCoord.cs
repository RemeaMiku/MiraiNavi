using System.Diagnostics.CodeAnalysis;
using MiraiNavi.Location.Abstracts;

namespace MiraiNavi.Location;

public record struct EcefCoord : ICartesianCoord3<EcefCoord>
{
    public static readonly EcefCoord NotAvailable = new(double.NaN, double.NaN, double.NaN);

    public static EcefCoord Origin { get; } = new(0, 0, 0);

    public EcefCoord(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public EcefCoord(IList<double> xyz)
    {
        if(xyz.Count != 3)
            throw new ArgumentException($"{nameof(xyz)} must be 3 elements", nameof(xyz));
        X = xyz[0];
        Y = xyz[1];
        Z = xyz[2];
    }

    public EcefCoord(Span<double> xyz)
    {
        if(xyz.Length != 3)
            throw new ArgumentException($"{nameof(xyz)} must be 3 elements", nameof(xyz));
        X = xyz[0];
        Y = xyz[1];
        Z = xyz[2];
    }

    public EcefCoord(CartesianCoord2 coord, double z)
    {
        X = coord.X;
        Y = coord.Y;
        Z = z;
    }

    public double X { get; set; }

    public double Y { get; set; }

    public double Z { get; set; }

    public static EcefCoord operator +(EcefCoord left, EcefCoord right)
    {
        return new()
        {
            X = left.X + right.X,
            Y = left.Y + right.Y,
            Z = left.Z + right.Z
        };
    }

    public static EcefCoord operator /(EcefCoord left, double right)
    {
        return new()
        {
            X = left.X / right,
            Y = left.Y / right,
            Z = left.Z / right
        };
    }

    public static EcefCoord operator *(EcefCoord left, double right)
    {
        return new()
        {
            X = left.X * right,
            Y = left.Y * right,
            Z = left.Z * right
        };
    }

    public static EcefCoord operator -(EcefCoord left, EcefCoord right)
    {
        return new()
        {
            X = left.X - right.X,
            Y = left.Y - right.Y,
            Z = left.Z - right.Z
        };
    }

    public static EcefCoord operator +(EcefCoord value) => value;

    public static EcefCoord operator -(EcefCoord value)
    {
        return new()
        {
            X = -value.X,
            Y = -value.Y,
            Z = -value.Z
        };
    }

    /// <summary>
    /// Deconstructs the EcefCoord into x, y, and z.    
    /// </summary>
    /// <remarks>
    /// You can get the x, y, and z of the EcefCoord like this:
    /// var(x, y, z) = ecefCoord
    /// </remarks>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public readonly void Deconstruct(out double x, out double y, out double z)
    {
        x = X;
        y = Y;
        z = Z;
    }

    public readonly override string ToString() => $"{X:F3},{Y:F3},{Z:F3}";

    public readonly string ToString(string? format, IFormatProvider? formatProvider)
    {
        format ??= "F3";
        return string.Join(',', X.ToString(format, formatProvider), Y.ToString(format, formatProvider), Z.ToString(format, formatProvider));
    }

    public static EcefCoord Parse(string s, IFormatProvider? provider)
    {
        var values = s.Split(',', StringSplitOptions.TrimEntries);
        return values.Length != 3
            ? throw new FormatException("s must be 3 elements")
            : new(double.Parse(values[0], provider), double.Parse(values[1], provider), double.Parse(values[2], provider));
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out EcefCoord result)
    {
        result = default;
        if(s is null)
            return false;
        var values = s.Split(',', StringSplitOptions.TrimEntries);
        if(values.Length != 3)
            return false;
        if(!double.TryParse(values[0], provider, out var x))
            return false;
        if(!double.TryParse(values[1], provider, out var y))
            return false;
        if(!double.TryParse(values[2], provider, out var z))
            return false;
        result = new(x, y, z);
        return true;
    }
}
