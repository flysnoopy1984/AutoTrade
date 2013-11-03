using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AutoTrade
{
    public partial class MyBrowser : WebBrowser
    {
        public MyBrowser()
        {
            InitializeComponent();
        }

        public string cookie()
        {
         
            if (this.Url == null) 
                return null; 
            string dir = this.Url.Host; 
            FileStream fr = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.Cookies) + "\\index.dat", FileMode.Open, FileAccess.Read, FileShare.ReadWrite); 
            byte[] __dat = new byte[(int)fr.Length]; 
            fr.Read(__dat, 0, __dat.Length); 
            fr.Close(); 
            fr.Dispose(); 
            string __datstream = Encoding.Default.GetString(__dat); 
            int p1 = 0; 
            p1 = __datstream.IndexOf("@" + dir, p1); 
            if (p1 == -1) 
                p1 = __datstream.IndexOf("@" + dir.Substring(dir.IndexOf('.') + 1)); 
            if (p1 == -1) 
                return this.Document.Cookie; 
            int p2 = __datstream.IndexOf(".txt", p1 + 1); 
            p1 = __datstream.LastIndexOf('@', p2); 
            string dm = __datstream.Substring(p1 + 1, p2 - p1 + 3).TrimStart('?'); 
            p1 = __datstream.LastIndexOf(":", p1); 
            p2 = __datstream.IndexOf('@', ++p1); 
            __datstream = string.Format("{0}@{1}", __datstream.Substring(p1, p2 - p1), dm); 
             
            Dictionary<string, string> __cookiedicts = new Dictionary<string, string>(); 
            string __n; 
            StringBuilder __cookies = new StringBuilder(); 
            __datstream = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.Cookies) + "\\" + __datstream, Encoding.Default); 
            p1 = -2; 
            do 
            { 
                p1 += 2; 
                p2 = __datstream.IndexOf('\n', p1); 
                if (p2 == -1) 
                    break; 
                __n = __datstream.Substring(p1, p2 - p1); 
                p1 = p2 + 1; 
                p2 = __datstream.IndexOf('\n', p1); 
                if (!__cookiedicts.ContainsKey(__n)) 
                    __cookiedicts.Add(__n, __datstream.Substring(p1, p2 - p1)); 
            } 
            while ((p1 = __datstream.IndexOf("*\n", p1)) > -1); 
            if (this.Document.Cookie != null && this.Document.Cookie.Length > 0) 
            { 
                foreach (string s in this.Document.Cookie.Split(';')) 
                { 
                    p1 = s.IndexOf('='); 
                    if (p1 == -1) 
                        continue; 
                    __datstream = s.Substring(0, p1).TrimStart(); 
                    if (__cookiedicts.ContainsKey(__datstream)) 
                        __cookiedicts[__datstream] = s.Substring(p1 + 1); 
                    else 
                        __cookiedicts.Add(__datstream, s.Substring(p1 + 1)); 
                } 
            } 
            foreach (string s in __cookiedicts.Keys) 
            { 
                if (__cookies.Length > 0) 
                    __cookies.Append(';'); 
                __cookies.Append(s); 
                __cookies.Append('='); 
                __cookies.Append(__cookiedicts[s]); 
            } 
            return __cookies.ToString(); 
        
        }
    }
}
