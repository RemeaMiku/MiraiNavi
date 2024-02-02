using MiraiNavi.Location.Contracts;

namespace MiraiNavi.Location;

public static class CoordConversion
{
    #region Public Methods

    public static TCartesian ToCartesian<TGeodetic, TCartesian>(this TGeodetic geodetic, ICartesianGeodeticCoordConverter<TCartesian, TGeodetic> converter)
        where TCartesian : ICartesianCoord3<TCartesian>
        where TGeodetic : IGeodeticCoord<TGeodetic>
        => converter.FromGeodeticToCartesian(geodetic);

    public static TCartesian ToCartesian<TLatLon, TCartesian>(this TLatLon geodetic, ICartesianLatLonConverter<TCartesian, TLatLon> converter)
        where TCartesian : ICartesianCoord2<TCartesian>
        where TLatLon : ILatLon<TLatLon>
        => converter.FromLatLngToCartesian(geodetic);

    public static EcefCoord ToEcef(this GeodeticCoord geodetic, EarthEllipsoid ellipsoid) => EcefGeodeticCoordConverter.Create(ellipsoid).FromGeodeticToCartesian(geodetic);

    public static GeodeticCoord ToGeodetic(this EcefCoord ecef, EarthEllipsoid ellipsoid) => EcefGeodeticCoordConverter.Create(ellipsoid).FromCartesionToGeodetic(ecef);

    public static TGeodetic ToGeodetic<TCartesian, TGeodetic>(this TCartesian cartesian, ICartesianGeodeticCoordConverter<TCartesian, TGeodetic> converter)
        where TCartesian : ICartesianCoord3<TCartesian>
        where TGeodetic : IGeodeticCoord<TGeodetic>
        => converter.FromCartesionToGeodetic(cartesian);

    public static TLatLon ToLatLon<TCartesian, TLatLon>(this TCartesian cartesian, ICartesianLatLonConverter<TCartesian, TLatLon> converter)
        where TCartesian : ICartesianCoord2<TCartesian>
        where TLatLon : ILatLon<TLatLon>
        => converter.FromCartesionToLatLng(cartesian);

    #endregion Public Methods
}
