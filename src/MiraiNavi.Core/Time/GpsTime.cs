using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using static System.TimeSpan;

namespace MiraiNavi.Time;

public readonly record struct GpsTime :
    IAdditionOperators<GpsTime, TimeSpan, GpsTime>,
    IComparisonOperators<GpsTime, GpsTime, bool>,
    IComparable<GpsTime>,
    IEquatable<GpsTime>,
    IEqualityOperators<GpsTime, GpsTime, bool>,
    IFormattable,
    IParsable<GpsTime>,
    ISubtractionOperators<GpsTime, GpsTime, TimeSpan>,
    ISubtractionOperators<GpsTime, TimeSpan, GpsTime>,
    IUnaryPlusOperators<GpsTime, GpsTime>
{
    public GpsTime(double totalSeconds)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(totalSeconds, 0, nameof(totalSeconds));
        DurationSinceStartDate = FromSeconds(totalSeconds);
    }

    public GpsTime(int week, double secondsOfWeek)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(week, 0, nameof(week));
        ArgumentOutOfRangeException.ThrowIfLessThan(secondsOfWeek, 0, nameof(secondsOfWeek));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(secondsOfWeek, Constants.SecondsPerWeek, nameof(secondsOfWeek));
        DurationSinceStartDate = FromSeconds(week * Constants.SecondsPerWeek + secondsOfWeek);
    }

    public GpsTime(TimeSpan durationSinceStartDate)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(durationSinceStartDate, Zero, nameof(durationSinceStartDate));
        DurationSinceStartDate = durationSinceStartDate;
    }

    public int Week => DurationSinceStartDate.Days / 7;

    public double Sow => DurationSinceStartDate.TotalSeconds % Constants.SecondsPerWeek;

    public TimeSpan DurationSinceStartDate { get; }

    public double TotalSeconds => DurationSinceStartDate.TotalSeconds;

    public static GpsTime operator +(GpsTime left, TimeSpan right) => new(left.DurationSinceStartDate + right);

    public static GpsTime Now => DateTimeOffset.UtcNow.ToGpsTime();

    public static bool operator >(GpsTime left, GpsTime right) => left.DurationSinceStartDate > right.DurationSinceStartDate;

    public static bool operator >=(GpsTime left, GpsTime right) => left.DurationSinceStartDate >= right.DurationSinceStartDate;

    public static bool operator <(GpsTime left, GpsTime right) => left.DurationSinceStartDate < right.DurationSinceStartDate;

    public static bool operator <=(GpsTime left, GpsTime right) => left.DurationSinceStartDate <= right.DurationSinceStartDate;

    public static TimeSpan operator -(GpsTime left, GpsTime right) => left.DurationSinceStartDate - right.DurationSinceStartDate;

    public static GpsTime operator +(GpsTime value) => value;

    public static GpsTime operator -(GpsTime left, TimeSpan offset) => new(left.DurationSinceStartDate - offset);

    public int CompareTo(GpsTime other) => DurationSinceStartDate.CompareTo(other.DurationSinceStartDate);

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
        if(!double.TryParse(values[1], provider, out var sow) || sow < 0 || sow > Constants.SecondsPerWeek)
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
}
