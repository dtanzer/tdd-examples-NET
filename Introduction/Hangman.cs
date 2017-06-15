using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Introduction
{
    public class Hangman
    {
        private string word;
        private int wrongGuesses;
        private char guess;

        public string Hint { get { return Regex.Replace(word, "[^"+guess+"]", "_"); } }

        public int WrongGuesses { get { return wrongGuesses; } }

        public Hangman(string word)
        {
            this.word = word;
        }

        static void Main()
        {

        }

        public void Guess(char guess)
        {
            if(word.Contains(guess))
            {
                this.guess = guess;
            }
            else
            {
                wrongGuesses++;
            }
        }
    }
}
