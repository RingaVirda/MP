using System;

namespace Lab1
{
    public class ValueException : Exception
    {
        public ValueException(string msg)
            : base(msg)
        { }
    }
}