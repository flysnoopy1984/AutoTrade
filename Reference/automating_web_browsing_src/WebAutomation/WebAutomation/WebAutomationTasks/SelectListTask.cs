using System;
using System.Windows.Forms;
using mshtml;

namespace WebAutomation.WebAutomationTasks
{
    public class SelectListTask
    {
        public string Execute(HtmlElement dropdown, string value)
        {
            try
            {
                HTMLSelectElementClass iElement = (HTMLSelectElementClass) dropdown.DomElement;
                iElement.value = value;
            }

            catch (Exception ex)
            {
                return String.Format("Unable to select value in drop down list: {0}", ex.Message);
            }

            return "A value from list was selected.";
        }
    }
}