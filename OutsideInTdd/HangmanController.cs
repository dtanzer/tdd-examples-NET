using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutsideInTdd
{
    public class HangmanController
    {
        private IHangman _hangman;
        private IUi _ui;

        public HangmanController(IUi ui, IHangman hangman)
        {
            _ui = ui;
            _hangman = hangman;
        }

        public static void Main(string[] args)
        {
        }

        public void StartGame()
        {
            StringBuilder sb = new StringBuilder();

            foreach(var c in _hangman.Hint)
            {
                if(sb.Length > 0)
                {
                    sb.Append(" ");
                }

                if(c == null)
                {
                    sb.Append("_");
                }
            }

            _ui.Render(sb.ToString());
        }
    }
}
