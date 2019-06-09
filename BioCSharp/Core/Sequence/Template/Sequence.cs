using System;
using System.Collections.Generic;
using System.Text;

namespace BioCSharp.Core.Sequence.Template
{
    public interface ISequence<T> : IEnumerable<T>, IAccessioned where T : ICompound
    {

        int GetLength();

        T GetCompoundAt(int position);

        int GetIndexOf(T compound);

        int GetLastIndexOf(T compound);

        string GetSequenceAsString();

        List<T> GetAsList();

        ISequenceView<T> GetSubSequence(int start, int end);

        ICompoundSet<T> GetCompoundSet();

        int CountCompounds(params T[] compounds);

        ISequenceView<T> GetInverse();

    }
}
