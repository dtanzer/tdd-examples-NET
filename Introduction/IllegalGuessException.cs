using System;

namespace Introduction
{
    public class IllegalGuessException : Exception
    {
        public IllegalGuessException(string message) : base(message)
        {
        }
    }
}