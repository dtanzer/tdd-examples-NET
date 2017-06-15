using Microsoft.VisualStudio.TestTools.UnitTesting;
using Introduction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Introduction.Tests
{
    [TestClass()]
    public class HangmanTests
    {
        [TestMethod()]
        public void ShowsUnderscoreForEachLetterInWordOnStart()
        {
            string word = "some test word";

            Hangman hangman = new Hangman(word);

            string expected = Regex.Replace(word, ".", "_");
            Assert.AreEqual(expected, hangman.Hint);
        }

        [TestMethod()]
        public void IncrementsNumberOfWrongGuessesWhenGuessedLetterIsNotInWord()
        {
            string word = "a";
            char notInWord = 'b';

            Hangman hangman = new Hangman(word);
            int wrongGuessesBefore = hangman.WrongGuesses;

            hangman.Guess(notInWord);

            Assert.AreEqual(wrongGuessesBefore + 1, hangman.WrongGuesses);
        }

        [TestMethod()]
        public void DoesNotIncrementNumberOfWrongGuessesWhenGuessedLetterIsInWord()
        {
            string word = "a";
            char inWord = 'a';

            Hangman hangman = new Hangman(word);
            int wrongGuessesBefore = hangman.WrongGuesses;

            hangman.Guess(inWord);

            Assert.AreEqual(wrongGuessesBefore, hangman.WrongGuesses);
        }

        [TestMethod()]
        public void ShowsAllOccurancesOfLetterOnRightGuess()
        {
            string word = "abcba";
            char inWord = 'b';

            Hangman hangman = new Hangman(word);
            hangman.Guess(inWord);

            Assert.AreEqual("_b_b_", hangman.Hint);
        }

        [TestMethod()]
        public void ShowsAllOccurancesOfAllCorrectlyGuessesLetters()
        {
            string word = "abcba";
            char inWord1 = 'a';
            char inWord2 = 'b';

            Hangman hangman = new Hangman(word);
            hangman.Guess(inWord1);
            hangman.Guess(inWord2);

            Assert.AreEqual("ab_ba", hangman.Hint);
        }
    }
}