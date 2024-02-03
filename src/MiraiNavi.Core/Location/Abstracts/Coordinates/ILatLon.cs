namespace MiraiNavi.Location.Abstracts;

public interface ILatLon<TSelf> : ICoord<TSelf> where TSelf : ILatLon<TSelf>
{
    #region Public Properties

    Angle Latitude { get; set; }

    Angle Longitude { get; set; }

    #endregion Public Properties
}