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
        private string guesses = "";

        public string Hint
        {
            get
            {
                if(guesses == "")
                {
                    return Regex.Replace(word, ".", "_");
                }
                return Regex.Replace(word, "[^"+guesses+"]", "_");
            }
        }

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
                this.guesses += guess;
            }
            else
            {
                wrongGuesses++;
            }
        }
    }
}
