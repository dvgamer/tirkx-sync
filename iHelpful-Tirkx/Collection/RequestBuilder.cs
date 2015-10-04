using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using System.Text;

namespace iHelpful_Tirkx.Collection
{
    public class RequestBuilder
    {
        const String Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
        const String UserAgent = "Mozilla/5.0 (Windows NT 6.2;) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1700.107 Safari/537.36";

        private String URL;
        private String Host;
        private Int32 ContentLength = 0;

        public Method By = Method.GET;
        public String Referer; // http://forum.tirkx.com/main/index.php
        public String Origin; // http://forum.tirkx.com
        public String Connection = "keep-alive";
        public String CacheControl = "max-age=0";
        public String ContentType;
        public NameValueCollection PostValue;
        public NameValueCollection Cookie;
        public Uri CurrentURL;
        public enum Method { POST, GET }

        public RequestBuilder(string url, bool https)
        {
            Cookie = new NameValueCollection();
            PostValue = new NameValueCollection();
            CurrentURL = new Uri(url);
            URL = CurrentURL.PathAndQuery;
            Host = CurrentURL.Host;
        }

        public override string ToString()
        {
            String _post = PostToString();
            ContentLength = _post.Length;
            StringBuilder _sb = new StringBuilder();
            _sb.AppendLine(By.ToString() + " " + URL + " HTTP/1.1");
            _sb.AppendLine("Host: " + Host);
            _sb.AppendLine("Cache-Control: " + CacheControl);
            if (!String.IsNullOrEmpty(ContentType)) _sb.AppendLine("Content-Type: " + ContentType);
            if (!String.IsNullOrEmpty(Accept)) _sb.AppendLine("Accept: " + Accept);
            if (!String.IsNullOrEmpty(UserAgent)) _sb.AppendLine("User-Agent: " + UserAgent);
            if (!String.IsNullOrEmpty(Referer)) _sb.AppendLine("Referer: " + Referer);
            if (!String.IsNullOrEmpty(Origin)) _sb.AppendLine("Origin: " + Origin);
            if (ContentLength>0) _sb.AppendLine("Content-Length: " + ContentLength);
            if (Cookie.Count > 0) _sb.AppendLine("cookie: " + ContentLength);
            _sb.AppendLine("Connection: " + Connection);
            _sb.AppendLine("");
            _sb.Append(_post);
            return _sb.ToString();
        }

        private String PostToString()
        {
            String _s = "";
            foreach (string key in PostValue.AllKeys) _s += key + "=" + PostValue.Get(key) + "&";            
            return _s.Substring(0, _s.Length - 1);
        }

    }
}