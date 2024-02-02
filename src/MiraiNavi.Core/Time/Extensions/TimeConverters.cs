using static System.TimeSpan;

namespace MiraiNavi.Time;

public static class TimeConverters
{
    #region Public Methods

    public static DateTimeOffset ToDateTimeOffset(this GpsTime gpsTime)
    {
        var leapSecondOffset = FromSeconds(LeapSecond._leapSecondDates.Count(d => ToGpsTime(d) <= gpsTime));
        return GpsTime.StartDate + gpsTime.DurationSinceStartDate - leapSecondOffset + _startDateLeapSecondOffset;
    }

    public static GpsTime ToGpsTime(this DateTimeOffset dateTimeOffset) => new(dateTimeOffset - GpsTime.StartDate + LeapSecond.GetLeapSecondOffset(dateTimeOffset) - _startDateLeapSecondOffset);

    #endregion Public Methods

    #region Private Fields

    const int _startDateLeapSecondCount = 9;

    readonly static TimeSpan _startDateLeapSecondOffset = FromSeconds(_startDateLeapSecondCount);

    #endregion Private Fields
}
