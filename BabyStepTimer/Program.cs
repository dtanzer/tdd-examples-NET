﻿using System;
using System.Drawing;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace BabyStepTimer
{
    public static class Program
    {
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        private static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const string BackgroundColorNeutral = "#ffffff";
        private const string BackgroundColorFailed = "#ffcccc";
        private const string BackgroundColorPassed = "#ccffcc";

        private const long SecondsInCycle = 120;

        private static Form _mainForm;
        public static WebBrowser WebBrowser;
        private static bool _timerRunning;
        private static DateTime _currentCycleStartTime;
        private static string _lastRemainingTime;
        private static string _bodyBackgroundColor = BackgroundColorNeutral;

        private const string TwoDigitsFormat = "00";

        [STAThread]
        public static void Main()
        {
            _mainForm = new Form
            {
                Text = "Babysteps Timer",
                FormBorderStyle = FormBorderStyle.None,
                Size = new Size(250, 120)
            };

            WebBrowser = new WebBrowser();
            WebBrowser.ScrollBarsEnabled = false;
            WebBrowser.DocumentText = CreateTimerHtml(getRemainingTimeCaption(TimeSpan.FromSeconds(0L)), BackgroundColorNeutral, false);

            WebBrowser.Document.MouseDown += (sender, e) =>
            {
                var element = WebBrowser.Document.GetElementFromPoint(e.ClientMousePosition);
                if (element.TagName == "BODY" && e.MouseButtonsPressed == MouseButtons.Left)
                {
                    ReleaseCapture();
                    SendMessage(_mainForm.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
                }
            };

            WebBrowser.Navigating += (sender, args) =>
            {
                if (args.Url.AbsoluteUri == "command://start/")
                {
                    _mainForm.TopMost = true;
                    WebBrowser.Document.OpenNew(false);
                    WebBrowser.Document.Write(CreateTimerHtml(getRemainingTimeCaption(TimeSpan.FromMilliseconds(0)), BackgroundColorNeutral, true));

                    ThreadStart start = () =>
                    {
                        _timerRunning = true;
                        _currentCycleStartTime = DateTime.Now;

                        while (_timerRunning)
                        {
                            TimeSpan elapsedTime = DateTime.Now - _currentCycleStartTime;

                            if (elapsedTime.TotalMilliseconds >= SecondsInCycle * 1000 + 980)
                            {
                                _currentCycleStartTime = DateTime.Now;
                                elapsedTime = DateTime.Now - _currentCycleStartTime;
                            }
                            if (elapsedTime.TotalMilliseconds >= 5000 && elapsedTime.TotalMilliseconds < 6000 && _bodyBackgroundColor != BackgroundColorNeutral)
                            {
                                _bodyBackgroundColor = BackgroundColorNeutral;
                            }

                            string remainingTime = getRemainingTimeCaption(elapsedTime);
                            if (_lastRemainingTime!=remainingTime)
                            {
                                if (remainingTime == "00:10")
                                {
                                    playSound("2166__suburban-grilla__bowl-struck.wav");
                                }
                                else if (remainingTime == "00:00")
                                {
                                    playSound("32304__acclivity__shipsbell.wav");
                                    _bodyBackgroundColor = BackgroundColorFailed;
                                }

                                if (WebBrowser.InvokeRequired)
                                {
                                    WebBrowser.Invoke(new Action<string>(text =>
                                    {
                                        WebBrowser.Document.OpenNew(false);
                                        WebBrowser.Document.Write(text);
                                        _mainForm.Refresh();
                                    }), CreateTimerHtml(remainingTime, _bodyBackgroundColor, true));
                                }
                                _lastRemainingTime = remainingTime;
                            }
                            Thread.Sleep(10);
                        }
                    };
                    Thread timerThread = new Thread(start) {IsBackground = true};
                    timerThread.Start();
                }
                else if (args.Url.AbsoluteUri == "command://stop/")
                {
                    _timerRunning = false;
                    _mainForm.TopMost = false;
                    WebBrowser.Document.OpenNew(false);
                    WebBrowser.Document.Write(CreateTimerHtml(getRemainingTimeCaption(TimeSpan.FromSeconds(0)), BackgroundColorNeutral, false));
                    _mainForm.Refresh();
                }
                else if (args.Url.AbsoluteUri == "command://reset/")
                {
                    _currentCycleStartTime = DateTime.Now;
                    _bodyBackgroundColor = BackgroundColorPassed;
                }
                else if (args.Url.AbsoluteUri == "command://quit/")
                {
                    _mainForm.Close();
                }
                args.Cancel = true;
            };

            _mainForm.Controls.Add(WebBrowser);

            Application.Run(_mainForm);
        }

        private static string getRemainingTimeCaption(TimeSpan elapsedTime)
        {
            TimeSpan remainingTime = TimeSpan.FromSeconds(SecondsInCycle) - TimeSpan.FromSeconds(Math.Floor(elapsedTime.TotalSeconds));
            return remainingTime.ToString("mm':'ss");
        }

        private static string CreateTimerHtml(string timerText, string bodyColor, bool running)
        {
            string timerHtml = "<html><body style=\"border: 3px solid #555555; background: " + bodyColor +
                               "; margin: 0; padding: 0;\">" +
                               "<h1 style=\"text-align: center; font-size: 30px; color: #333333;\">" + timerText +
                               "</h1>" +
                               "<div style=\"text-align: center\">";
            if (running)
            {
                timerHtml += "<a style=\"color: #555555;\" href=\"command://stop\">Stop</a> " +
                             "<a style=\"color: #555555;\" href=\"command://reset\">Reset</a> ";
            }
            else
            {
                timerHtml += "<a style=\"color: #555555;\" href=\"command://start\">Start</a> ";
            }
            timerHtml += "<a style=\"color: #555555;\" href=\"command://quit\">Quit</a> ";
            timerHtml += "</div>" +
                         "</body></html>";
            return timerHtml;
        }

        private static void playSound(string url)
        {
            var playThread = new Thread(() =>
            {
                var inputStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("BabyStepTimer.Resources."+url);
                SoundPlayer player = new SoundPlayer(inputStream);
                player.Play();
            });
            playThread.IsBackground = true;
            playThread.Start();
        }
    }
}