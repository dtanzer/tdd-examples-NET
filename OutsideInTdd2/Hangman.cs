using System;

namespace OutsideInTdd2
{
    public class Hangman
    {
        public virtual string Hint { get; }

        public virtual void Guess(char v)
        {
        }
    }
}