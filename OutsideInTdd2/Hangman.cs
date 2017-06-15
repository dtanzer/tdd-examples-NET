using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutsideInTdd2
{
    public class Hangman
    {
        public virtual string Hint { get; private set; }

        public virtual void Guess(char guess)
        {
            throw new NotImplementedException();
        }
    }
}
