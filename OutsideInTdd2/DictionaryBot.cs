using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutsideInTdd2
{
    public class DictionaryBot
    {
        private Hangman hangman;
        private DictionaryAnalyzer dictionaryAnalyzer;

        public DictionaryBot(Hangman hangman, DictionaryAnalyzer dictionaryAnalyzer)
        {
            this.hangman = hangman;
            this.dictionaryAnalyzer = dictionaryAnalyzer;
        }

        public static void Main(string[] args)
        {
        }

        public void NextMove()
        {
            var nextMove = dictionaryAnalyzer.NextMove(hangman.Hint);
            hangman.Guess(nextMove);
        }
    }
}
