using System;
using System.Collections.Generic;
using System.Text;
using BioCSharp.Core.Sequence.Template;

namespace BioCSharp.Core.Sequence.Location.Template
{
    public interface IProxySequenceReader<T> : ISequenceReader<T> where T : ICompound
    {

    }
}
