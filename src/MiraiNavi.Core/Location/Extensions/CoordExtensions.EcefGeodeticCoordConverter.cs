namespace MiraiNavi.Location;

static partial class CoordExtensions
{
    #region Public Methods

    public static EcefCoord ToEcef(this GeodeticCoord geodetic, EarthEllipsoid ellipsoid) => EcefGeodeticCoordConverter.Create(ellipsoid).GeodeticToCartesian(geodetic);

    public static GeodeticCoord ToGeodetic(this EcefCoord ecef, EarthEllipsoid ellipsoid) => EcefGeodeticCoordConverter.Create(ellipsoid).CartesionToGeodetic(ecef);

    #endregion Public Methods
}
