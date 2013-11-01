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
using System.Web;

namespace AutoTrade
{
    public partial class MainForm : Form
    {
        private WebClient _WebClient;
        private ShowInfoForm _ShowInfo;
        Thread _thShow,_thInit;
        private string _Cookies = "";

        static CookieContainer cookie2 = new CookieContainer();

        public enum ResponeType
        {
            String,
            File
        }


        public MainForm()
        {
            InitializeComponent();
            _WebClient = new WebClient();

            _ShowInfo = new ShowInfoForm();

            CheckForIllegalCrossThreadCalls = false;//为false可以跨线程调用windows控件     

            //_thInit = new Thread(new ThreadStart(InitNet));
            //_thInit.Start();
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
            string loginUrl = "https://passport.jd.com/new/login.aspx";
            string s =  Get(loginUrl, null);
            _ShowInfo.ShowInfo(s);
            
        }

        private void bn_Login_Click(object sender, EventArgs e)
        {
            //_ShowInfo.Show();
            //_thShow = new Thread(new ThreadStart(Login));
            //_thShow.Start();
            string loginUrl = "https://passport.jd.com/new/login.aspx";
            Get(loginUrl, null);

            string loginRefererUrl = "http://passport.jd.com/uc/login?ltype=logout";
            string loginServiceUrl = "http://passport.jd.com/uc/loginService";
            var s = Post(loginServiceUrl,
               new Dictionary<string, string>(){
                {"uuid",Convert.ToString(Guid.NewGuid())},
                {"loginname",HttpUtility.UrlEncode("flysnoopy1984")},
                {"nloginpwd",HttpUtility.UrlEncode("Edifier1984")},
                {"loginpwd",HttpUtility.UrlEncode("Edifier1984")},
                {"machineNet","machineCpu"},
                {"machineDisk",""},
                {"authcode",""}}, loginRefererUrl);

            var dic = new Dictionary<string, string>();
            s = Get("http://order.jd.com/center/list.action?r=635133982534597500", null, fun: a => dic = a);

            _ShowInfo.ShowInfo(s);
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
           // item.CookieCollection = cookie2;
            item.Cookie = _Cookies;
            result = helper.GetHtml(item);
          
            _ShowInfo.ShowInfo(result.Html);
        }

        public static string Post(string url, Dictionary<string, string> postData, string referer = "", string accept = "", string contentType = "", ResponeType type = ResponeType.String, string fileSavePath = "", Action<string> action = null, Func<Dictionary<string, string>> fun = null)
        {
            var result = "";
            //var cookie = new CookieContainer();
            StringBuilder strPostData = new StringBuilder();
            if (postData != null)
            {
                postData.AsQueryable().ToList().ForEach(a =>
                {
                    strPostData.AppendFormat("{0}={1}&", a.Key, a.Value);
                });
            }
            byte[] byteArray = Encoding.UTF8.GetBytes(strPostData.ToString().TrimEnd('&'));

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

            webRequest.CookieContainer = cookie2;

            webRequest.Method = "POST";
            if (string.IsNullOrEmpty(accept))
                webRequest.Accept = "application/json, text/javascript, */*;";
            else
                webRequest.Accept = accept;

            if (!string.IsNullOrEmpty(referer))
                webRequest.Referer = referer;
            if (string.IsNullOrEmpty(contentType))
                webRequest.ContentType = "application/x-www-form-urlencoded";
            else
                webRequest.ContentType = contentType;

            if (strPostData.Length > 0)
                webRequest.ContentLength = byteArray.Length;

            //请求
            Stream newStream = webRequest.GetRequestStream();
            newStream.Write(byteArray, 0, byteArray.Length);
            newStream.Close();

            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            var responSteam = response.GetResponseStream();

            if (type == ResponeType.String)
            {
                StreamReader strRespon = new StreamReader(responSteam, Encoding.UTF8);
                result = strRespon.ReadToEnd();
            }
            else
            {
                BinaryReader br = new BinaryReader(responSteam);
                byte[] byteArr = br.ReadBytes(200000);
                FileStream fs = new FileStream(fileSavePath, FileMode.OpenOrCreate);
                fs.Write(byteArr, 0, byteArr.Length);
                fs.Dispose();
                fs.Close();
                result = "OK";
            }
            if (action != null)
            {
                action.Invoke(result);
            }
            if (fun != null)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                foreach (var item in cookie2.GetCookies(webRequest.RequestUri))
                {
                    var c = item as Cookie;
                    dic.Add(c.Name, c.Value);
                }
                fun = () => { return dic; };
            }
            return result;

        }


        public static string Get(string url, Dictionary<string, string> postData = null, string referer = "", Action<string> action = null, Action<Dictionary<string, string>> fun = null)
        {
            var result = "";

            StringBuilder strPostData = new StringBuilder("?");
            if (postData != null)
            {
                postData.AsQueryable().ToList().ForEach(a =>
                {
                    strPostData.AppendFormat("{0}={1}&", a.Key, a.Value);
                });
            }
            if (strPostData.Length == 1)
                strPostData = strPostData.Clear();
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url + strPostData.ToString().TrimEnd('&'));
            webRequest.CookieContainer = cookie2;
            webRequest.Method = "GET";
            webRequest.Accept = "text/javascript, text/html, application/xml, text/xml, */*;";
            if (!string.IsNullOrEmpty(referer))
                webRequest.Referer = referer;
            //请求
            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            var responSteam = response.GetResponseStream();

            StreamReader strRespon = new StreamReader(responSteam, Encoding.Default);
            result = strRespon.ReadToEnd();

            if (action != null)
            {
                action.Invoke(result);
            }
            if (fun != null)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                foreach (var item in cookie2.GetCookies(webRequest.RequestUri))
                {
                    var c = item as Cookie;
                    dic.Add(c.Name, c.Value);
                }
                fun.Invoke(dic);
            }
            return result;

        }

      
    }
}
