using Introduction;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OutsideInTdd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutsideInTdd.Tests
{
    [TestClass()]
    public class HangmanControllerTests
    {
        [TestMethod()]
        public void HangmanShowsHintAfterStartGame()
        {
            // Stub
            var ui = new HangmanUiMock();
            var hangman = new HangmanStub();

            var hangmanController = new HangmanController(ui, hangman);

            //Execute
            hangmanController.StartGame();

            // Verify
            ui.VerfiyRenderWasCalled("_ _ _");
        }

        [TestMethod()]
        public void HangmanShowsHintAfterStartGameWithLongHint()
        {
            // Stub
            var ui = new HangmanUiMock();
            var hangman = new HangmanStub();
            hangman.SetHint(new char?[4]);

            var hangmanController = new HangmanController(ui, hangman);
            
            //Execute
            hangmanController.StartGame();

            // Verify
            ui.VerfiyRenderWasCalled("_ _ _ _");
        }
    }

    public class HangmanStub : IHangman
    {
        public char?[] Hint => _Hint;
        private char?[] _Hint = new char?[3];


        internal void SetHint(char?[] v)
        {
            _Hint = v;
        }
    }

    public class HangmanUiMock : IUi
    {
        private string _hint;

        public void Render(string hint)
        {
            _hint = hint;
        }

        internal void VerfiyRenderWasCalled(string expectedHint)
        {
            if (expectedHint.Equals(_hint)) return;

            Assert.Fail("Expected Render() has been called; but was not");
        }
    }
}