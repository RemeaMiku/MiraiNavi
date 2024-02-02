using static System.Math;

namespace MiraiNavi.Location;

/// <summary>
/// Represents an earth ellipsoid. An earth ellipsoid or earth spheroid is a mathematical figure approximating the earth's form, used as a reference frame for computations in geodesy, astronomy, and the geosciences.
/// </summary>
/// <remarks>
/// Create an instance by the semi-major axis(equatorial radius) and the semi-minor axis(polar radius) of the ellipse.
/// </remarks>
/// <param name="A">the semi-major axis(equatorial radius)</param>
/// <param name="B">the semi-minor axis(polar radius)</param>
public class EarthEllipsoid(double A, double B)
{
    #region Public Fields

    public readonly static EarthEllipsoid Grs80 = new(6378137.0, 6356752.31414036);

    public readonly static EarthEllipsoid Wgs84 = new(6378137.0, 6356752.31424517);

    #endregion Public Fields

    #region Public Properties

    /// <summary>
    /// Gets the semi-major axis(equatorial radius) of the ellipse
    /// </summary>
    public double A { get; init; } = A;

    /// <summary>
    /// Gets the semi-minor axis(polar radius) of the ellipse
    /// </summary>
    public double B { get; init; } = B;

    /// <summary>
    /// Gets the first eccentricity of the ellipsoid
    /// </summary>
    public double E1 => Sqrt(E12);

    /// <summary>
    /// Gets the first eccentricity squared of the ellipsoid
    /// </summary>
    public double E12 => 1 - Pow(B / A, 2);
    /// <summary>
    /// Gets the second eccentricity of the ellipsoid
    /// </summary>
    public double E2 => Sqrt(E22);

    /// <summary>
    /// Gets the second eccentricity squared of the ellipsoid
    /// </summary>
    public double E22 => Pow(A / B, 2) - 1;
    /// <summary>
    /// Gets the flattening of the ellipse
    /// </summary>    
    public double F => (A - B) / A;

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
