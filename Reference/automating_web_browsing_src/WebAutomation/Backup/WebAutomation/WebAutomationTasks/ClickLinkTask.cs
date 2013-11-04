using System;
using System.Windows.Forms;
using mshtml;

namespace WebAutomation.WebAutomationTasks
{
    public class ClickLinkTask
    {
        public string Execute(HtmlElement linkToClick)
        {
            try
            {
                HTMLAnchorElementClass linkElement = (HTMLAnchorElementClass) linkToClick.DomElement;
                linkElement.click();
            }
            catch (Exception ex)
            {
                return String.Format("Unable to click link: {0}", ex.Message);
            }

            return "Link was clicked, new page opened...";
        }
    }
}