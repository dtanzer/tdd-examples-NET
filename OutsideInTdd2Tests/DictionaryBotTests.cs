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

        [TestMethod()]
        public void PassesHintFromHangmanToDictionaryAnalyzer()
        {
            var hangmanMock = new Mock<Hangman>();
            hangmanMock.SetupGet(hangman => hangman.Hint).Returns("_sug__");
            var analyzerMock = new Mock<DictionaryAnalyzer>();
            analyzerMock.Setup(analyzer => analyzer.NextSuggestion(It.IsAny<List<string>>(), It.IsAny<string>()))
                .Returns(new DictionaryAnalyzer.Suggestion()
                {
                    BestGuess = 'i',
                    RemainingWords = new List<string> { }
                });

            DictionaryBot bot = new DictionaryBot(hangmanMock.Object, analyzerMock.Object, new List<string> { });
            bot.NextMove();

            analyzerMock.Verify(analyzer => analyzer.NextSuggestion(
                It.IsAny<List<string>>(), It.Is<string>(s => s.Equals("_sug__"))));
        }

        [TestMethod()]
        public void PassesKnownWordsToDictionaryAnalyzerOnFirstRun()
        {
            var hangmanMock = new Mock<Hangman>();
            hangmanMock.SetupGet(hangman => hangman.Hint).Returns("_sug__");
            var analyzerMock = new Mock<DictionaryAnalyzer>();
            analyzerMock.Setup(analyzer => analyzer.NextSuggestion(It.IsAny<List<string>>(), It.IsAny<string>()))
                .Returns(new DictionaryAnalyzer.Suggestion()
                {
                    BestGuess = 'i',
                    RemainingWords = new List<string> { }
                });

            var knownWords = new List<string> { "abc", "defg" };
            DictionaryBot bot = new DictionaryBot(hangmanMock.Object, analyzerMock.Object, knownWords);
            bot.NextMove();

            analyzerMock.Verify(analyzer => analyzer.NextSuggestion(
                It.Is<List<string>>(l => l == knownWords), It.IsAny<string>()));
        }

        [TestMethod()]
        public void PassesRemainingWordsFromLastSuggestionToToDictionaryAnalyzer()
        {
            var hangmanMock = new Mock<Hangman>();
            hangmanMock.SetupGet(hangman => hangman.Hint).Returns("_sug__");

            var expectedRemainingWords = new List<string> { "abc" };
            var analyzerMock = new Mock<DictionaryAnalyzer>();
            analyzerMock.Setup(analyzer => analyzer.NextSuggestion(It.IsAny<List<string>>(), It.IsAny<string>()))
                .Returns(new DictionaryAnalyzer.Suggestion()
                {
                    BestGuess = 'i',
                    RemainingWords = expectedRemainingWords
                });

            DictionaryBot bot = new DictionaryBot(hangmanMock.Object, analyzerMock.Object, new List<string> { });
            bot.NextMove();
            bot.NextMove();

            analyzerMock.Verify(analyzer => analyzer.NextSuggestion(
                It.Is<List<string>>(l => l == expectedRemainingWords), It.IsAny<string>()));
        }
    }
}