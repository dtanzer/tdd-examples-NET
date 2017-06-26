using Microsoft.VisualStudio.TestTools.UnitTesting;
using BabyStepTimer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace BabyStepTimer.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        public void Click(string command)
        {
            if (Program.WebBrowser.InvokeRequired)
                Program.WebBrowser.Invoke(new Action(() => Program.WebBrowser.Navigate($"command://{command}/")));
            else
                Program.WebBrowser.Navigate($"command://{command}/");
        }

        public void RunTimerCommands(Action p)
        {
            Task.Run(() => {
                Thread.Sleep(100);
                p();
                Click("quit");
            });
            Program.Main();
        }

        public string GetDisplayedText()
        {
            Func<string> getDocumentText = () => Program.WebBrowser.DocumentText;
            if (Program.WebBrowser.InvokeRequired)
                return (string)Program.WebBrowser.Invoke(getDocumentText);
            return getDocumentText();
        }

        public void AssertRemainingTimeIs(string displayedText, string remainingTime)
        {
            Assert.IsTrue(displayedText.Contains(remainingTime), $"Expected remaining time of {remainingTime} in {displayedText}");
        }

        public void AdvanceTimeBy(int millis)
        {
            Thread.Sleep(millis);
        }

        [TestMethod()]
        [Timeout(1000)]
        public void QuitButtonStopsApplication()
        {
            RunTimerCommands(()=> { });                     
        }

        [TestMethod()]
        [Timeout(3*1000)]
        public void CountdownAfterClickingStart()
        {
            string timeNow = String.Empty;
            RunTimerCommands(() => {
                Click("start");
                AdvanceTimeBy(1000);
                timeNow = GetDisplayedText();               
            });

            AssertRemainingTimeIs(timeNow, "1:59");
        }

        [TestMethod()]
        [Timeout(3 * 1000)]
        public void CountdownSchows2MinutesAfterClickingStart()
        {
            string timeNow = String.Empty;
            RunTimerCommands(() => {
                Click("start");
                AdvanceTimeBy(100);
                timeNow = GetDisplayedText();
            });

            AssertRemainingTimeIs(timeNow, "2:00");
        }

    }
}