using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.TimeSpan;

namespace MiraiNavi.Time;

public static class TimeConverters
{
    const int _startDateLeapSecondCount = 9;

    readonly static TimeSpan _startDateLeapSecondOffset = FromSeconds(_startDateLeapSecondCount);

    public static GpsTime ToGpsTime(this DateTimeOffset dateTimeOffset) => new(dateTimeOffset - GpsTime.StartDate + dateTimeOffset.GetLeapSecondOffset() - _startDateLeapSecondOffset);

    public static DateTimeOffset ToDateTimeOffset(this GpsTime gpsTime)
    {
        var leapSecondOffset = FromSeconds(LeapSecond._leapSecondDates.Count(d => ToGpsTime(d) <= gpsTime));
        return GpsTime.StartDate + gpsTime.DurationSinceStartDate - leapSecondOffset + _startDateLeapSecondOffset;
    }
}
