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
        private DictionaryAnalyzer analyzer;
        private List<string> knownWords;

        public DictionaryBot(Hangman hangman, DictionaryAnalyzer analyzer, List<string> knownWords)
        {
            this.hangman = hangman;
            this.analyzer = analyzer;
            this.knownWords = knownWords;
        }

        public static void Main(string[] args)
        {
        }

        public void NextMove()
        {
            var suggestion = analyzer.NextSuggestion(null, null);
            hangman.Guess(suggestion.BestGuess);
        }
    }
}
