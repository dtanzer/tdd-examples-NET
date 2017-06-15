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
        private DictionaryAnalyzer.Suggestion lastSuggestion;

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
            string hint = hangman.Hint;
            var remainingWords = knownWords;
            if(this.lastSuggestion != null)
            {
                remainingWords = lastSuggestion.RemainingWords;
            }
            this.lastSuggestion = analyzer.NextSuggestion(remainingWords, hint);
            hangman.Guess(this.lastSuggestion.BestGuess);
        }
    }
}
