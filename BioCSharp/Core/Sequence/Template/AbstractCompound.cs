using System;
using System.Collections.Generic;
using System.Text;

namespace BioCSharp.Core.Sequence.Template
{
    public abstract class AbstractCompound : ICompound
    {

        private readonly string _base;
        private readonly string _upperedBase;
        private string _shortName = null;
        private string _longName = null;
        private string _description = null;
        private float? _molecularWeight = null;

        public AbstractCompound()
        {

            _base = null;
            _upperedBase = _base.ToUpper();

        }

        public AbstractCompound(string Base)
        {

            _base = Base;
            _upperedBase = _base.ToUpper();

        }

        public string GetBase()
        {
            return _base;
        }

        public string GetUpperedBase()
        {
            return _upperedBase;
        }

        public string GetDescription()
        {
            return _description;
        }

        public void SetDescription(string description)
        {
            _description = description;
        }

        public string GetShortName()
        {
            return _shortName;
        }

        public void SetShortName(string shortName)
        {
            _shortName = shortName;
        }

        public string GetLongName()
        {
            return _longName;
        }

        public void SetLongName(string longName)
        {
            _longName = longName;
        }

        public float? GetMolecularWeight()
        {
            return _molecularWeight;
        }

        public void SetMolecularWeight(float molecularWeight)
        {
            _molecularWeight = molecularWeight;
        }

        public override string ToString()
        {
            return _base;
        }

        public override bool Equals(object obj)
        {

            if (obj == null)
            {
                return false;
            }

            if (!(obj is AbstractCompound))
            {
                return false;
            }

            AbstractCompound them = (AbstractCompound) obj;

            return _base.Equals(them._base);

        }

        public override int GetHashCode()
        {
            return _base.GetHashCode();
        }

        public bool EqualsIgnoreCase(ICompound compound)
        {

            if (compound == null)
            {
                return false;
            }

            if (!(compound is AbstractCompound)) {
                return false;
            }

            AbstractCompound them = (AbstractCompound) compound;

            return _base.Equals(them._base, StringComparison.InvariantCultureIgnoreCase);

        }

    }

}
