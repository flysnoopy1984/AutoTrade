using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using WebAutomation.WebAutomationTasks;

namespace WebAutomation
{
    public partial class WebAutomationForm : Form
    {
        public WebAutomationForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.cbTasks.Items.Add("Navigate to specific web page");  
            this.cbTasks.Items.Add("Save as picture");                      
            this.cbTasks.Items.Add("Click a button");                         
            this.cbTasks.Items.Add("Fill data in text box");                 
            this.cbTasks.Items.Add("Click link task");                        
            this.cbTasks.Items.Add("Select a radio button");
            this.cbTasks.Items.Add("Select a value in list box");
            
            this.cbTasks.SelectedIndex = 0;
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            #region Navigate task
            
            if (this.cbTasks.SelectedIndex == 0) 
            {
                //we use a function for this task since we reuse it in other examples
                this.LoadPageInBrowser();
                this.WriteLog("Navigated to page.");
            } 
            
            #endregion

            #region Screen shot task
            
            if (this.cbTasks.SelectedIndex == 1)
            {
                this.LoadPageInBrowser();

                MakeScreenShotTask screenShotTask = new MakeScreenShotTask();
                string message = screenShotTask.Execute(this.webBrowser, ConfigurationManager.AppSettings.Get("picture.path"), "testPicture.jpeg");
                this.WriteLog(message);
            } 
            
            #endregion

            #region Click button task
            
            if (this.cbTasks.SelectedIndex == 2)
            {
                this.LoadPageInBrowser();

                ClickButtonTask clickButtonTask = new ClickButtonTask();
                HtmlElement buttonToClick = this.FindControlByName("Button", this.webBrowser.Document.All);
                string message = clickButtonTask.Execute(this.webBrowser, buttonToClick);
                this.WriteLog(message);
            } 
            
            #endregion

            #region Fill data task

            if (this.cbTasks.SelectedIndex == 3)
            {
                this.LoadPageInBrowser();

                EntryDataTask entryDataTask = new EntryDataTask();
                HtmlElement textBox = this.FindControlByName("TextBox", this.webBrowser.Document.All);
                string message = entryDataTask.Execute(textBox, "Hello from program!!!");
               this.WriteLog(message);
            } 

            #endregion

            #region Click link data task

            if (this.cbTasks.SelectedIndex == 4)
            {
                this.LoadPageInBrowser();

                ClickLinkTask clickTask = new ClickLinkTask();
                HtmlElement linkToClick = this.FindControlByTag("A", this.webBrowser.Document.All);
                string message = clickTask.Execute(linkToClick);
               this.WriteLog(message);
            }

            #endregion

            #region Select a radio button task

            if (this.cbTasks.SelectedIndex == 5)
            {
                this.LoadPageInBrowser();

                SelectRadioButtonTask selectRadioButtonTask = new SelectRadioButtonTask();
                HtmlElement linkToClick = this.FindControlByName("Radio3", this.webBrowser.Document.All);
                string message = selectRadioButtonTask.Execute(linkToClick);
               this.WriteLog(message);
            }

            #endregion

            #region Select a value from list box task

            if (this.cbTasks.SelectedIndex == 6)
            {
                this.LoadPageInBrowser();

                SelectListTask selectListTask = new SelectListTask();
                HtmlElement listBox = this.FindControlByName("List", this.webBrowser.Document.All);
                string message = selectListTask.Execute(listBox, "Second");
               this.WriteLog(message);
            }

            #endregion
        }

        #region Helpers

        private void WriteLog(string logMessage)
        {
            this.txtLog.Text = string.Empty;
            this.txtLog.Text = logMessage;
        }
        
        private void LoadPageInBrowser()
        {
            string folder = Application.StartupPath;
            string exampleFile = ConfigurationManager.AppSettings.Get("example.name");

            NavigateTask navTask = new NavigateTask();
            navTask.Execute(this.webBrowser, Path.Combine(folder, exampleFile));
        }

        private HtmlElement FindControlByName(string name, HtmlElementCollection listOfHtmlControls)
        {
            foreach (HtmlElement element in listOfHtmlControls)
            {
                if (!string.IsNullOrEmpty(element.OuterHtml))
                {
                    if (element.Name == name.Trim())
                    {
                        return element;
                    }
                }
            }

            return null;
        }

        private HtmlElement FindControlByTag(string tag, HtmlElementCollection listOfHtmlControls)
        {
            foreach (HtmlElement element in listOfHtmlControls)
            {
                if (!string.IsNullOrEmpty(element.OuterHtml))
                {
                    if (element.TagName == tag.Trim())
                    {
                        return element;
                    }
                }
            }

            return null;
        }
        
        #endregion
    }
}