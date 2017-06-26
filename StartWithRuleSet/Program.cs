using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartWithRuleSet
{
    public class Hangman
    {
        private string secretWord;

        private ISet<char> guessedCharacters;

        public Hangman(string secret)
        {
            guessedCharacters = new HashSet<char>();
            secretWord = secret;
        }

        public string Output {
            get
            {
                string result = "";
                foreach(char c in secretWord)
                {
                    if (guessedCharacters.Contains(c))
                    {
                        result += c;
                    }
                    else
                    {
                        result += "_";
                    }
                }

                return result;
            }
        }

        public bool IsSuccessful { get; set; }
        public int IncorrectGuessCount { get; private set; }

        public static void Main(string[] args)
        {
        }

        public void GuessInput(char guess)
        {
            if (secretWord.Contains(guess))
            {
                guessedCharacters.Add(guess);
            }
            else
            {
                ++IncorrectGuessCount;
            }
        }
    }
}
