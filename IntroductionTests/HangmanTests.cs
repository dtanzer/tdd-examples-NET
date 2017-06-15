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
    }
}