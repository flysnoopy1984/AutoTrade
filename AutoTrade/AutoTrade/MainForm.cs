using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Threading;
using AutoTrade.Core;

namespace AutoTrade
{
    public partial class MainForm : Form
    {
        private WebClient _WebClient;
        private ShowInfoForm _ShowInfo;
        Thread _thShow,_thInit;
        private string _Cookies = "";

        public MainForm()
        {
            InitializeComponent();
            _WebClient = new WebClient();

            _ShowInfo = new ShowInfoForm();

            CheckForIllegalCrossThreadCalls = false;//为false可以跨线程调用windows控件     

            _thInit = new Thread(new ThreadStart(InitNet));
            _thInit.Start();
        }

        private void InitNet()
        {
            HttpItem item = new HttpItem();
            HttpHelper helper = new HttpHelper();
            HttpResult result = new HttpResult();

            item.URL = "https://passport.jd.com/new/login.aspx";

            result = helper.GetHtml(item);
            _Cookies = result.Cookie;

            tb_Msg.AppendText(_Cookies);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] bs = new byte[16];
            bs = Encoding.Default.GetBytes("Edifier1984");
            Guid g = new Guid(bs);
            MessageBox.Show(g.ToString());
        }

        private void bn_Login_Click(object sender, EventArgs e)
        {
            _ShowInfo.Show();
            _thShow = new Thread(new ThreadStart(Login));
            _thShow.Start();           
        }


        private void Login()
        {

            HttpItem item = new HttpItem();
            HttpHelper helper = new HttpHelper();
            HttpResult result = new HttpResult();
            Guid guid = Guid.NewGuid();

            item.URL = "https://passport.jd.com/new/login.aspx?ReturnUrl=http%3A%2F%2Fwww.jd.com%2F";
            item.Method = "post";
            item.Allowautoredirect = true;
            item.ContentType = "application/x-www-form-urlencoded";
            item.Postdata = "uuid="+guid+"&loginname=flysnoopy1984@126.com&loginpwd=Edifier1984&authcode=";
            item.Header.Add("x-requested-with", "XMLHttpRequest");
            item.Header.Add("Accept-Encoding", "gzip, deflate");
            item.Referer = "http://www.jd.com/";
            item.Accept = "*/*";
            item.Encoding = Encoding.Default;

            _Cookies = "unick=jackysongYY;pin=flysnoopy1984;mp=flysnoopy1984@126.com;_pst=flysnoopoy1984;" + _Cookies;
            _Cookies = "__jda=95931165.290243407.1371634814.1371634814.1371634814.1; __jdb=95931165.1.290243407|1.1371634814; __jdc=95931165; __jdv=95931165|direct|-|none|-;" + _Cookies;
            _Cookies = _Cookies.Replace("HttpOnly,", null);
            _Cookies = _Cookies.Replace("HttpOnly", null);

            item.Cookie = _Cookies;
            result = helper.GetHtml(item);
            
            _ShowInfo.ShowInfo(result.Html);
            
        }      

      
        private void bn_OrderList_Click(object sender, EventArgs e)
        {
            HttpItem item = new HttpItem();
            HttpHelper helper = new HttpHelper();
            HttpResult result = new HttpResult();

            item.URL = "http://jd2008.jd.com/JdHome/OrderList.aspx";                        
            item.Encoding = Encoding.GetEncoding("gb2312");
            item.Header.Add("Accept-Encoding", "gzip, deflate");
            item.ContentType = "text/html";
            item.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)";
            item.Cookie = _Cookies;
            result = helper.GetHtml(item);
          
            _ShowInfo.ShowInfo(result.Html);
        }

      
    }
}
