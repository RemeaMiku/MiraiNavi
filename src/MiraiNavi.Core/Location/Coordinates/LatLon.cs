using System.Diagnostics.CodeAnalysis;
using MiraiNavi.Location.Abstracts;

namespace MiraiNavi.Location;

public record struct LatLon : ILatLon<LatLon>
{
    public static readonly LatLon NotAvailable = new(Angle.NotAvailable, Angle.NotAvailable);

    public static LatLon Origin { get; } = new();

    public Angle Latitude { get; set; }

    public Angle Longitude { get; set; }

    public LatLon(Angle latitude, Angle longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public static LatLon Parse(string s, IFormatProvider? provider)
    {
        var values = s.Split(',');
        return values.Length != 2
            ? throw new FormatException($"{nameof(s)} must be 2 elements")
            : new(Angle.Parse(values[0]), Angle.Parse(values[1]));
    }

    public readonly void Deconstruct(out Angle latitude, out Angle longitude)
    {
        latitude = Latitude;
        longitude = Longitude;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out LatLon result)
    {
        result = default;
        if(s is null)
            return false;
        var values = s.Split(',');
        if(values.Length != 2)
            return false;
        if(!Angle.TryParse(values[0], provider, out var lat))
            return false;
        if(!Angle.TryParse(values[1], provider, out var lng))
            return false;
        result = new(lat, lng);
        return true;
    }

    public readonly string ToString(string? format, IFormatProvider? formatProvider)
    {
        format ??= "F8";
        return string.Join(',', Latitude.ToString(format, formatProvider), Longitude.ToString(format, formatProvider));
    }

    public readonly override string ToString() => $"{Latitude:F8},{Longitude:F8}";

    public readonly bool IsValid => IsValidLatitude(Latitude) && IsValidLongitude(Longitude);

    public static bool IsValidLatitude(Angle latitude) => latitude <= Angle.RightAngle && latitude >= -Angle.RightAngle;

    public static bool IsValidLongitude(Angle longitude) => longitude <= Angle.StraightAngle && longitude >= -Angle.StraightAngle;
}
