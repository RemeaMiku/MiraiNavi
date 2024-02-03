namespace MiraiNavi.Location.Abstracts;

public interface ICartesianLatLonConverter<TCartesian, TLatLon>
    where TCartesian : ICartesianCoord2<TCartesian>
    where TLatLon : ILatLon<TLatLon>
{
    #region Public Methods

    TLatLon CartesionToLatLng(TCartesian cartesian);

    TCartesian LatLngToCartesian(TLatLon latlon);

    #endregion Public Methods
}