using System.Numerics;

namespace MiraiNavi.Location.Contracts;

public interface ICartesianCoord2<TSelf> :
    ICoord<TSelf>,
    IAdditionOperators<TSelf, TSelf, TSelf>,
    IDivisionOperators<TSelf, double, TSelf>,
    IMultiplyOperators<TSelf, double, TSelf>,
    ISubtractionOperators<TSelf, TSelf, TSelf>,
    IUnaryPlusOperators<TSelf, TSelf>,
    IUnaryNegationOperators<TSelf, TSelf>
    where TSelf : ICartesianCoord2<TSelf>
{
    double X { get; set; }

    double Y { get; set; }
}
