using System;
using System.Collections.Generic;
using System.Text;
using BioCSharp.Core.Sequence.Template;

namespace BioCSharp.Core.Sequence.Location.Template
{
    public interface ILocation : IEnumerable<ILocation>, IAccessioned
    {

        IPoint GetStart();

        IPoint GetEnd();

        int GetLength();

        Strand GetStrand();

        List<ILocation> GetSubLocations();

        List<ILocation> GetRelevantSubLocations();

        bool IsComplex();

        bool IsCircular();

        bool IsBetweenCompounds();

        ISequence<T> GetSubSequence<T>(ISequence<T> sequence) where T : ICompound;

        ISequence<T> getRelevantSubSequence<T>(ISequence<T> sequence) where T : ICompound;

    }

    public static class Tools
    {

        public static ILocation Location(List<ILocation> locations, int? sequenceLength, string type)
        {

            type = type ?? "join";
            sequenceLength = sequenceLength ?? -1;

            return null;

        }

        public static ILocation Location(int start, int end, Strand strand, int length)
        {

            int min = Math.Min(start, end);

            bool isReverse = (min != start);

            if (isReverse)
            {
                
                return new SimpleLocation(
                    new SimplePoint(start).Reverse(length),
                    new SimplePoint(end).Reverse(length),
                    strand);

            }

            return new SimpleLocation(start, end, strand);

        }

        public static ILocation CircularLocation(int start, int end, Strand strand, int length)
        {

            int min = Math.Min(start, end);
            int max = Math.Max(start, end);

            bool isReverse = (min != start);

            if (min > length)
            {

                throw new ArgumentException("Cannot process a "
                                            + "location whose lowest coordinate is less than "
                                            + "the given length " + length);

            }

            if (max <= length)
            {
                return Location(start, end, strand, length);
            }

            int modStart = ModulateCircularIndex(start, length);
            int modEnd = ModulateCircularIndex(end, length);
            int numberOfPasses = CompleteCircularPasses(Math.Max(start, end), length);

            if (isReverse)
            {

                int reversedModStart = new SimplePoint(modStart).Reverse(length).GetPosition();
                int reversedModEnd = new SimplePoint(modEnd).Reverse(length).GetPosition();
                modStart = reversedModStart;
                modEnd = reversedModEnd;
                start = reversedModStart;
                end = (length * (numberOfPasses + 1)) + modEnd;

            }

            List<ILocation> locations = new List<ILocation>();
            locations.Add(new SimpleLocation(modStart, length, strand));
            for (int i = 0; i < numberOfPasses; i++)
            {
                locations.Add(new SimpleLocation(1, length, strand));
            }
            locations.Add(new SimpleLocation(1, modEnd, strand));
            return new SimpleLocation(new SimplePoint(start),
                new SimplePoint(end), strand, true, false, locations);

        }

        public static ILocation GetMax(List<ILocation> locations)
        {

            return ScanLocationsMax(locations);

        }

        private static ILocation ScanLocationsMax(List<ILocation> locations)
        {
            ILocation location = null;
            foreach (var l in locations)
            {
                if (location == null)
                {
                    location = l;
                }
                else
                {
                    if (AcceptMax(location, l))
                    {
                        location = l;
                    }
                }
            }
            return location;
        }

        private static ILocation ScanLocationsMin(List<ILocation> locations)
        {
            ILocation location = null;
            foreach (var l in locations)
            {
                if (location == null)
                {
                    location = l;
                }
                else
                {
                    if (AcceptMin(location, l))
                    {
                        location = l;
                    }
                }
            }
            return location;
        }

        public static int ModulateCircularIndex(int index, int seqLength)
        {
            
            if (seqLength == 0)
            {
                return index;
            }
            
            while (index > seqLength)
            {
                index -= seqLength;
            }

            return index;

        }


        public static int CompleteCircularPasses(int index, int seqLength)
        {

            int count = 0;
            while (index > seqLength)
            {
                count++;
                index -= seqLength;
            }
            return count - 1;

        }

        private static bool AcceptMax(ILocation previous, ILocation current)
        {
            int res = current.GetEnd().CompareTo(previous.GetEnd());
            return res > 0;
        }

        private static bool AcceptMin(ILocation previous, ILocation current)
        {
            int res = current.GetStart().CompareTo(previous.GetStart());
            return res < 0;
        }

    }

}
