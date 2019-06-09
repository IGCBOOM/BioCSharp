using System;
using System.Collections.Generic;
using System.Text;

namespace BioCSharp.Core.Sequence.Template
{
    interface IAbstractSequence<T> : ISequence<T> where T : ICompound
    {



    }
}
