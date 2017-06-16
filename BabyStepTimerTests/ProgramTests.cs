using Microsoft.VisualStudio.TestTools.UnitTesting;
using BabyStepTimer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace BabyStepTimer.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void ShowsTwoMinutesTimeWhenApplicationIsStarted()
        {
            string html = "";

            act(() =>
            {
                html = getProgramHtml();
            });

            Assert.IsTrue(html.Contains("02:00"), "Expected html to contain 02:00 (was: "+html+").");
        }

        delegate void ActCommands();
        private void act(ActCommands commands)
        {
            var controlThread = new Thread(() =>
            {
                Thread.Sleep(100);

                commands();

                click("quit");
            });
            controlThread.Start();

            Program.Main();
        }

        private string getProgramHtml()
        {
            var getHtmlFunc = new Func<string>(() => Program._webBrowser.DocumentText);
            if (Program._webBrowser.InvokeRequired)
                return (string) Program._webBrowser.Invoke(getHtmlFunc);
            return getHtmlFunc();
        }

        private void click(string command)
        {
            var clickAction = new Action<string>(cmd => Program._webBrowser.Navigate($"command://{cmd}/"));
            if (Program._webBrowser.InvokeRequired)
                Program._webBrowser.Invoke(clickAction, command);
            else
                clickAction(command);
        }
    }
}