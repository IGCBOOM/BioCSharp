using System;
using System.Collections.Generic;
using System.Text;

namespace BioCSharp.Core.Sequence.Template
{
    public interface ISequenceView<T> : ISequence<T> where T : ICompound
    {

        ISequence<T> GetViewedSequence();

        int GetBioStart();

        int GetBioEnd();

    }
}
