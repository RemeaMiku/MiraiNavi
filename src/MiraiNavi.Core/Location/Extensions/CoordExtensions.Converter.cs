using MiraiNavi.Location.Abstracts;

namespace MiraiNavi.Location;

static partial class CoordExtensions
{
    #region Public Methods

    public static TCartesian ToCartesian<TGeodetic, TCartesian>(this TGeodetic geodetic, ICartesianGeodeticCoordConverter<TCartesian, TGeodetic> converter)
        where TCartesian : ICartesianCoord3<TCartesian>
        where TGeodetic : IGeodeticCoord<TGeodetic>
        => converter.GeodeticToCartesian(geodetic);

    public static TCartesian ToCartesian<TLatLon, TCartesian>(this TLatLon geodetic, ICartesianLatLonConverter<TCartesian, TLatLon> converter)
        where TCartesian : ICartesianCoord2<TCartesian>
        where TLatLon : ILatLon<TLatLon>
        => converter.LatLngToCartesian(geodetic);

    public static TGeodetic ToGeodetic<TCartesian, TGeodetic>(this TCartesian cartesian, ICartesianGeodeticCoordConverter<TCartesian, TGeodetic> converter)
        where TCartesian : ICartesianCoord3<TCartesian>
        where TGeodetic : IGeodeticCoord<TGeodetic>
        => converter.CartesionToGeodetic(cartesian);

    public static TLatLon ToLatLon<TCartesian, TLatLon>(this TCartesian cartesian, ICartesianLatLonConverter<TCartesian, TLatLon> converter)
        where TCartesian : ICartesianCoord2<TCartesian>
        where TLatLon : ILatLon<TLatLon>
        => converter.CartesionToLatLng(cartesian);

    #endregion Public Methods
}
