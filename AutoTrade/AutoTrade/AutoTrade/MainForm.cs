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
using mshtml;
using System.Diagnostics;

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
            NewWeb();

            //_thInit = new Thread(new ThreadStart(InitNet));
            //_thInit.Start();
        }


        private void InitNet()
        {
            //HttpItem item = new HttpItem();
            //HttpHelper helper = new HttpHelper();
            //HttpResult result = new HttpResult();

            //item.URL = "https://passport.yhd.com/passport/login_input.do";
            

            //result = helper.GetHtml(item);
            //_Cookies = result.Cookie;

            //tb_Msg.AppendText(_Cookies);
         //   tb_Msg.AppendText(result.Html);
        }



        private void NewWeb()
        {
            this.myBrowser1.Url = new Uri("http://www.baidu.com");
          //  this.myBrowser1.Url = new Uri("https://passport.jd.com/new/login.aspx?ReturnUrl=http%3A%2F%2Fwww.jd.com%2Fbigimage.aspx%3Fid%3D1015445051");
            myBrowser1.ScriptErrorsSuppressed = true;
            myBrowser1.LocationChanged += new EventHandler(myBrowser1_LocationChanged);
        
            myBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(myBrowser1_DocumentCompleted);
        }

        void myBrowser1_LocationChanged(object sender, EventArgs e)
        {
            Debug.WriteLine(myBrowser1.Url);
        }

        void myBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Debug.WriteLine(e.Url);
            Uri url = e.Url;
            if (url.OriginalString == "http://www.jd.com/bigimage.aspx?id=1015445051")
            {
                this.myBrowser1.Document.All["InitCartUrl"].InvokeMember("click");
            }
            if (url.AbsolutePath == "/cart/addToCart.html")
            {
                
                this.myBrowser1.Navigate("http://cart.jd.com/cart/cart.html");
            }
            if (url.AbsolutePath == "/cart/cart.html")
            {


                // this.myBrowser1.Document.All["toSettlement"].InvokeMember("click");
            }
            if (url.OriginalString == "http://ckmap.mediav.com/b?type=10")
            {
                foreach (HtmlElement link in myBrowser1.Document.Links)
                {
                    Debug.WriteLine(link.GetAttribute("id"));
                    if (link.GetAttribute("id") == "toSettlement")
                    {
                        link.InvokeMember("click");
                    }
                }
            }
            if (url.AbsolutePath == "/order/getOrderInfo.action" && url.Host == "trade.jd.com")
            {
                ////  myBrowser1.Document.All["consignee_name"].InnerText = "aaa";
                //HtmlElementCollection cols =   myBrowser1.Document.GetElementsByTagName("input");
                //foreach (HtmlElement obj in cols)
                //{
                //    Debug.WriteLine(obj.InnerHtml);
                //}

               // HTMLDocument doc = myBrowser1.Document.DomDocument as HTMLDocument;
               // IHTMLElement ele = doc.getElementById("consignee-form");
               //HTMLInputElementClass name = doc.all.item("consignee-form", 0) as HTMLInputElementClass;
               // if (name != null)
               //     name.value = "aaa";

            }
            //注册
            else if (url.OriginalString == "https://reg.jd.com/reg/person")
            {
                myBrowser1.Document.All["regName"].SetAttribute("value", "darkhome1");
                myBrowser1.Document.All["pwd"].SetAttribute("value", "edifier");
                myBrowser1.Document.All["pwdRepeat"].SetAttribute("value", "edifier");

                

                myBrowser1.Document.All["registsubmit"].InvokeMember("click");
                HtmlElement ele = myBrowser1.Document.CreateElement("script");
                IHTMLScriptElement jse = (IHTMLScriptElement)ele.DomElement;
                jse.text = "function AddSubmit() { alert(isSubmit);}";
                myBrowser1.Document.Body.AppendChild(ele);
                myBrowser1.Document.InvokeScript("AddSubmit");
                
                //HTMLDocument doc = myBrowser1.Document.DomDocument as HTMLDocument;

                //HTMLFormElementClass form = doc.all.item("personRegForm", 0) as HTMLFormElementClass;
                //form.submit();
            }
        }

      
     
     

        private void button1_Click(object sender, EventArgs e)
        {
            myBrowser1.Print();
            
        }

        private void bn_Login_Click(object sender, EventArgs e)
        {
            _ShowInfo.Show();
            _thShow = new Thread(new ThreadStart(Login));
            _thShow.Start();
            //string loginUrl = "https://passport.jd.com/new/login.aspx";
            //Get(loginUrl, null);

            //string loginRefererUrl = "http://passport.jd.com/uc/login?ltype=logout";
            //string loginServiceUrl = "http://passport.jd.com/uc/loginService";
            //var s = Post(loginServiceUrl,
            //   new Dictionary<string, string>(){
            //    {"uuid",Convert.ToString(Guid.NewGuid())},
            //    {"loginname",HttpUtility.UrlEncode("flysnoopy1984")},
            //    {"nloginpwd",HttpUtility.UrlEncode("Edifier1984")},
            //    {"loginpwd",HttpUtility.UrlEncode("Edifier1984")},
            //    {"machineNet","machineCpu"},
            //    {"machineDisk",""},
            //    {"authcode",""}}, loginRefererUrl);

            //var dic = new Dictionary<string, string>();
            //s = Get("http://order.jd.com/center/list.action?r=635190049375727500", null, fun: a => dic = a);

            //_ShowInfo.ShowInfo(s);
        }


        private void Login()
        {

            HttpItem item = new HttpItem();
            HttpHelper helper = new HttpHelper();
            HttpResult result = new HttpResult();
            Guid guid = Guid.NewGuid();

            item.URL = "http://www.yhd.com/1/";
            item.Method = "post";
            item.Allowautoredirect = true;
            item.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
            item.Postdata = "credentials.username=darkhome%40126.com&credentials.password=edifier1984&validCode=&loginSource=1&returnUrl=http%3A%2F%2Fwww.yhd.com&isAutoLogin=0";
            item.Header.Add("x-requested-with", "XMLHttpRequest");
            item.Header.Add("Accept-Encoding", "gzip, deflate");
            item.Referer = "http://www.yhd.com/1/";
            item.Accept = "*/*";
         //   item.Encoding = Encoding.Default;

        //    _Cookies = "__jda=122270672.1582893612.1383385889.1383385889.1383385889.1; __jdb=122270672.1.1582893612|1.1383385889; __jdc=122270672; __jdv=122270672|direct|-|none|-;" + _Cookies;
         //   _Cookies = _Cookies.Replace("HttpOnly,", null);
       //     _Cookies = _Cookies.Replace("HttpOnly", null);
       //     _Cookies = _Cookies.Replace("Path=/;", null);
             
            

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

      

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {

                this.myBrowser1.Navigate("https://passport.jd.com/new/login.aspx?ReturnUrl=http%3A%2F%2Fwww.jd.com%2Fbigimage.aspx%3Fid%3D1015445051");

                HTMLDocument doc = myBrowser1.Document.DomDocument as HTMLDocument;

                System.Windows.Forms.HtmlDocument document = this.myBrowser1.Document;

                HTMLInputElementClass input = doc.all.item("loginname", 0) as HTMLInputElementClass;
                input.value = "flysnoopy1984";
                
                HTMLInputElementClass pwd = doc.all.item("nloginpwd", 0) as HTMLInputElementClass;
                pwd.value = "Edifier1984";

              //  myBrowser1.Document.InvokeScript("loginsubmit");
                object obj = this.myBrowser1.Document.All["loginsubmit"].InvokeMember("click");

              //  HTMLFormElementClass form = doc.all.item("formlogin", 0) as HTMLFormElementClass;
              //  form.submit();
                //myBrowser1.Refresh();
              
                //this.myBrowser1.Navigate("http://item.jd.com/388340.html");
                
              
              
            

                //myBrowser1.Refresh();
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.myBrowser1.Navigate("https://reg.jd.com/reg/person");

            //regName
        }

      
    }
}
