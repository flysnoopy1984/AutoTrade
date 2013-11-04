using System;
using System.Windows.Forms;

namespace WebAutomation.WebAutomationTasks
{
    public class EntryDataTask
    {
        public string Execute(HtmlElement element, string valueToFill)
        {
            try
            {
                element.InnerText = valueToFill;
            }
            catch (Exception ex)
            {
                return String.Format("Unable to fill value in input field: {0}", ex.Message);
            }

            return "Text box was field with value.";
        }
    }
}