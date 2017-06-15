using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OutsideInTdd2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OutsideInTdd2.Tests
{
    [TestClass()]
    public class DictionaryBotTests
    {
        [TestMethod()]
        public void PlaysNextSuggestionFromDictionaryAnalyzer()
        {
            var expectedGuess = 'e';
            var hangmanMock = new Mock<Hangman>();
            var analyzerMock = new Mock<DictionaryAnalyzer>();
            analyzerMock.Setup(analyzer => analyzer.NextSuggestion(It.IsAny<List<string>>(), It.IsAny<string>()))
                .Returns(new DictionaryAnalyzer.Suggestion()
                {
                    BestGuess = expectedGuess,
                    RemainingWords = new List <string> { }
                });

            DictionaryBot bot = new DictionaryBot(hangmanMock.Object, analyzerMock.Object, new List<string> { });
            bot.NextMove();

            hangmanMock.Verify(hangman => hangman.Guess(expectedGuess));
        }
    }
}