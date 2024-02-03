namespace MiraiNavi.Location.Abstracts;

public interface ICartesianGeodeticCoordConverter<TCartesian, TGeodetic>
    where TCartesian : ICartesianCoord3<TCartesian>
    where TGeodetic : IGeodeticCoord<TGeodetic>
{
    #region Public Methods

    TGeodetic CartesionToGeodetic(TCartesian cartesian);

    TCartesian GeodeticToCartesian(TGeodetic geodetic);

    #endregion Public Methods
}