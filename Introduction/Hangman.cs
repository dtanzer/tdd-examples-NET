using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Introduction
{
    public class Hangman
    {
        private string word;

        public string Hint { get { return Regex.Replace(word, ".", "_"); } }

        public Hangman(string word)
        {
            this.word = word;
        }

        public void Foo()
        {

        }

        static void Main()
        {

        }
    }
}
