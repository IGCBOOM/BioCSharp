using System;
using System.Collections.Generic;
using System.Text;

namespace BioCSharp.Core.Sequence.Template
{
    public interface ICompoundSet<T> where T : ICompound
    {

        int GetMaxSingleCompoundStringLength();

        bool? IsCompoundStringLengthEqual();

        T GetCompoundForString(string text);

        string GetStringForCompound(T compound);

        bool CompoundsEquivalent(T compoundOne, T compoundTwo);

        bool IsValidSequence(ISequence<T> sequence);

        HashSet<T> GetEquivalentCompounds(T compound);

        bool HasCompound(T compound);

        List<T> GetAllCompounds();

        bool IsComplementable();

    }
}
