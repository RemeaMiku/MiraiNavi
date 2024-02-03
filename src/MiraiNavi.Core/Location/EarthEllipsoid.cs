using static System.Math;

namespace MiraiNavi.Location;

/// <summary>
/// Represents an earth ellipsoid. An earth ellipsoid or earth spheroid is a mathematical figure approximating the earth's form, used as a reference frame for computations in geodesy, astronomy, and the geosciences.
/// </summary>
public class EarthEllipsoid
{
    #region Public Fields

    public readonly static EarthEllipsoid Grs80 = new(6378137.0, 6356752.31414036);

    public readonly static EarthEllipsoid Wgs84 = new(6378137.0, 6356752.31424517);

    #endregion Public Fields

    #region Public Properties


    /// <summary>
    /// Create an instance by the semi-major axis(equatorial radius) and the semi-minor axis(polar radius) of the ellipse.
    /// </summary>
    /// <param name="a">the semi-major axis(equatorial radius)</param>
    /// <param name="b">the semi-minor axis(polar radius)</param>
    public EarthEllipsoid(double a, double b)
    {
        A = a;
        B = b;
        F = (a - b) / a;
        E12 = 1 - Pow(b / a, 2);
        E22 = Pow(a / b, 2) - 1;
        E1 = Sqrt(E12);
        E2 = Sqrt(E22);
    }

    /// <summary>
    /// Gets the semi-major axis(equatorial radius) of the ellipse
    /// </summary>
    public double A { get; }

    /// <summary>
    /// Gets the semi-minor axis(polar radius) of the ellipse
    /// </summary>
    public double B { get; }

    /// <summary>
    /// Gets the first eccentricity of the ellipsoid
    /// </summary>
    public double E1 { get; }

    /// <summary>
    /// Gets the first eccentricity squared of the ellipsoid
    /// </summary>
    public double E12 { get; }
    /// <summary>
    /// Gets the second eccentricity of the ellipsoid
    /// </summary>
    public double E2 { get; }

    /// <summary>
    /// Gets the second eccentricity squared of the ellipsoid
    /// </summary>
    public double E22 { get; }
    /// <summary>
    /// Gets the flattening of the ellipse
    /// </summary>    
    public double F { get; }

    #endregion Public Properties

    #region Public Methods

    /// <summary>
    /// Gets the earth's meridional radius of curvature (in the north–south direction) at the specified geodetic latitude.
    /// </summary>
    /// <param name="latRads">the specified geodetic latitude(in radians)</param>
    /// <returns>the earth's meridional radius of curvature</returns>
    public double M(double latRads) => A * (1 - E1 * E1) / Pow(1 - Pow(E1 * Sin(latRads), 2), 1.5);

    /// <summary>
    /// Gets the earth's meridional radius of curvature (in the north–south direction) at the specified geodetic latitude.
    /// </summary>
    /// <param name="latitude">the specified geodetic latitude</param>
    /// <returns>the earth's meridional radius of curvature</returns>
    public double M(Angle latitude) => M(latitude.Radians);

    /// <summary>
    /// Gets the earth's prime-vertical radius of curvature(also called the earth's transverse radius of curvature) at the specified geodetic latitude.
    /// </summary>
    /// <param name="latRads">the specified geodetic latitude(in radians)</param>
    /// <returns>the earth's prime-vertical radius of curvature</returns>
    public double N(double latRads) => A / Sqrt(1 - Pow(E1 * Sin(latRads), 2));

    /// <summary>
    /// Gets the earth's prime-vertical radius of curvature(also called the earth's transverse radius of curvature) at the specified geodetic latitude.
    /// </summary>
    /// <param name="latitude">the specified geodetic latitude</param>
    /// <returns>the earth's prime-vertical radius of curvature</returns>
    public double N(Angle latitude) => N(latitude.Radians);

    #endregion Public Methods
}
