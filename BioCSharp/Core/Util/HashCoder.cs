using System;
using static BioCSharp.Core.Util.JavaFuncs;

namespace BioCSharp.Core.Util
{
    public class HashCoder
    {

        public static readonly int Seed = 9;
        public static readonly int Prime = 79;

        public static int Hash(int seed, bool b)
        {
            return (Prime * seed) + (b ? 1 : 0);
        }

        public static int Hash(int seed, char c)
        {
            return (Prime * seed) + c;
        }

        public static int Hash(int seed, int i)
        {
            return (Prime * seed) + i;
        }

        public static int Hash(int seed, ulong l)
        {
            return (Prime * seed) + (int) (l ^ (l >> 32));
        }

        public static int Hash(int seed, float f)
        {
            return Hash(seed, FloatToInt32Bits(f));
        }

        public static int Hash(int seed, double d)
        {
            return Hash(seed, DoubleToUInt64Bits(d));
        }

        public static int Hash(int seed, object o)
        {

            var result = seed;

            if (o == null)
            {
                result = Hash(result, 0);
            }

            if (!o.GetType().IsArray)
            {
                result = Hash(result, o.GetHashCode());
            }
            else
            {

                Array objectArray = (Array) o;
                for (int i = 0; i < objectArray.Length; i++)
                {

                    object item = objectArray.GetValue(i);
                    result = Hash(result, item);

                }

            }

            return result;

        }

    }
}
