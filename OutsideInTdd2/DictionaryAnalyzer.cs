using System;
using System.Collections.Generic;

namespace OutsideInTdd2
{
    public class DictionaryAnalyzer
    {
        public virtual Suggestion NextSuggestion(List<string> list, string hint)
        {
            throw new NotImplementedException();
        }

        public class Suggestion
        {
            public char BestGuess { get; set;  }
            public List<string> RemainingWords { get; set; }
        }
    }
}