using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BioCSharp.Core.Sequence.Location.Template;

namespace BioCSharp.Core.Sequence.Location
{
    public class SimpleLocation : AbstractLocation
    {

        private static readonly List<ILocation> EMPTY_LOCS = new List<ILocation>();

        public SimpleLocation(int start, int end) : this(new SimplePoint(start), new SimplePoint(end))
        {
            
        }

        public SimpleLocation(IPoint start, IPoint end) : this(start, end, new Strand("+", 1))
        {
            
        }

        public SimpleLocation(int start, int end, Strand strand) : this(new SimplePoint(start), new SimplePoint(end), strand)
        {
            
        }

        public SimpleLocation(int start, int end, Strand strand, List<ILocation> subLocations) : this(new SimplePoint(start), new SimplePoint(end), strand, subLocations)
        {
            
        }

        public SimpleLocation(IPoint start, IPoint end, Strand strand) : base(start, end, strand, false, false, new List<ILocation>())
        {
            
        }

        public SimpleLocation(IPoint start, IPoint end, Strand strand, AccessionID accession) : base(start, end, strand, false, false, accession, EMPTY_LOCS)
        {
            
        }

        public SimpleLocation(IPoint start, IPoint end, Strand strand, bool betweenCompounds, AccessionID accession) : base(start, end, strand, false, betweenCompounds, accession, EMPTY_LOCS)
        {
            
        }

        public SimpleLocation(IPoint start, IPoint end, Strand strand, bool circular, bool betweenBases) : base(start, end, strand, circular, betweenBases, EMPTY_LOCS)
        {
            
        }

        public SimpleLocation(int start, int end, Strand strand, params ILocation[] subLocations) : this(new SimplePoint(start), new SimplePoint(end), strand, subLocations)
        {
            
        }

        public SimpleLocation(IPoint start, IPoint end, Strand strand, params ILocation[] subLocations) : base(start, end, strand, false, false, subLocations.ToList())
        {
            
        }

        public SimpleLocation(IPoint start, IPoint end, Strand strand, bool circular, params ILocation[] subLocations) : base(start, end, strand, circular, false, subLocations.ToList())
        {
            
        }

        public SimpleLocation(IPoint start, IPoint end, Strand strand, bool circular, List<ILocation> subLocations) : base(start, end, strand, circular, false, subLocations)
        {
            
        }

        public SimpleLocation(IPoint start, IPoint end, Strand strand, List<ILocation> subLocations) : base(start, end, strand, false, false, subLocations)
        {
            
        }

        public SimpleLocation(IPoint start, IPoint end, Strand strand, bool circular, bool betweenBases, List<ILocation> subLocations) : base(start, end, strand, circular, betweenBases, subLocations)
        {
            
        }

    }
}
