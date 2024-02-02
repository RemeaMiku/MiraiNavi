using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiraiNavi.Time;

public static class LeapSecond
{
    internal static readonly DateTimeOffset[] _leapSecondDates =
    [
        new(1972, 7, 1, 0, 0, 0, TimeSpan.Zero),
        new(1973, 1, 1, 0, 0, 0, TimeSpan.Zero),
        new(1974, 1, 1, 0, 0, 0, TimeSpan.Zero),
        new(1975, 1, 1, 0, 0, 0, TimeSpan.Zero),
        new(1976, 1, 1, 0, 0, 0, TimeSpan.Zero),
        new(1977, 1, 1, 0, 0, 0, TimeSpan.Zero),
        new(1978, 1, 1, 0, 0, 0, TimeSpan.Zero),
        new(1979, 1, 1, 0, 0, 0, TimeSpan.Zero),
        new(1980, 1, 1, 0, 0, 0, TimeSpan.Zero),
        new(1981, 7, 1, 0, 0, 0, TimeSpan.Zero),
        new(1982, 7, 1, 0, 0, 0, TimeSpan.Zero),
        new(1983, 7, 1, 0, 0, 0, TimeSpan.Zero),
        new(1985, 7, 1, 0, 0, 0, TimeSpan.Zero),
        new(1988, 1, 1, 0, 0, 0, TimeSpan.Zero),
        new(1990, 1, 1, 0, 0, 0, TimeSpan.Zero),
        new(1991, 1, 1, 0, 0, 0, TimeSpan.Zero),
        new(1992, 7, 1, 0, 0, 0, TimeSpan.Zero),
        new(1993, 7, 1, 0, 0, 0, TimeSpan.Zero),
        new(1994, 7, 1, 0, 0, 0, TimeSpan.Zero),
        new(1996, 1, 1, 0, 0, 0, TimeSpan.Zero),
        new(1997, 7, 1, 0, 0, 0, TimeSpan.Zero),
        new(1999, 1, 1, 0, 0, 0, TimeSpan.Zero),
        new(2006, 1, 1, 0, 0, 0, TimeSpan.Zero),
        new(2009, 1, 1, 0, 0, 0, TimeSpan.Zero),
        new(2012, 7, 1, 0, 0, 0, TimeSpan.Zero),
        new(2015, 7, 1, 0, 0, 0, TimeSpan.Zero),
        new(2017, 1, 1, 0, 0, 0, TimeSpan.Zero),
    ];

    public static int GetLeapSecondCount(this DateTimeOffset date) => _leapSecondDates.Count(d => d <= date);

    public static TimeSpan GetLeapSecondOffset(this DateTimeOffset date) => TimeSpan.FromSeconds(GetLeapSecondCount(date));

    public static DateTimeOffset GetLeapSecondDate(int count)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(count, 1, nameof(count));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(count, _leapSecondDates.Length, nameof(count));
        return _leapSecondDates[count - 1];
    }
}
