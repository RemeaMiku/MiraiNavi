using System.Diagnostics.CodeAnalysis;
using MiraiNavi.Location.Abstracts;

namespace MiraiNavi.Location;

public record struct CartesianCoord2 : ICartesianCoord2<CartesianCoord2>
{
    public static readonly CartesianCoord2 NotAvailable = new(double.NaN, double.NaN);

    public CartesianCoord2(double x, double y)
    {
        X = x;
        Y = y;
    }

    public static CartesianCoord2 Origin { get; } = new(0, 0);

    public double X { get; set; }

    public double Y { get; set; }

    public static CartesianCoord2 Parse(string s, IFormatProvider? provider)
    {
        var values = s.Split(',', StringSplitOptions.TrimEntries);
        return values.Length != 2
            ? throw new FormatException("s must be 2 elements")
            : new(double.Parse(values[0], provider), double.Parse(values[1], provider));
    }
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out CartesianCoord2 result)
    {
        result = default;
        if(s is null)
            return false;
        var values = s.Split(',', StringSplitOptions.TrimEntries);
        if(values.Length != 2)
            return false;
        if(!double.TryParse(values[0], provider, out var x))
            return false;
        if(!double.TryParse(values[1], provider, out var y))
            return false;
        result = new(x, y);
        return true;
    }

    public readonly void Deconstruct(out double x, out double y)
    {
        x = X;
        y = Y;
    }

    public readonly override string ToString() => $"{X:F3},{Y:F3}";

    public readonly string ToString(string? format, IFormatProvider? formatProvider)
    {
        format ??= "F3";
        return string.Join(',', X.ToString(format, formatProvider), Y.ToString(format, formatProvider));
    }

    public static CartesianCoord2 operator +(CartesianCoord2 value) => value;

    public static CartesianCoord2 operator +(CartesianCoord2 left, CartesianCoord2 right) => new(left.X + right.X, left.Y + right.Y);

    public static CartesianCoord2 operator -(CartesianCoord2 value) => new(-value.X, -value.Y);

    public static CartesianCoord2 operator -(CartesianCoord2 left, CartesianCoord2 right) => new(left.X - right.X, left.Y - right.Y);

    public static CartesianCoord2 operator *(CartesianCoord2 left, double right) => new(left.X * right, left.Y * right);

    public static CartesianCoord2 operator /(CartesianCoord2 left, double right) => new(left.X / right, left.Y / right);
}
