using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using BioCSharp.Core.Sequence.Template;

namespace BioCSharp.Core.Sequence.Location.Template
{
    [Serializable]
    public class AbstractLocation : ILocation
    {

        private static readonly long _serialVersionUID = 1L;

        private IPoint _start;
        private IPoint _end;
        private Strand _strand;
        private List<ILocation> _subLocations;
        private bool _circular;
        private bool _betweenCompounds;
        private AccessionID _accession;

        private bool _partialOn5prime = false;
        private bool _partialOn3prime = false;

        protected AbstractLocation() : base()
        {
        }

        public AbstractLocation(IPoint start, IPoint end, Strand strand, bool circular, bool betweenCompounds, List<ILocation> subLocations) : this(start, end, strand, circular, betweenCompounds, null, subLocations)
        {
            
        }

        public AbstractLocation(IPoint start, IPoint end, Strand strand,
            bool circular, bool betweenCompounds, AccessionID accession,
            List<ILocation> subLocations)
        {
            this._start = start;
            this._end = end;
            this._strand = strand;
            this._circular = circular;
            this._betweenCompounds = betweenCompounds;
            this._accession = accession;
            this._subLocations = subLocations == null ? null : new List<ILocation>(subLocations);
            AssertLocation();
        }

        protected void AssertLocation()
        {
            if (IsCircular() && !IsComplex())
            {
                throw new InvalidOperationException("Cannot have a circular "
                                                + "location which is not complex");
            }

            int st = GetStart().GetPosition();
            int e = GetEnd().GetPosition();

            if (st > e)
            {
                throw new InvalidOperationException(
                    $"Start {st} is greater than end {e}; " + "this is an incorrect format");
            }

            if (IsBetweenCompounds() && IsComplex())
            {
                throw new InvalidOperationException("Cannot have a complex location "
                                                + "which is located between a pair of compounds");
            }

            if (IsBetweenCompounds() && (st + 1) != e)
            {
                throw new InvalidOperationException(
                    string.Format("Start {1} is not next to end {1}", st, e));
            }

        }


        public IEnumerator<ILocation> GetEnumerator()
        {

            List<ILocation> list;

            if (IsComplex())
            {
                list = GetSubLocations();
            }
            else
            {
                list = new List<ILocation> {this};
            }

            return list.GetEnumerator();

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public AccessionID GetAccession()
        {
            return _accession;
        }

        public IPoint GetStart()
        {
            return _start;
        }

        public IPoint GetEnd()
        {
            return _end;
        }

        public int GetLength()
        {
            return (GetEnd().GetPosition() - GetStart().GetPosition()) + 1;

        }

        public Strand GetStrand()
        {
            return _strand;
        }

        public List<ILocation> GetSubLocations()
        {
            return _subLocations ?? new List<ILocation>();
        }

        public List<ILocation> GetRelevantSubLocations()
        {
            return GetAllSubLocations(this);
        }

        private List<ILocation> GetAllSubLocations(ILocation location)
        {

            List<ILocation> flatSubLocations = new List<ILocation>();
            foreach (var l in location.GetSubLocations())
            {
                if (l.IsComplex())
                {
                    flatSubLocations.AddRange(GetAllSubLocations(l));
                }
                else
                {
                    flatSubLocations.Add(l);
                }
            }
            return flatSubLocations;

        }


        public bool IsComplex()
        {
            return GetSubLocations().Any();
        }

        public bool IsPartialOn5prime()
        {
            return _partialOn5prime;
        }

        public void SetPartialOn5prime(bool partialOn5prime)
        {
            _partialOn5prime = partialOn5prime;
        }

        public bool IsPartialOn3prime()
        {
            return _partialOn3prime;
        }

        public void SetPartialOn3prime(bool partialOn3prime)
        {
            _partialOn3prime = partialOn3prime;
        }

        public bool IsPartial()
        {
            return _partialOn5prime || _partialOn3prime;
        }

        public bool IsCircular()
        {
            return _circular;
        }

        public bool IsBetweenCompounds()
        {
            return _betweenCompounds;
        }

        public ISequence<T> GetSubSequence<T>(ISequence<T> sequence) where T : ICompound
        {

            if (IsCircular())
            {
                List<ISequence<T>> sequences = new List<ISequence<T>>();
                foreach(var l in this)
                {
                    sequences.Add(l.GetSubSequence(sequence));
                }
                return new JoiningSequenceReader<T>(sequence.GetCompoundSet(), sequences);
            }
            return reverseSequence(sequence.getSubSequence(
                getStart().getPosition(), getEnd().getPosition()));

        }

        public ISequence<T> GetRelevantSubSequence<T>(ISequence<T> sequence) where T : ICompound
        {
            throw new NotImplementedException();
        }
    }
}
