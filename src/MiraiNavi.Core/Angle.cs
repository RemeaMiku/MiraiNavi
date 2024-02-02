using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Text.Json.Serialization;
using MiraiNavi.Serialization;
using static System.Double;

namespace MiraiNavi;

/// <summary>
/// Represents an angle consisting of degrees, minutes, and seconds.
/// </summary>
[JsonConverter(typeof(AngleJsonConverter))]
public readonly struct Angle :
    IAdditionOperators<Angle, Angle, Angle>,
    IComparable,
    IComparable<Angle>,
    IComparisonOperators<Angle, Angle, bool>,
    IDivisionOperators<Angle, double, Angle>,
    IEquatable<Angle>,
    IEqualityOperators<Angle, Angle, bool>,
    IFormattable,
    IMultiplyOperators<Angle, double, Angle>,
    IParsable<Angle>,
    ISubtractionOperators<Angle, Angle, Angle>,
    IUnaryPlusOperators<Angle, Angle>,
    IUnaryNegationOperators<Angle, Angle>
{

    #region Public Fields

    public const double DegreesPerRadian = 180 / Pi;
    public const double DoublePI = 2 * Pi;
    public const double MinutesPerRadian = 180 / Pi * 60;
    public const double OneHalfOfPI = Pi / 2;
    public const double RadiansPerDegree = Pi / 180;
    public const double RadiansPerMinute = Pi / 180 / 60;
    public const double RadiansPerSecond = Pi / 180 / 3600;
    public const double SecondsPerRadian = 180 / Pi * 3600;

    public readonly static Angle NotAvailable = new(NaN);
    public readonly static Angle MaxValue = new(double.MaxValue);
    public readonly static Angle MinValue = new(double.MinValue);
    public readonly static Angle NegativeInfinity = new(double.NegativeInfinity);
    public readonly static Angle PositiveInfinity = new(double.PositiveInfinity);
    public readonly static Angle RightAngle = new(OneHalfOfPI);
    public readonly static Angle RoundAngle = new(DoublePI);
    public readonly static Angle StraightAngle = new(Pi);
    public readonly static Angle Zero = new(0);

    #endregion Public Fields

    Angle(double radians) => Radians = radians;

    #region Public Constructors

    public Angle(int degrees, int minutes = 0, double seconds = 0)
    {
        var sign = degrees >= 0 ? 1 : -1;
        Radians = degrees * RadiansPerDegree + sign * (minutes * RadiansPerMinute + seconds * RadiansPerSecond);
    }

    #endregion Public Constructors

    #region Public Enums

    public enum Ranges
    {
        ZeroToRound,
        NegativeStraightToStraight
    }

    #endregion Public Enums

    #region Public Properties

    public int Degrees => (int)TotalDegrees;

    public int Minutes => (int)(double.Abs(TotalMinutes) % 60);

    public double Radians { get; }

    public double Seconds => (double)(decimal.Abs((decimal)TotalSeconds) % 60);

    public double TotalDegrees => DegreesPerRadian * Radians;

    public double TotalMinutes => MinutesPerRadian * Radians;

    public double TotalSeconds => SecondsPerRadian * Radians;

    #endregion Public Properties

    #region Public Methods

    public static Angle Abs(Angle angle) => new(double.Abs(angle.Radians));

    public static Angle Acos(double value) => new(double.Acos(value));

    public static Angle Acosh(double value) => new(double.Acosh(value));

    public static Angle Asin(double value) => new(double.Asin(value));

    public static Angle Asinh(double value) => new(double.Asinh(value));

    public static Angle Atan(double value) => new(double.Atan(value));

    public static Angle Atan2(double y, double x) => new(double.Atan2(y, x));

    public static Angle Atanh(double value) => new(double.Atanh(value));

    public static double Cos(Angle angle) => double.Cos(angle.Radians);

    public static double Cosh(Angle angle) => double.Cosh(angle.Radians);

    public static double Cot(Angle angle) => double.Tan(OneHalfOfPI - angle.Radians);

    public static double Cot(double radians) => double.Tan(Pi / 2 - radians);

    public static double Csc(Angle angle) => 1 / Sin(angle);

    public static double Csc(double radians) => 1 / double.Sin(radians);

    public static Angle FromDegrees(double degrees) => new(degrees * RadiansPerDegree);

    public static Angle FromRadians(double radians) => new(radians);

    public static double DegreesToRadians(double degrees) => degrees * RadiansPerDegree;

    public static double RadiansToDegrees(double radians) => radians * DegreesPerRadian;

    public static Angle Max(Angle left, Angle right) => new(double.Max(left.Radians, right.Radians));

    public static Angle Min(Angle left, Angle right) => new(double.Min(left.Radians, right.Radians));

    public static Angle operator -(Angle left, Angle right) => new(left.Radians - right.Radians);

    public static Angle operator -(Angle angle) => new(-angle.Radians);

    public static bool operator !=(Angle left, Angle right) => !(left == right);

    public static Angle operator *(double num, Angle angle) => new(angle.Radians * num);

    public static Angle operator *(Angle angle, double num) => new(angle.Radians * num);

    public static Angle operator /(Angle angle, double num) => new(angle.Radians / num);

    public static Angle operator +(Angle left, Angle right) => new(left.Radians + right.Radians);

    public static Angle operator +(Angle value) => value;

    public static bool operator <(Angle left, Angle right) => left.Radians < right.Radians;

    public static bool operator <=(Angle left, Angle right) => left.Radians <= right.Radians;

    public static bool operator ==(Angle left, Angle right) => left.Equals(right);

    public static bool operator >(Angle left, Angle right) => left.Radians > right.Radians;

    public static bool operator >=(Angle left, Angle right) => left.Radians >= right.Radians;

    public static Angle Parse(string s, IFormatProvider? provider = null) => FromDegrees(double.Parse(s, provider));

    public static Angle Parse(ReadOnlySpan<char> s, IFormatProvider? provider) => FromDegrees(double.Parse(s, provider));

    public static Angle Parse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider) => FromDegrees(double.Parse(utf8Text, provider));

    public static double Sec(Angle angle) => 1 / Cos(angle);

    public static double Sec(double radians) => 1 / double.Cos(radians);

    public static int Sign(Angle angle) => double.Sign(angle.Radians);

    public static double Sin(Angle angle) => double.Sin(angle.Radians);

    public static (double Sin, double Cos) SinCos(Angle angle) => double.SinCos(angle.Radians);

    public static double Sinh(Angle angle) => double.Sinh(angle.Radians);

    public static double Tan(Angle angle) => double.Tan(angle.Radians);

    public static double Tanh(Angle angle) => double.Tanh(angle.Radians);

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Angle result)
    {
        if(!double.TryParse(s, provider, out var deg))
        {
            result = default;
            return false;
        }
        result = FromDegrees(deg);
        return true;
    }

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Angle result)
    {
        var flag = double.TryParse(s, provider, out var degrees);
        result = FromDegrees(degrees);
        return flag;
    }

    public static bool TryParse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider, [MaybeNullWhen(false)] out Angle result)
    {
        var flag = double.TryParse(utf8Text, provider, out var degrees);
        result = FromDegrees(degrees);
        return flag;
    }

    public Angle AddDegrees(double degrees) => new(Radians + degrees * RadiansPerDegree);

    public Angle AddMinutes(double minutes) => new(Radians + minutes * RadiansPerMinute);

    public Angle AddRadians(double radians) => new(Radians + radians);

    public Angle AddSeconds(double seconds) => new(Radians + seconds * RadiansPerSecond);

    public readonly int CompareTo(Angle other) => Radians.CompareTo(other.Radians);

    public int CompareTo(object? obj)
    {
        return obj is not Angle angle ? throw new ArgumentException($"Object must be of type {nameof(Angle)}") : CompareTo(angle);
    }

    /// <summary>
    /// Deconstructs this angle into degrees, minutes, and seconds.   
    /// </summary>
    /// <remarks>
    /// You can get the degrees, minutes, and seconds of this angle like this:
    /// var (degrees, minutes, seconds) = angle;
    /// </remarks>
    /// <param name="degrees">degrees of the angle</param>
    /// <param name="minutes">minutes of the angle</param>
    /// <param name="seconds">seconds of the angle</param>
    public void Deconstruct(out int degrees, out int minutes, out double seconds)
    {
        var totalSeconds = SecondsPerRadian * Radians;
        var absTotalSeconds = double.Abs(totalSeconds);
        degrees = (int)(totalSeconds / 3600);
        minutes = (int)(absTotalSeconds / 60 % 60);
        seconds = (double)((decimal)absTotalSeconds % 60);
    }

    public readonly bool Equals(Angle other) => Radians == other.Radians;

    public override bool Equals(object? obj) => obj is Angle angle && Equals(angle);

    public override int GetHashCode() => Radians.GetHashCode();

    public Angle Map(Ranges range)
    {
        var angle = new Angle(Radians % DoublePI);
        if(angle < Zero)
            angle += RoundAngle;
        if(range == Ranges.NegativeStraightToStraight && angle > StraightAngle)
            angle -= RoundAngle;
        return angle;
    }

    public override string ToString() => $"{TotalDegrees:F8}";

    /// <summary>
    /// Converts this to a formatted string.
    /// </summary>
    /// <param name="format">
    ///   <list type="table">
    ///     <listheader>
    ///       <term>Format strings</term>
    ///     </listheader>
    ///     <item>
    ///       <term>"deg"</term>
    ///       <description>Format in degrees</description>
    ///     </item>
    ///     <item>
    ///       <term>"rad"</term>
    ///       <description>Format in radians</description>
    ///     </item>
    ///     <item>
    ///       <term>"dms"</term>
    ///       <description>Format in degrees°minutes′seconds″]</description>
    ///     </item>
    ///   </list>
    /// </param>
    /// <param name="formatProvider"></param>
    /// <returns></returns>
    public string ToString(string? format, IFormatProvider? formatProvider = null)
    {
        format ??= "F8";
        switch(format.ToUpper())
        {
            case "DEG":
                return $"{TotalDegrees.ToString(formatProvider)}";
            case "RAD":
                return $"{Radians.ToString(formatProvider)}";
            case "DMS":
                var (degrees, minutes, seconds) = this;
                return $"{degrees}°{minutes}′{seconds:F6}″";
            default:
                return $"{TotalDegrees.ToString(format, formatProvider)}";
        }
    }

    #endregion Public Methods
}