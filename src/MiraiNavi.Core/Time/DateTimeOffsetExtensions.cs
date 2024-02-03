namespace MiraiNavi.Time;

public static class DateTimeOffsetExtensions
{
    #region Public Methods

    public static GpsTime ToGpsTime(this DateTimeOffset dateTimeOffset) => GpsTime.FromDateTimeOffset(dateTimeOffset);

    #endregion Public Methods
}
