namespace MiraiNavi.Time;

public static class DateTimeOffsetExtensions
{
    public static GpsTime ToGpsTime(this DateTimeOffset dateTimeOffset) => GpsTime.FromDateTimeOffset(dateTimeOffset);
}
