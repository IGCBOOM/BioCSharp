using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BioCSharp.Core.Sequence.Location.Template;
using BioCSharp.Core.Sequence.Template;

namespace BioCSharp.Core.Sequence.Storage
{
    public class JoiningSequenceReader<T> : IProxySequenceReader<T> where T : ICompound
    {

        private static readonly bool BINARY_SEARCH = true;
        private readonly List<ISequence<T>> _sequences;
        private readonly ICompoundSet<T> _compoundSet;
        private int[] _maxSequenceIndex;
        private int[] _minSequenceIndex;

        public JoiningSequenceReader(params ISequence<T>[] sequences) : this(sequences.ToList())
        {
            
        }

        public JoiningSequenceReader(List<ISequence<T>> sequences)
        {

            _sequences = GrepSequences(sequences);
            _compoundSet = GrepCompoundSet();

        }

        public JoiningSequenceReader(ICompoundSet<T> compoundSet, params ISequence<T>[] sequences) : this(compoundSet, sequences.ToList())
        {
            
        }

        public JoiningSequenceReader(ICompoundSet<T> compoundSet, List<ISequence<T>> sequences)
        {
            _sequences = GrepSequences(sequences);
            _compoundSet = compoundSet;
        }

        private List<ISequence<T>> GrepSequences(List<ISequence<T>> sequences)
        {

            List<ISequence<T>> seqs = new List<ISequence<T>>();
            foreach(ISequence<T> s in sequences)
            {

                if (s.GetLength() != 0)
                {
                    seqs.Add(s);
                }

            }

            return seqs;
        }

        private ICompoundSet<T> GrepCompoundSet()
        {

            if (!_sequences.Any())
            {
                throw new InvalidOperationException("Cannot get a CompoundSet because we have no sequences. Set during construction");
            }

            return _sequences[0].GetCompoundSet();

        }

        public IEnumerator<T> GetEnumerator()
        {
            


        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public AccessionID GetAccession()
        {
            throw new NotImplementedException();
        }

        public int GetLength()
        {

            int[] maxSeqIndex = GetMaxSequenceIndex();
            if (maxSeqIndex.Length == 0)
            {
                return 0;
            }

            return maxSeqIndex[maxSeqIndex.Length - 1];

        }

        public T GetCompoundAt(int position)
        {

            int sequenceIndex = GetSequenceIndex(position);
            ISequence<T> sequence = _sequences[sequenceIndex];
            int indexInSequence = (position - GetMinSequenceIndex()[sequenceIndex]) + 1;
            return sequence.GetCompoundAt(indexInSequence);

        }

        public int GetIndexOf(T compound)
        {
            throw new NotImplementedException();
        }

        public int GetLastIndexOf(T compound)
        {
            throw new NotImplementedException();
        }

        public string GetSequenceAsString()
        {
            throw new NotImplementedException();
        }

        public List<T> GetAsList()
        {
            throw new NotImplementedException();
        }

        public ISequenceView<T> GetSubSequence(int start, int end)
        {
            throw new NotImplementedException();
        }

        public ICompoundSet<T> GetCompoundSet()
        {
            return _compoundSet;
        }

        public int CountCompounds(params T[] compounds)
        {
            throw new NotImplementedException();
        }

        public ISequenceView<T> GetInverse()
        {
            throw new NotImplementedException();
        }

        public void SetCompoundSet(ICompoundSet<T> compoundSet)
        {
            throw new NotImplementedException();
        }

        public void SetContents(string sequence)
        {
            throw new NotImplementedException();
        }

        private int GetSequenceIndex(int position)
        {

            if (BINARY_SEARCH)
            {
                return BinarySearch(position);
            }
            else
            {
                return LinearSearch(position);
            }

        }

        private int[] GetMaxSequenceIndex()
        {

            if (_maxSequenceIndex == null)
            {
                InitSeqIndexes();
            }

            return _maxSequenceIndex;

        }

        private int[] GetMinSequenceIndex()
        {

            if (_minSequenceIndex == null)
            {
                InitSeqIndexes();
            }

            return _minSequenceIndex;

        }

        private void InitSeqIndexes()
        {

            _minSequenceIndex = new int[_sequences.Count];
            _maxSequenceIndex = new int[_sequences.Count];
            int currentMaxIndex = 0;
            int currentMinIndex = 1;
            int lastLength = 0;

            for (int i = 0; i < _sequences.Count; i++)
            {

                currentMinIndex += lastLength;
                currentMaxIndex += _sequences[i].GetLength();
                _minSequenceIndex[i] = currentMinIndex;
                _maxSequenceIndex[i] = currentMaxIndex;
                lastLength = _sequences[i].GetLength();

            }

        }

        private int LinearSearch(int position)
        {

            int[] minSeqIndex = GetMinSequenceIndex();
            int[] maxSeqIndex = GetMaxSequenceIndex();
            int length = minSeqIndex.Length;

            for (int i = 0; i < length; i++)
            {
                if (position >= minSeqIndex[i] && position <= maxSeqIndex[i])
                {
                    return i;
                }
            }

            throw new IndexOutOfRangeException("Given position " + position + " does not map into this Sequence");

        }

        private int BinarySearch(int position)
        {

            int[] minSeqIndex = GetMinSequenceIndex();
            int[] maxSeqIndex = GetMaxSequenceIndex();

            int low = 0;
            int high = minSeqIndex.Length - 1;

            while (low <= high)
            {

                int mid = (low + high) >> 1;

                int midMinPosition = minSeqIndex[mid];
                int midMaxPosition = maxSeqIndex[mid];

                if (midMinPosition < position && midMaxPosition < position)
                {
                    low = mid + 1;
                }
                else if (midMinPosition > position && midMaxPosition > position)
                {
                    high = mid - 1;
                }
                else
                {
                    return mid;
                }

            }

            throw new IndexOutOfRangeException("Given position " + position + " does not map into this Sequence");

        }

    }
}
