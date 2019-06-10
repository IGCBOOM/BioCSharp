using System;
using System.Collections.Generic;
using System.Text;

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

        public AbstractLocation(IPoint start, IPoint end, Strand strand,
            bool circular, bool betweenCompounds,
            List<ILocation> subLocations) : this(start, end, strand, circular, betweenCompounds, null, subLocations);
        {
            
        }

    }
}
