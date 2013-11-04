using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoTrade
{
    public partial class ShowInfoForm : Form
    {
        public ShowInfoForm()
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;//为false可以跨线程调用windows控件
        }

        public void ShowInfo(string text)
        {
            try
            {
                SetInfo(text);
                this.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SetInfo(string text)
        {
            this.tb_Info.Text = text;
        }
    }
}
