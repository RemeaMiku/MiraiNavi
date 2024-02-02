namespace MiraiNavi.Location.Contracts;

public interface IGeodeticCoord<TSelf> : ILatLon<TSelf> where TSelf : IGeodeticCoord<TSelf>
{
    double H { get; set; }
}
