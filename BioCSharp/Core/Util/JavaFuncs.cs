using System;
using System.Collections.Generic;
using System.Text;

namespace BioCSharp.Core.Util
{
    public unsafe class JavaFuncs
    {

        public static uint FloatToUInt32Bits(float f)
        {
            return *((uint*)&f);
        }

        public static int FloatToInt32Bits(float f)
        {
            return *((int*)&f);
        }

        public static ulong DoubleToUInt64Bits(double d)
        {
            return *((ulong*)&d);
        }

        public static long DoubleToInt64Bits(double d)
        {
            return *((long*)&d);
        }

    }
}
