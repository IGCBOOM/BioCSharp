using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BioCSharp.Core.Sequence.Location.Template
{
    public interface IPoint : IComparable<IPoint>
    {

        int GetPosition();

        bool IsUnknown();

        bool IsUncertain();

        IPoint Reverse(int length);

        IPoint Offset(int distance);

        bool IsLower(IPoint point);

        bool IsHigher(IPoint point);

        IPoint ClonePoint();

    }

    public interface Resolver<T> where T : IPoint
    {
        int resolve(T point);
    }

}
