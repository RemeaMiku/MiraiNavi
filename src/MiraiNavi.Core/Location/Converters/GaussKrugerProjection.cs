using MiraiNavi.Location.Abstracts;
using static MiraiNavi.Angle;
using static System.Double;

namespace MiraiNavi.Location;

public sealed class GaussKrugerProjection : ICartesianLatLonConverter<CartesianCoord2, LatLon>
{
    #region Public Constructors

    public GaussKrugerProjection(EarthEllipsoid ellipsoid, Angle centralLongitude)
    {
        Ellipsoid = ellipsoid;
        CentralLongitude = centralLongitude;
        SetValues();
    }

    #endregion Public Constructors

    #region Public Properties

    public Angle CentralLongitude { get; set; }

    public EarthEllipsoid Ellipsoid { get; }

    #endregion Public Properties

    #region Public Methods

    public LatLon CartesionToLatLng(CartesianCoord2 cartesianCoord)
    {
        var (x, y) = cartesianCoord;
        var e2_1 = Ellipsoid.E12;
        var B_f0 = x / (Ellipsoid.A * _a * (1 - e2_1));
        double GetB_f1(double B_f0) => x / (Ellipsoid.A * _a * (1 - e2_1)) + 1 / _a * (_b / 2 * Sin(2 * B_f0)) - _c / 4 * Sin(4 * B_f0) + _d / 6 * Sin(6 * B_f0) - _e / 8 * Sin(8 * B_f0);
        var B_f1 = GetB_f1(B_f0);
        while(Abs(B_f1 - B_f0) >= _iterationThereshold)
        {
            B_f0 = B_f1;
            B_f1 = GetB_f1(B_f0);
        }
        var M = Ellipsoid.M(B_f1);
        var N = Ellipsoid.N(B_f1);
        var t = Tan(B_f1);
        var eta2 = Ellipsoid.E2 * Ellipsoid.E2 * Cos(B_f1) * Cos(B_f1);
        var B = B_f1 - y * y / (2 * M * N) * t * (1 - y * y / (12 * N * N) * (5 + eta2 + 3 * t * t - 9 * eta2 * t * t) + Pow(y, 4) / (360 * Pow(N, 4)) * (61 + 90 * t * t + 45 * Pow(t, 4)));
        var dL = y / (N * Cos(B_f1)) * (1 - y * y / (6 * N * N) * (1 + eta2 + 2 * t * t) + Pow(y, 4) / (120 * Pow(N, 4)) * (5 + 6 * eta2 + 28 * t * t + 8 * eta2 * t * t + 24 * Pow(t, 4)));
        return new(FromRadians(B), FromRadians(dL) + CentralLongitude);
    }

    public CartesianCoord2 LatLngToCartesian(LatLon latlon)
    {
        var (latitude, longitude) = latlon;
        var B = latitude.Radians;
        var (sinB, cosB) = SinCos(B);
        var N = Ellipsoid.N(B);
        var dL = (longitude - CentralLongitude).Radians;
        var e2_1 = Ellipsoid.E12;
        var e2_2 = Ellipsoid.E22;
        var tanB = sinB / cosB;
        var eta2 = e2_2 * cosB * cosB;
        var x_0 = Ellipsoid.A * (1 - e2_1) * (_a * B - _b / 2 * Sin(2 * B) + _c / 4 * Sin(4 * B) - _d / 6 * Sin(6 * B) + _e / 8 * Sin(8 * B));
        var x = x_0 + N / 2 * sinB * cosB * dL * dL + N / 24 * sinB * Pow(cosB, 3) * (5 - tanB * tanB + 9 * eta2 + 4 * eta2 * eta2) * Pow(dL, 4) + N / 720 * sinB * Pow(cosB, 5) * (61 - 58 * tanB * tanB + Pow(tanB, 4)) * Pow(dL, 6);
        var y = N * cosB * dL + N / 6 * Pow(cosB, 3) * (1 - tanB * tanB + eta2) * Pow(dL, 3) + N / 120 * Pow(cosB, 5) * (5 - 18 * tanB * tanB + Pow(tanB, 4) + 14 * eta2 - 58 * eta2 * tanB * tanB) * Pow(dL, 5);
        return new(x, y);
    }

    #endregion Public Methods

    #region Private Fields

    const double _iterationThereshold = 1E-6;

    double _a;
    double _b;
    double _c;
    double _d;
    double _e;

    #endregion Private Fields

    #region Private Methods

    void SetValues()
    {
        var e2 = Ellipsoid.E12;
        var e4 = e2 * e2;
        var e6 = e4 * e2;
        var e8 = e6 * e2;
        var e10 = e8 * e2;
        _a = 1 + 3 / 4.0 * e2 + 45 / 64.0 * e4 + 175 / 256.0 * e6 + 11025 / 16384.0 * e8 + 43659 / 65536.0 * e10;
        _b = 3 / 4.0 * e2 + 15 / 16.0 * e4 + 525 / 512.0 * e6 + 2205 / 2048.0 * e8 + 72765 / 65536.0 * e10;
        _c = 15 / 64.0 * e4 + 105 / 256.0 * e6 + 2205 / 4096.0 * e8 + 10395 / 16384.0 * e10;
        _d = 35 / 512.0 * e6 + 315 / 2048.0 * e8 + 31185 / 131072.0 * e10;
        _e = 315 / 16384.0 * e8 + 3645 / 65536.0 * e10;
    }

    #endregion Private Methods
}
