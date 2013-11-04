using System;
using System.Threading;
using System.Windows.Forms;
using mshtml;

namespace WebAutomation.WebAutomationTasks
{
    public class ClickButtonTask
    {
        public string Execute(WebBrowser browser, HtmlElement btn)
        {
            bool loadFinished = false;
            int counterTimeOut = 500;
            string message;

            try
            {
                browser.DocumentCompleted += delegate { loadFinished = true; };

                HTMLInputElementClass iElement = (HTMLInputElementClass)btn.DomElement;
                iElement.click();

                while (!loadFinished && counterTimeOut > 0)
                {
                    Thread.Sleep(100);
                    Application.DoEvents();
                    counterTimeOut--;
                }

                message = string.Format("Button {0} clicked", btn.InnerHtml.ToString());
            }
            catch (Exception ex)
            {
                message = ex.ToString();
            }

            return message;
        }
    }
}
