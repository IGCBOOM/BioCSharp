using System;
using System.Collections.Generic;
using System.Text;

namespace BioCSharp.Core.Util
{
    public class Crc64Checksum
    {
        private const ulong Poly64 = 0xD800000000000000L;

        private static readonly ulong[] CrcTable = new ulong[256];

        private ulong _crc;

        static Crc64Checksum()
        {

            for (var i = 0; i < 256; ++i)
            {

                var part = (ulong)i;

                for (var j = 0; j < 8; ++j)
                {

                    part = ((part & 1) != 0) ? (part >> 1) ^ Poly64 : (part >> 1);
                    CrcTable[i] = part;

                }

            }

        }

        public void Update(int b)
        {

            var low = _crc >> 8;
            var high = CrcTable[(int) ((_crc ^ (ulong)b) & 0xFF)];
            _crc = low ^ high;

        }

        public void Update(byte[] b, int offset, int length)
        {

            for (var i = offset; i < length; ++i)
            {
                Update(b[i]);
            }

        }

        public void Update(string s)
        {

            var size = s.Length;
            for (int i = 0; i < size; ++i)
            {
                Update(s[i]);
            }

        }

        public ulong GetValue()
        {
            return _crc;
        }

        public override string ToString()
        {

            var a = _crc >> 4;
            var b = _crc & 0xF;
            var builder = new StringBuilder();
            builder.Append(a.ToString("x"));
            builder.Append(b.ToString("x"));
            for (int i = 16 - builder.Length; i > 0; --i)
            {
                builder.Insert(0, '0');
            }

            return builder.ToString().ToUpper();

        }

        public void Reset()
        {
            _crc = 0;
        }

    }
}
