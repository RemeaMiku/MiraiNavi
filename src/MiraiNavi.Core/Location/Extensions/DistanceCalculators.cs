using System.Numerics;
using MiraiNavi.Location.Contracts;
using static System.Double;
using static MiraiNavi.Angle;

namespace MiraiNavi.Location;

public static class DistanceCalculators
{
    public static double CalculateDistance<TSelf>(this ICartesianCoord2<TSelf> start, ICartesianCoord2<TSelf> end) where TSelf : struct, ICartesianCoord2<TSelf>
    {
        var dx = end.X - start.X;
        var dy = end.Y - start.Y;
        return Sqrt(dx * dx + dy * dy);
    }

    public static double CalculateDistance<TSelf>(this ICartesianCoord3<TSelf> start, ICartesianCoord3<TSelf> end)
        where TSelf : struct, ICartesianCoord3<TSelf>
    {
        var dx = end.X - start.X;
        var dy = end.Y - start.Y;
        var dz = end.Z - start.Z;
        return Sqrt(dx * dx + dy * dy + dz * dz);
    }

    public static double CalculateDistance<TSelf>(this ILatLon<TSelf> start, ILatLon<TSelf> end, EarthEllipsoid e)
        where TSelf : ILatLon<TSelf>
    {
        if(start == end)
            return 0;
        (var sinPhi1, var cosPhi1) = SinCos(start.Latitude);
        (var sinPhi2, var cosPhi2) = SinCos(end.Latitude);
        (var sinLambda1, var cosLambda1) = SinCos(start.Longitude);
        (var sinLambda2, var cosLambda2) = SinCos(end.Longitude);
        var dx = cosPhi2 * cosLambda2 - cosPhi1 * cosLambda1;
        var dy = cosPhi2 * sinLambda2 - cosPhi1 * sinLambda1;
        var dz = sinPhi2 - sinPhi1;
        var chord = Sqrt(dx * dx + dy * dy + dz * dz);
        var centralAngleRads = 2 * double.Asin(chord / 2);
        var radius = (2 * e.A + e.B) / 3;
        return centralAngleRads * radius;
    }
}
