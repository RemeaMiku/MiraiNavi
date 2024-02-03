namespace MiraiNavi.Location.Abstracts;

public interface ICartesianCoord3<TSelf> : ICartesianCoord2<TSelf> where TSelf : ICartesianCoord3<TSelf>
{
    #region Public Properties

    double Z { get; set; }

    #endregion Public Properties
}
