namespace MiraiNavi.Location.Contracts;

public interface ICartesianGeodeticCoordConverter<TCartesian, TGeodetic>
    where TCartesian : ICartesianCoord3<TCartesian>
    where TGeodetic : IGeodeticCoord<TGeodetic>
{
    TCartesian FromGeodeticToCartesian(TGeodetic geodeticCoord);

    TGeodetic FromCartesionToGeodetic(TCartesian cartesianCoord);
}