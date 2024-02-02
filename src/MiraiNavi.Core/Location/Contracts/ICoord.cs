using System.Numerics;

namespace MiraiNavi.Location.Contracts;

public interface ICoord<TSelf> :
    IEquatable<TSelf>,
    IEqualityOperators<TSelf, TSelf, bool>,
    IFormattable,
    IParsable<TSelf>
    where TSelf : ICoord<TSelf>
{
    abstract static TSelf Origin { get; }
}
