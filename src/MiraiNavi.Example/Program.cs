using MiraiNavi;
using MiraiNavi.Location;
using MiraiNavi.Location.Converters;
using MiraiNavi.Location.Coordinates;
using MiraiNavi.Time;

var angle = new Angle(436, 39, 4.792131235);
WriteAngle(angle);
WriteAngle(angle.Map(Angle.Ranges.NegativeStraightToStraight));

var a = new GeodeticCoord(Angle.RightAngle / 2, Angle.Parse("90"), 32031);
var b = a.ToEcef(EarthEllipsoid.Wgs84);
var c = b.ToGeodetic(EarthEllipsoid.Wgs84);
var d = CoordConversions.ToEcef(c, EarthEllipsoid.Wgs84);
var e = LatLon.Parse("30,114", default);
var projection = new GaussKrugerProjection(EarthEllipsoid.Wgs84, Angle.Parse("120"));
var f = e.ToCartesian(projection);
var g = f.ToLatLon(projection);
Console.WriteLine(e);
Console.WriteLine(f);
Console.WriteLine(g);
var duration = DateTime.Today - new DateTime(1980, 1, 6, 0, 0, 0);
Console.WriteLine(duration.TotalSeconds);

var today = new DateTimeOffset(DateTime.UtcNow.Date, TimeSpan.Zero);
Console.WriteLine(today);
Console.WriteLine(today.ToGpsTime());

static void WriteAngle(Angle angle)
{
    Console.WriteLine($"{nameof(angle)}: {angle}");
    Console.WriteLine($"{nameof(angle)}(F6): {angle:F6}");
    Console.WriteLine($"{nameof(angle)}(dms): {angle:dms}");
    Console.WriteLine($"{nameof(Angle.Degrees)}: {angle.Degrees}");
    Console.WriteLine($"{nameof(Angle.Minutes)}: {angle.Minutes}");
    Console.WriteLine($"{nameof(Angle.Seconds)}: {angle.Seconds}");
    Console.WriteLine($"{nameof(Angle.TotalDegrees)}: {angle.TotalDegrees}");
    Console.WriteLine($"{nameof(Angle.TotalMinutes)}: {angle.TotalMinutes}");
    Console.WriteLine($"{nameof(Angle.TotalSeconds)}: {angle.TotalSeconds}");
    Console.WriteLine();
}
