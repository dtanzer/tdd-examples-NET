using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartWithRuleSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartWithRuleSet.Tests
{
    [TestClass()]
    public class HangmanTests
    {
        [TestMethod()]

        public void ShowsUnderscoreWhenWordIsA()
        {
            Hangman hangman = new Hangman("a");
            Assert.AreEqual("_", hangman.Output);
        }

        [TestMethod()]
        [Ignore]
        public void GameWonWhenLastLetterGuessedOK()
        {
            Hangman hangman = new Hangman("a");
            hangman.GuessInput('a');
            Assert.IsTrue(hangman.IsSuccessful);
        }

        [TestMethod()]
        public void ShowsTwoUnderscoreWhenWordIsNo()
        {
            Hangman hangman = new Hangman("no");
            Assert.AreEqual("__", hangman.Output);
        }

        [TestMethod]
        public void ShowGuessedCharacterInOutputString()
        {
            Hangman hangman = new Hangman("test");
            hangman.GuessInput('t');

            Assert.AreEqual("t__t", hangman.Output);
        }

        [TestMethod]
        public void ShowSecretWhenAllGuessesAreCorrect()
        {
            Hangman hangman = new Hangman("test");
            hangman.GuessInput('t');
            hangman.GuessInput('e');
            hangman.GuessInput('s');
            Assert.AreEqual("test", hangman.Output);
        }

        [TestMethod]
        public void IncreaseIncorrectGuessCountWhenGuessingIncorrectly()
        {
            Hangman hangman = new Hangman("foobar");
            hangman.GuessInput('x');
            Assert.AreEqual(1, hangman.IncorrectGuessCount);
        }

    }
}