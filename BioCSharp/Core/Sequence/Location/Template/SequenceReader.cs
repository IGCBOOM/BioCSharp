using System;
using System.Collections.Generic;
using System.Text;
using BioCSharp.Core.Sequence.Template;

namespace BioCSharp.Core.Sequence.Location.Template
{
    public interface ISequenceReader<T> : ISequence<T> where T : ICompound
    {

        void SetCompoundSet(ICompoundSet<T> compoundSet);

        void SetContents(string sequence);


    }
}
