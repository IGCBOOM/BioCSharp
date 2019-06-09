using System;
using System.Collections.Generic;
using System.Text;

namespace BioCSharp.Core.Exceptions
{
    class CompoundNotFoundException : Exception
    {

        public CompoundNotFoundException(string message) : base(message)
        {
        }

        public CompoundNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

    }
}
