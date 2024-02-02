namespace MiraiNavi.Location.Contracts;

public interface IGeodeticCoord<TSelf> : ILatLon<TSelf> where TSelf : IGeodeticCoord<TSelf>
{
    #region Public Properties

    double H { get; set; }

    #endregion Public Properties
}
