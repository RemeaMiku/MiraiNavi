using System.Diagnostics.CodeAnalysis;
using MiraiNavi.Location.Abstracts;

namespace MiraiNavi.Location;

public record struct GeodeticCoord : IGeodeticCoord<GeodeticCoord>
{
    public static readonly GeodeticCoord NotAvailable = new(Angle.NotAvailable, Angle.NotAvailable, double.NaN);

    public GeodeticCoord(LatLon latLon, double height)
    {
        Latitude = latLon.Latitude;
        Longitude = latLon.Longitude;
        H = height;
    }

    public GeodeticCoord(Angle latitude, Angle longitude, double height)
    {
        Latitude = latitude;
        Longitude = longitude;
        H = height;
    }

    public static GeodeticCoord Origin { get; } = new(Angle.Zero, Angle.Zero, 0);

    public double H { get; set; }

    public Angle Latitude { get; set; }

    public Angle Longitude { get; set; }

    public double B
    {
        readonly get => Latitude.Radians;
        set => Latitude = Angle.FromRadians(value);
    }

    public double L
    {
        readonly get => Longitude.Radians;
        set => Longitude = Angle.FromRadians(value);
    }

    public static GeodeticCoord Parse(string s, IFormatProvider? provider)
    {
        var values = s.Split(',');
        return values.Length != 3
            ? throw new FormatException($"{nameof(s)} must be 3 elements")
            : new(Angle.Parse(values[0]), Angle.Parse(values[1]), double.Parse(values[2]));
    }

    /// <summary>
    /// Deconstructs the GeodeticCoord into latitude, longitude, and height.    
    /// </summary>
    /// <remarks>
    /// You can get the latitude, longitude, and height of the GeodeticCoord like this:
    /// var (latitude, longitude, height) = geodeticCoord;
    /// </remarks>
    /// <param name="latitude">the latitude of this coord</param>
    /// <param name="longitude">the longitude of this coord<</param>
    /// <param name="height">the height of this coord<</param>
    public readonly void Deconstruct(out Angle latitude, out Angle longitude, out double height)
    {
        latitude = Latitude;
        longitude = Longitude;
        height = H;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out GeodeticCoord result)
    {
        result = default;
        if(s is null)
            return false;
        var values = s.Split(',');
        if(values.Length != 3)
            return false;
        if(!Angle.TryParse(values[0], provider, out var lat))
            return false;
        if(!Angle.TryParse(values[1], provider, out var lng))
            return false;
        if(!double.TryParse(values[2], provider, out var height))
            return false;
        result = new(lat, lng, height);
        return true;
    }

    public readonly override string ToString() => $"{Latitude:F8},{Longitude:F8},{H:F3}";

    public readonly string ToString(string? format, IFormatProvider? formatProvider)
    {
        format ??= "F8";
        return string.Join(',', Latitude.ToString(format, formatProvider), Longitude.ToString(format, formatProvider), H.ToString("F3"));
    }

    public readonly bool IsValid => LatLon.IsValidLatitude(Latitude) && LatLon.IsValidLongitude(Longitude);
}
