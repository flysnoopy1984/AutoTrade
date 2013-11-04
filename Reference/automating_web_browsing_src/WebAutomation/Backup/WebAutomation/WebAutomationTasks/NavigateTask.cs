using System;
using System.Threading;
using System.Windows.Forms;

namespace WebAutomation.WebAutomationTasks
{
    public class NavigateTask
    {
        public string Execute(WebBrowser browser, string urlToLoad)
        {
            bool loadFinished = false;
            int counterTimeOut = 500;
            string message;

            try
            {
                browser.DocumentCompleted += delegate { loadFinished = true; };
                browser.Navigate(urlToLoad);

                while (!loadFinished && counterTimeOut > 0)
                {
                    Thread.Sleep(100);
                    Application.DoEvents();
                    counterTimeOut--;
                }

                message = string.Format("Navigated to {0}", urlToLoad);
            }
            catch (Exception ex)
            {
                message = ex.ToString();
            }

            return message;
        }
    }
}