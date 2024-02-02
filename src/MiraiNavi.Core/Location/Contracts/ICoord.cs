using System.Numerics;

namespace MiraiNavi.Location.Contracts;

public interface ICoord<TSelf> :
    IEquatable<TSelf>,
    IEqualityOperators<TSelf, TSelf, bool>,
    IFormattable,
    IParsable<TSelf>
    where TSelf : ICoord<TSelf>
{
    #region Public Properties

    abstract static TSelf Origin { get; }

    #endregion Public Properties
}
