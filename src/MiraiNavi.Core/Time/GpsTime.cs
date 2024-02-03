using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using static System.Math;
using static MiraiNavi.UnitConverters;
using static System.TimeSpan;

namespace MiraiNavi.Time;

public readonly record struct GpsTime :
    IAdditionOperators<GpsTime, double, GpsTime>,
    IComparisonOperators<GpsTime, GpsTime, bool>,
    IComparable<GpsTime>,
    IEquatable<GpsTime>,
    IEqualityOperators<GpsTime, GpsTime, bool>,
    IFormattable,
    IParsable<GpsTime>,
    ISubtractionOperators<GpsTime, GpsTime, double>,
    ISubtractionOperators<GpsTime, double, GpsTime>,
    IUnaryPlusOperators<GpsTime, GpsTime>
{
    const double _maxTotalSeconds = int.MaxValue * SecondsPerWeek + SecondsPerWeek - double.Epsilon;

    public GpsTime(double totalSeconds)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(totalSeconds, 0, nameof(totalSeconds));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(totalSeconds, _maxTotalSeconds, nameof(totalSeconds));
        Week = (int)Floor(totalSeconds / SecondsPerWeek);
        Sow = totalSeconds - WeeksToSeconds(Week);
    }

    public GpsTime(int week, double secondsOfWeek)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(week, 0, nameof(week));
        ArgumentOutOfRangeException.ThrowIfLessThan(secondsOfWeek, 0, nameof(secondsOfWeek));
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(secondsOfWeek, SecondsPerWeek, nameof(secondsOfWeek));
        Week = week;
        Sow = secondsOfWeek;
    }

    public int Week { get; }

    public double Sow { get; }

    public double TotalSeconds => WeeksToSeconds(Week) + Sow;

    public static GpsTime operator +(GpsTime left, double right) => new(left.TotalSeconds + right);

    public static GpsTime Now => DateTimeOffset.UtcNow.ToGpsTime();

    public static bool operator >(GpsTime left, GpsTime right) =>
        left.Week > right.Week || left.Week >= right.Week && left.Sow > right.Sow;

    public static bool operator >=(GpsTime left, GpsTime right) =>
        left.Week > right.Week || (left.Week >= right.Week && left.Sow >= right.Sow);


    public static bool operator <(GpsTime left, GpsTime right) =>
        left.Week < right.Week || left.Week <= right.Week && left.Sow < right.Sow;


    public static bool operator <=(GpsTime left, GpsTime right) =>
        left.Week < right.Week || (left.Week <= right.Week && left.Sow <= right.Sow);

    public static double operator -(GpsTime left, GpsTime right) => WeeksToSeconds(left.Week - right.Week) + left.Sow - right.Sow;

    public static GpsTime operator +(GpsTime value) => value;

    public static GpsTime operator -(GpsTime left, double right) => new(left.TotalSeconds - right);

    public int CompareTo(GpsTime other)
    {
        var weekComparison = Week.CompareTo(other.Week);
        return weekComparison is not 0 ? weekComparison : Sow.CompareTo(other.Sow);
    }

    public static GpsTime Parse(string s, IFormatProvider? provider)
    {
        var values = s.Split(',', StringSplitOptions.TrimEntries);
        if(values.Length != 2)
            throw new FormatException("The input string was not in the correct format.");
        var week = int.Parse(values[0]);
        var sow = double.Parse(values[1]);
        return new(week, sow);
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out GpsTime result)
    {
        result = default;
        if(s is null)
            return false;
        var values = s.Split(',', StringSplitOptions.TrimEntries);
        if(values.Length != 2)
            return false;
        if(!int.TryParse(values[0], provider, out var week) || week < 0)
            return false;
        if(!double.TryParse(values[1], provider, out var sow) || sow < 0 || sow > SecondsPerWeek)
            return false;
        result = new(week, sow);
        return true;
    }

    public override string ToString() => $"{Week},{Sow:F3}";

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        format ??= "F3";
        return string.Join(',', Week.ToString(formatProvider), Sow.ToString(format, formatProvider));
    }

    public static readonly DateTimeOffset StartDate = new(1980, 1, 6, 0, 0, 0, Zero);

    internal const int _startDateLeapSecondCount = 9;

    public static GpsTime FromDateTimeOffset(DateTimeOffset dateTimeOffset) =>
        new((dateTimeOffset - StartDate).TotalSeconds + LeapSecond.GetLeapSecondCount(dateTimeOffset) - _startDateLeapSecondCount);

    public DateTimeOffset ToDateTimeOffset()
    {
        var @this = this;
        var leapSecond = LeapSecond._leapSecondDates.Count(d => FromDateTimeOffset(d) <= @this);
        return StartDate + FromSeconds(TotalSeconds - leapSecond + _startDateLeapSecondCount);
    }
}
