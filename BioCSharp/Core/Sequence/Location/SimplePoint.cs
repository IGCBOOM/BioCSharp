using System;
using System.Collections.Generic;
using System.Text;
using BioCSharp.Core.Sequence.Location.Template;
using BioCSharp.Core.Util;

namespace BioCSharp.Core.Sequence.Location
{
    [Serializable]
    public class SimplePoint : IPoint
    {

        private const long _serialVersionUID = 1L;

        private int _position;
        private bool _unknown;
        private bool _uncertain;

        protected SimplePoint()
        {
        }

        public SimplePoint(int position)
        {
            _position = position;
        }

        public SimplePoint(int position, bool unknown, bool uncertain)
        {

            _position = position;
            _unknown = unknown;
            _uncertain = uncertain;

        }

        public int CompareTo(IPoint other)
        {
            return GetPosition().CompareTo(other.GetPosition());
        }

        public int GetPosition()
        {
            return _position;
        }

        protected void SetPosition(int position)
        {
            _position = position;
        }

        public bool IsUnknown()
        {
            return _unknown;
        }

        protected void SetUnknown(bool unknown)
        {
            _unknown = unknown;
        }

        public bool IsUncertain()
        {
            return _uncertain;
        }

        protected void SetUncertain(bool uncertain)
        {
            _uncertain = uncertain;
        }

        public IPoint Reverse(int length)
        {

            int translatedPosition = Reverse(GetPosition(), length);
            return new SimplePoint(translatedPosition, IsUnknown(), IsUncertain());

        }

        public IPoint Offset(int distance)
        {

            int offsetPosition = GetPosition() + distance;
            return new SimplePoint(offsetPosition, IsUnknown(), IsUncertain());

        }

        protected int Reverse(int position, int length)
        {
            return (length - position) + 1;
        }

        public override bool Equals(object obj)
        {

            bool equals = false;
            if (GetType() == obj.GetType())
            {

                SimplePoint p = (SimplePoint) obj;
                equals = GetPosition() == p.GetPosition() &&
                         IsUncertain() == p.IsUncertain() &&
                         IsUnknown() == p.IsUnknown();

            }

            return equals;

        }

        public override int GetHashCode()
        {

            int r = HashCoder.Seed;
            r = HashCoder.Hash(r, GetPosition());
            r = HashCoder.Hash(r, IsUncertain());
            r = HashCoder.Hash(r, IsUnknown());
            return r;

        }

        public override string ToString()
        {
            return GetPosition().ToString();
        }

        public bool IsLower(IPoint point)
        {
            return CompareTo(point) < 0;
        }

        public bool IsHigher(IPoint point)
        {
            return CompareTo(point) > 0;
        }

        public IPoint ClonePoint()
        {
            return this.Offset(0);
        }
    }
}
