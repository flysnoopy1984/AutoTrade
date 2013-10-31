using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DotNet.Utilities;
using System.Text.RegularExpressions;


namespace _18京东
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string cookies = "";

        #region 登陆
        private void btnLogin_Click(object sender, EventArgs e)
        {
            HttpItem item = new HttpItem();
            HttpHelper helper = new HttpHelper();
            HttpResult result = new HttpResult();

            item.URL = "http://passport.jd.com/uc/loginService?uuid=20f9b1b4-67af-4426-af5b-3533a66739cb&ReturnUrl=http%3a%2f%2fjd2008.jd.com%2fJdHome%2fOrderList.aspx&r=0.03137395461591336";
            item.Method = "post";
            item.Allowautoredirect = true;
            item.ContentType = "application/x-www-form-urlencoded";
            item.Postdata = "uuid=20f9b1b4-67af-4426-af5b-3533a66739cb&loginname=京东账号&loginpwd=京东密码&authcode=";
            item.Header.Add("x-requested-with", "XMLHttpRequest");
            item.Header.Add("Accept-Encoding", "gzip, deflate");
            item.Referer = "http://passport.jd.com/new/login.aspx?ReturnUrl=http%3a%2f%2fjd2008.jd.com%2fJdHome%2fOrderList.aspx";
            item.Accept = "*/*";
            item.Encoding = Encoding.UTF8;
            item.Cookie = cookies;
            result = helper.GetHtml(item);
            cookies = "__jda=95931165.290243407.1371634814.1371634814.1371634814.1; __jdb=95931165.1.290243407|1.1371634814; __jdc=95931165; __jdv=95931165|direct|-|none|-;" + result.Cookie;
            cookies = cookies.Replace("HttpOnly,", null);
            txtResult.Text = "登陆成功了！\n" + result.Html;
        }
        #endregion

        #region 预加载
        private void Form1_Load(object sender, EventArgs e)
        {
            HttpItem item = new HttpItem();
            HttpHelper helper = new HttpHelper();
            HttpResult result = new HttpResult();

            item.URL = "https://passport.jd.com/new/login.aspx";

            result = helper.GetHtml(item);
            cookies = result.Cookie;
            txtResult.Text = "预加载完毕！\n" + result.Html;
        }
        #endregion

        #region 红包页面
        private void btnAccess_Click(object sender, EventArgs e)
        {
            HttpItem item = new HttpItem();
            HttpHelper helper = new HttpHelper();
            HttpResult result = new HttpResult();

            item.URL = "http://coupon.jd.com/user_quan.aspx";
            item.Encoding = Encoding.GetEncoding("gb2312");
            item.Header.Add("Accept-Encoding", "gzip, deflate");
            item.ContentType = "text/html";
            item.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)";
            item.Cookie = cookies;
            result = helper.GetHtml(item);
            result.Cookie = cookies;
            string html = result.Html;
            MatchCollection hongBao = Regex.Matches(html, "\\s+(￥.+)\\s+</div>");
            txtResult.ResetText();
            foreach (Match h in hongBao)
            {
                txtResult.AppendText(h.Groups[1].ToString() + "\n");
            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            txtResult.Text = cookies;
        }
    }
}
