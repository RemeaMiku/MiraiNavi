namespace MiraiNavi.Location.Contracts;

public interface ICartesianLatLonConverter<TCartesian, TLatLon>
    where TCartesian : ICartesianCoord2<TCartesian>
    where TLatLon : ILatLon<TLatLon>
{
    TCartesian FromLatLngToCartesian(TLatLon latlon);

    TLatLon FromCartesionToLatLng(TCartesian cartesian);
}