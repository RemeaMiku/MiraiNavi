namespace MiraiNavi.Location.Contracts;

public interface ICartesianGeodeticCoordConverter<TCartesian, TGeodetic>
    where TCartesian : ICartesianCoord3<TCartesian>
    where TGeodetic : IGeodeticCoord<TGeodetic>
{
    #region Public Methods

    TGeodetic FromCartesionToGeodetic(TCartesian cartesianCoord);

    TCartesian FromGeodeticToCartesian(TGeodetic geodeticCoord);

    #endregion Public Methods
}