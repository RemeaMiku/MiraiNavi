using static System.Double;

namespace MiraiNavi;

public static class UnitConverters
{
    #region Public Fields

    public const double DegreesPerRadian = 180.0 / Pi;

    public const double HalfPI = Pi / 2;

    public const double KilometersPerMile = 1.609344;

    public const double MetersPerMile = 1609.344;

    public const double MilesPerKiloMeter = 1.0 / KilometersPerMile;

    public const double MinutesPerRadian = 180 / Pi * 60;

    public const double RadiansPerDegree = Pi / 180.0;

    public const double RadiansPerMinute = Pi / 180 / 60;

    public const double RadiansPerSecond = Pi / 180 / 3600;

    public const double RoundDegrees = 360.0;

    public const double SecondsPerLeapYear = 31622400;

    public const double SecondsPerOrdinaryYear = 31536000;

    public const double SecondsPerRadian = 180 / Pi * 3600;

    public const double SecondsPerWeek = 604800;

    public const double TwoPi = 2.0 * Pi;

    #endregion Public Fields

    #region Public Methods

    /// <summary>
    /// Converts degrees per second to hertz.
    /// </summary>
    /// <param name="degrees">The value in degrees per second to convert.</param>
    /// <returns>The value from <paramref name="degrees"/> in hertz.</returns>
    public static double DegreesPerSecondToHertz(double degrees) =>
        degrees / RoundDegrees;

    /// <summary>
    /// Converts degrees per second to radians per second.
    /// </summary>
    /// <param name="degrees">The value in degrees per second to convert.</param>
    /// <returns>The value from <paramref name="degrees"/> in radians per second.</returns>
    public static double DegreesPerSecondToRadiansPerSecond(double degrees) =>
        HertzToRadiansPerSecond(DegreesPerSecondToHertz(degrees));

    /// <summary>
    /// Converts degrees to radian.
    /// </summary>
    /// <param name="degrees">The value in degrees to convert.</param>
    /// <returns>The value from <paramref name="degrees"/> in radian.</returns>
    public static double DegreesToRadians(double degrees) =>
        degrees * RadiansPerDegree;

    /// <summary>
    /// Converts hertz to degrees per second.
    /// </summary>
    /// <param name="hertz">The value in degrees per second to convert.</param>
    /// <returns>The value from <paramref name="hertz"/> in degrees per second.</returns>
    public static double HertzToDegreesPerSecond(double hertz) =>
        hertz * RoundDegrees;

    /// <summary>
    /// Converts hertz to radians per second.
    /// </summary>
    /// <param name="hertz">The value in radians per second to convert.</param>
    /// <returns>The value from <paramref name="hertz"/> in radians per second.</returns>
    public static double HertzToRadiansPerSecond(double hertz) =>
        hertz * TwoPi;

    /// <summary>
    /// Converts distances from kilometers to miles.
    /// </summary>
    /// <param name="kilometers">The value in kilometers to convert.</param>
    /// <returns>The value from <paramref name="kilometers"/> in miles.</returns>
    public static double KilometersToMiles(double kilometers) =>
        kilometers * MilesPerKiloMeter;

    public static double LeapYearsToSeconds(double years) =>
            years * SecondsPerLeapYear;

    /// <summary>
    /// Converts distances from miles to kilometers.
    /// </summary>
    /// <param name="miles">The value in miles to convert.</param>
    /// <returns>The value from <paramref name="miles"/> in kilometers.</returns>
    public static double MilesToKilometers(double miles) =>
        miles * KilometersPerMile;

    /// <summary>
    /// Converts distances from miles to meters.
    /// </summary>
    /// <param name="miles">The value in miles to convert.</param>
    /// <returns>The value from <paramref name="miles"/> in meters.</returns>
    public static double MilesToMeters(double miles) =>
        miles * MetersPerMile;
    public static double OrdinaryYearsToSeconds(double years) =>
            years * SecondsPerOrdinaryYear;

    /// <summary>
    /// Converts radians per second to degrees per second.
    /// </summary>
    /// <param name="radians">The value in radians per second to convert.</param>
    /// <returns>The value from <paramref name="radians"/> in degrees per second.</returns>
    public static double RadiansPerSecondToDegreesPerSecond(double radians) =>
        HertzToDegreesPerSecond(RadiansPerSecondToHertz(radians));

    /// <summary>
    /// Converts radians per second to hertz.
    /// </summary>
    /// <param name="radians">The value in radians per second to convert.</param>
    /// <returns>The value from <paramref name="radians"/> in hertz.</returns>
    public static double RadiansPerSecondToHertz(double radians) =>
        radians / TwoPi;

    /// <summary>
    /// Converts radians to degrees.
    /// </summary>
    /// <param name="radians">The value in radians to convert.</param>
    /// <returns>The value from <paramref name="radians"/> in degrees.</returns>
    public static double RadiansToDegrees(double radians) =>
        radians / RadiansPerDegree;
    public static double SecondsToLeapYears(double seconds) =>
        seconds / SecondsPerLeapYear;

    public static double SecondsToOrdinaryYears(double seconds) =>
        seconds / SecondsPerOrdinaryYear;

    public static double SecondsToWeeks(double seconds) =>
                seconds / SecondsPerWeek;

    public static double WeeksToSeconds(double weeks) =>
        weeks * SecondsPerWeek;

    #endregion Public Methods
}
