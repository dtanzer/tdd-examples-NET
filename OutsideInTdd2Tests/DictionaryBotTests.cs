using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OutsideInTdd2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutsideInTdd2.Tests
{
    [TestClass()]
    public class DictionaryBotTests
    {
        [TestMethod()]
        public void PlaySuggestionFromDictionaryAnalyzer()
        {
            
            char expectedNextMove = 'a';

            var hangmanMock = new Mock<Hangman>();
            var dictionaryAnalyzerMock = new Mock<DictionaryAnalyzer>();
            dictionaryAnalyzerMock.Setup(da => da.NextMove(It.IsAny<string>())).Returns(expectedNextMove);
            DictionaryBot bot = new DictionaryBot(hangmanMock.Object, dictionaryAnalyzerMock.Object);

            bot.NextMove();

            hangmanMock.Verify(hangman => hangman.Guess(expectedNextMove));
        }

        [TestMethod()]
        public void PlaySuggestionFromDictionaryAnalyzerWithHint()
        {

            char expectedNextMove = 'a';

            var hangmanMock = new Mock<Hangman>();
            var dictionaryAnalyzerMock = new Mock<DictionaryAnalyzer>();
            //dictionaryAnalyzerMock.Setup(da => da.NextMove(It.IsAny<string>())).Returns(expectedNextMove);
            hangmanMock.SetupGet(hangman => hangman.Hint).Returns("____");
            DictionaryBot bot = new DictionaryBot(hangmanMock.Object, dictionaryAnalyzerMock.Object);

            bot.NextMove();

            //hangmanMock.Verify(hangman => hangman.Guess(expectedNextMove));
            dictionaryAnalyzerMock.Verify(da => da.NextMove("____"));
            dictionaryAnalyzerMock.Verify(da => da.NextMove(It.Is<string>(hint => hint.Length==4)));
        }
    }
}