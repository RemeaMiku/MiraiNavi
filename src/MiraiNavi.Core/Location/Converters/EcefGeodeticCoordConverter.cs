using MiraiNavi.Location.Abstracts;
using static System.Double;

namespace MiraiNavi.Location;

public sealed partial class EcefGeodeticCoordConverter : ICartesianGeodeticCoordConverter<EcefCoord, GeodeticCoord>
{
    #region Public Properties

    public EarthEllipsoid Ellipsoid { get; }

    #endregion Public Properties

    #region Public Methods

    /// <summary>
    /// Gets an instance of <see cref="EcefGeodeticCoordConverter"/> with the specified <see cref="EarthEllipsoid"/>.If the instance already exists, it will be returned from the cache.
    /// </summary>
    /// <param name="ellipsoid">The specified ellipsoid. The converter will use the parameters of it to convert coordinates.</param>
    /// <returns></returns>
    public static EcefGeodeticCoordConverter Create(EarthEllipsoid ellipsoid)
    {
        if(_convertersCache.TryGetValue(ellipsoid, out var converter))
            return converter;
        converter = new(ellipsoid);
        _convertersCache[ellipsoid] = converter;
        return converter;
    }

    public static (double B, double L, double H) EcefToGeodetic(double x, double y, double z, double a, double b)
    {
        var L = Atan2(y, x);
        var r = Sqrt(x * x + y * y);
        var zz = b * Abs(z);
        var p = a * r;
        var a2 = a * a;
        var b2 = b * b;
        var q = a2 - b2;
        var add = 2 * (p + q);
        var sub = 2 * (p - q);
        var t = (zz + p + q) / (zz + 2 * p + q);
        var t2 = t * t;
        var dt = (t * (t2 * (t * zz + add) + sub) - zz) / (t2 * (4 * t * zz + 3 * add) + sub);
        while(dt > 1E-10)
        {
            t -= dt;
            t2 = t * t;
            dt = (t * (t2 * (t * zz + add) + sub) - zz) / (t2 * (4 * t * zz + 3 * add) + sub);
        }
        var B = 2 * Sign(z) * Atan(2 * a * t / (b * (1 - t2) + Sqrt(b2 * (1 - t2) * (1 - t2) + 4 * a2 * t2)));
        var sinB = Sin(B);
        var h = r * Cos(B) + z * sinB - Sqrt(a2 - (a2 - b2) * Pow(sinB, 2));
        return (B, L, h);
    }

    public static (double X, double Y, double Z) GeodeticToEcef(double B, double L, double h, double a, double b)
    {
        var sinB = Sin(B);
        var cosB = Cos(B);
        var sinL = Sin(L);
        var cosL = Cos(L);
        var sin2B = sinB * sinB;
        var a2 = a * a;
        var b2 = b * b;
        var b2Diva2 = b2 / a2;
        var N = a / Sqrt(1 - (1 - b2Diva2) * sin2B);
        var NAddh = N + h;
        var x = NAddh * cosB * cosL;
        var y = NAddh * cosB * sinL;
        var z = (N * b2Diva2 + h) * sinB;
        return (x, y, z);
    }

    public GeodeticCoord CartesionToGeodetic(EcefCoord ecef)
    {
        var (b, l, h) = EcefToGeodetic(ecef.X, ecef.Y, ecef.Z, Ellipsoid.A, Ellipsoid.B);
        return new(Angle.FromRadians(b), Angle.FromRadians(l), h);
    }

    public EcefCoord GeodeticToCartesian(GeodeticCoord geodetic)
    {
        var (x, y, z) = GeodeticToEcef(geodetic.B, geodetic.L, geodetic.H, Ellipsoid.A, Ellipsoid.B);
        return new(x, y, z);
    }

    #endregion Public Methods

    #region Private Fields

    readonly static Dictionary<EarthEllipsoid, EcefGeodeticCoordConverter> _convertersCache = [];

    #endregion Private Fields

    #region Private Constructors

    EcefGeodeticCoordConverter(EarthEllipsoid ellipsoid)
    {
        Ellipsoid = ellipsoid;
    }

    #endregion Private Constructors
}
