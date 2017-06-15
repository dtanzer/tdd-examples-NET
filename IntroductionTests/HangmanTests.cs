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
    }
}