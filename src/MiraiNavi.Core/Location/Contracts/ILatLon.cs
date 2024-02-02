namespace MiraiNavi.Location.Contracts;

public interface ILatLon<TSelf> : ICoord<TSelf> where TSelf : ILatLon<TSelf>
{
    Angle Latitude { get; set; }

    Angle Longitude { get; set; }
}