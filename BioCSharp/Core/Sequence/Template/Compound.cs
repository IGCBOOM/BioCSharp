using System;
using System.Collections.Generic;
using System.Text;

namespace BioCSharp.Core.Sequence.Template
{
    public interface ICompound
    {

        bool EqualsIgnoreCase(ICompound compound);

        string GetDescription();

        void SetDescription(string description);

        string GetShortName();

        void SetShortName(string shortName);

        string GetLongName();

        void SetLongName(string longName);

        float? GetMolecularWeight();

        void SetMolecularWeight(float molecularWeight);

    }
}
