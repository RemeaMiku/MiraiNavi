namespace MiraiNavi.Location.Contracts;

public interface ICartesianCoord3<TSelf> : ICartesianCoord2<TSelf> where TSelf : ICartesianCoord3<TSelf>
{
    double Z { get; set; }
}
