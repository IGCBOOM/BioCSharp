using System;
using System.Collections.Generic;
using System.Text;

namespace BioCSharp.Core.Sequence
{

    public class Strand
    {

        private readonly string _stringRepresentation;
        private readonly int _numericRepresentation;

        public Strand(string stringRepresentation, int numericRepresentation)
        {

            _stringRepresentation = stringRepresentation;
            _numericRepresentation = numericRepresentation;

        }

        public int GetNumericRepresentation()
        {
            return _numericRepresentation;
        }

        public string GetStringRepresentation()
        {
            return _stringRepresentation;
        }

        public Strand GetReverse()
        {

            if (_stringRepresentation == "+" && _numericRepresentation == 1)
            {
                return new Strand("-", -1);
            }
            if (_stringRepresentation == "-" && _numericRepresentation == -1)
            {
                return new Strand("+", 1);
            }
            else
            {
                return new Strand(".", 0);
            }

        }

    }

}
