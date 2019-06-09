using System;
using System.Collections.Generic;
using System.Linq;
using BioCSharp.Core.Exceptions;
using BioCSharp.Core.Util;

namespace BioCSharp.Core.Sequence.Template
{
    public abstract class AbstractCompoundSet<T> : ICompoundSet<T> where T : ICompound
    {

        private Dictionary<List<char>, T> _charSeqToCompound = new Dictionary<List<char>, T>();
        private int _maxCompoundCharSequenceLength = -1;
        private bool? _compoundStringLengthEqual = null;

        public Dictionary<T, HashSet<T>> equivalentsMap = new Dictionary<T, HashSet<T>>();

        protected void AddCompound(T compound, T lowerCasedCompound, IEnumerable<T> equivalents)
        {

            AddCompound(compound);
            AddCompound(lowerCasedCompound);

            AddEquivalent(compound, lowerCasedCompound);
            AddEquivalent(lowerCasedCompound, compound);

            foreach (var equivalent in equivalents)
            {

                AddEquivalent(compound, equivalent);
                AddEquivalent(equivalent, compound);
                AddEquivalent(lowerCasedCompound, equivalent);
                AddEquivalent(equivalent, lowerCasedCompound);

            }

        }

        protected void AddCompound(T compound, T lowerCasedCompound, params T[] equivalents)
        {

            List<T> equiv = equivalents.ToList();
            AddCompound(compound, lowerCasedCompound, equiv);

        }

        protected void AddEquivalent(T compound, T equivalent)
        {

            HashSet<T> s;
            try
            {
                equivalentsMap.TryGetValue(compound, out s);
            }
            catch (Exception)
            {
                s = null;
            }

            if (s == null)
            {

                s = new HashSet<T>();
                equivalentsMap[compound] = s;

            }

            s.Add(equivalent);

        }

        protected void AddCompound(T compound)
        {

            _charSeqToCompound[compound.ToString().ToList()] = compound;
            _maxCompoundCharSequenceLength = -1;
            _compoundStringLengthEqual = null;

        }

        public int GetMaxSingleCompoundStringLength()
        {

            if (_maxCompoundCharSequenceLength == -1)
            {
                foreach (var compound in _charSeqToCompound.Values)
                {

                    int size = GetStringForCompound(compound).Length;
                    if (size > _maxCompoundCharSequenceLength)
                    {
                        _maxCompoundCharSequenceLength = size;
                    }

                }
            }

            return _maxCompoundCharSequenceLength;

        }

        public bool? IsCompoundStringLengthEqual()
        {

            if (_compoundStringLengthEqual == null)
            {

                int lastsize = -1;
                _compoundStringLengthEqual = true;
                foreach (var c in _charSeqToCompound.Keys)
                {

                    if (lastsize != c.Count)
                    {

                        _compoundStringLengthEqual = false;

                    }

                }

            }

            return _compoundStringLengthEqual;

        }

        public T GetCompoundForString(string text)
        {

            if (text == null)
            {
                throw new ArgumentException("Given a null CharSequence to process.");
            }

            if (text.Length == 0)
            {
                return default(T);
            }

            if (text.Length > GetMaxSingleCompoundStringLength())
            {
                throw new ArgumentException("CharSequence supplied is too long.");
            }

            return _charSeqToCompound[text.ToList()];

        }

        public string GetStringForCompound(T compound)
        {
            return compound.ToString();
        }

        public bool CompoundsEquivalent(T compoundOne, T compoundTwo)
        {
            
            AssertCompound(compoundOne);
            AssertCompound(compoundTwo);

            return compoundOne.Equals(compoundTwo) || equivalentsMap[compoundOne].Contains(compoundTwo);

        }

        public bool IsValidSequence(ISequence<T> sequence)
        {

            foreach (var compound in sequence)
            {
                if (!HasCompound(compound))
                {
                    return false;
                }
            }

            return true;

        }

        public HashSet<T> GetEquivalentCompounds(T compound)
        {
            return equivalentsMap[compound];
        }

        public bool compoundsEqual(T compoundOne, T compoundTwo)
        {
            AssertCompound(compoundOne);
            AssertCompound(compoundTwo);
            return compoundOne.EqualsIgnoreCase(compoundTwo);
        }


        public bool HasCompound(T compound)
        {

            T retrievedCompound = GetCompoundForString(compound.ToString());
            return retrievedCompound != null;

        }

        public List<T> GetAllCompounds()
        {
            return new List<T>(_charSeqToCompound.Values);
        }

        public bool IsComplementable()
        {
            return false;
        }

        private void AssertCompound(T compound)
        {

            if (!HasCompound(compound))
            {
                
                throw new CompoundNotFoundException("The CompoundSet " + GetType() + " knows nothing about the compound " + compound);

            }

        }

        public override int GetHashCode()
        {

            int s = HashCoder.Seed;
            s = HashCoder.Hash(s, _charSeqToCompound);
            s = HashCoder.Hash(s, equivalentsMap);

            return s;

        }

        public override bool Equals(object obj)
        {

            if (!(obj is AbstractCompoundSet<T>))
            {
                return false;
            }

            if (this.GetType() == obj.GetType())
            {

                AbstractCompoundSet<T> that = (AbstractCompoundSet<T>)obj;
                return _charSeqToCompound == that._charSeqToCompound && equivalentsMap == that.equivalentsMap;

            }

            return false;

        }
    }
}
