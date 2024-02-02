namespace MiraiNavi.Location.Contracts;

public interface ICartesianLatLonConverter<TCartesian, TLatLon>
    where TCartesian : ICartesianCoord2<TCartesian>
    where TLatLon : ILatLon<TLatLon>
{
    #region Public Methods

    TLatLon FromCartesionToLatLng(TCartesian cartesian);

    TCartesian FromLatLngToCartesian(TLatLon latlon);

    #endregion Public Methods
}