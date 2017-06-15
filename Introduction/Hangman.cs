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

        public enum GameStatus
        {
            UNDEFINED,
            RUNNING,
            LOST,
            WON
        }

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

        public GameStatus Status { get; private set; }

        public Hangman(string word)
        {
            this.word = word;
            this.Status = GameStatus.RUNNING;
        }

        static void Main()
        {

        }

        public void Guess(char guess)
        {
            if(this.Status != GameStatus.RUNNING)
            {
                throw new IllegalGuessException("Illegal guess while game is not running:\"" + guess + "\".");
            }
            if(word.Contains(guess))
            {
                this.guesses += guess;
                if(!this.Hint.Contains('_'))
                {
                    this.Status = GameStatus.WON;
                }
            }
            else
            {
                wrongGuesses++;
                if(wrongGuesses == 11)
                {
                    this.Status = GameStatus.LOST;
                }
            }
        }
    }
}
