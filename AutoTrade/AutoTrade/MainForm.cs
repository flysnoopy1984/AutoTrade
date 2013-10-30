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

        Thread _thShow;
        public MainForm()
        {
            InitializeComponent();
            _WebClient = new WebClient();

            _ShowInfo = new ShowInfoForm();

            CheckForIllegalCrossThreadCalls = false;//为false可以跨线程调用windows控件            
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
            _thShow = new Thread(new ThreadStart(TestHelp));
            _thShow.Start();           
        }

        private void Test1()
        {
            

            _WebClient.BaseAddress = "http://passport.jd.com/uc/login";
            

            System.IO.Stream s =_WebClient.OpenRead("/");

            StreamReader sr = new StreamReader(s,Encoding.Default);

            _ShowInfo.ShowInfo(sr.ReadToEnd());
            
        }

        void TestHelp()
        {
            HttpItem item = new HttpItem();
            HttpHelper helper = new HttpHelper();
            HttpResult result = new HttpResult();

            item.URL = "https://passport.jd.com/new/login.aspx";

            result = helper.GetHtml(item);
            string cookies = result.Cookie;

            helper = new HttpHelper();
         //   result = new HttpResult();
            item = new HttpItem();
            item.URL = "http://passport.jd.com/uc/loginService?uuid=20f9b1b4-67af-4426-af5b-3533a66739cb&ReturnUrl=http%3a%2f%2fjd2008.jd.com%2fJdHome%2fOrderList.aspx&r=0.03137395461591336";
            item.Method = "post";
            item.Allowautoredirect = true;
            item.ContentType = "application/x-www-form-urlencoded";
            item.Postdata = "uuid=20f9b1b4-67af-4426-af5b-3533a66739cb&loginname=flysnoopy1984&loginpwd=Edifier1984&authcode=";
            item.Header.Add("x-requested-with", "XMLHttpRequest");
            item.Header.Add("Accept-Encoding", "gzip, deflate");
            item.Referer = "http://passport.jd.com/new/login.aspx?ReturnUrl=http%3a%2f%2fjd2008.jd.com%2fJdHome%2fOrderList.aspx";
            item.Accept = "*/*";
            item.Encoding = Encoding.UTF8;

            cookies = "__jda=95931165.290243407.1371634814.1371634814.1371634814.1; __jdb=95931165.1.290243407|1.1371634814; __jdc=95931165; __jdv=95931165|direct|-|none|-;" + result.Cookie;
            cookies = cookies.Replace("HttpOnly,", null);

            item.Cookie = cookies;
            result = helper.GetHtml(item);

            _ShowInfo.ShowInfo(result.Html);
        }

        private void TestHttpWebRequest()
        {
            HttpWebRequest rq = (HttpWebRequest)HttpWebRequest.Create(@"http://passport.jd.com/uc/login");
            rq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            rq.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/30.0.1599.101 Safari/537.36";
            rq.AllowAutoRedirect = true;

            CookieContainer cc = new CookieContainer();
            Cookie c = new Cookie("_pst", "flysnoopy1984","/",".jd.com");
           
            cc.Add(c);
            c = new Cookie("pin", "flysnoopy1984", "/", ".jd.com");
            cc.Add(c);
            c = new Cookie("logining", "1", "/", ".jd.com");
            cc.Add(c);
            c = new Cookie("unick", "jackysongYY", "/", ".jd.com");
            cc.Add(c);

            rq.CookieContainer = cc;

            WebResponse res = rq.GetResponse();

            System.IO.Stream s = res.GetResponseStream();

            StreamReader sr = new StreamReader(s, Encoding.Default);

            _ShowInfo.ShowInfo(sr.ReadToEnd());

           
            sr.Close();
        }

      
    }
}
