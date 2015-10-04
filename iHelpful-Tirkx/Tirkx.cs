using iHelpful_Tirkx.Collection;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace iHelpful_Tirkx
{
    public class Tirkx
    {
        public const String doHomeList = "http://forum.tirkx.com/main/tirkx_anime_list_home.php";
        public const String doLogin = "http://forum.tirkx.com/main/login.php?do=login";
        public const String doAnime = "http://forum.tirkx.com/main/tirkx_load_anime_per_topic.php?aid=90116&tirkx_locate=3BD4C7FA5F";
        public const String doFansub = "http://forum.tirkx.com/main/tirkx_load_FS_list.php?printFS=true";
        public const String doFollow = "http://forum.tirkx.com/main/tirkx_followAnime.php"; // 2=Follow,1=Unfollow   //follow=0&animeID=90116
        
        public enum Type
        {
            EN, TH, AU, OT
        }

        private enum Method
        {
            POST, GET
        }

        public struct NewFile
        {
            String Locate;
            String Thread;
            String Filename;
            Type Filetype;
            Boolean StreamAllow;
        }

        public Type Parse(string type)
        {
            Type _t = Type.TH;
            switch (type)
            {
                case "EN": _t = Type.EN; break;
                case "AU": _t = Type.AU; break;
                case "OT": _t = Type.OT; break; 
            }
            return _t;
        }
        
        private static String MD5(string text)
        {
            byte[] hashedBytes = MD5CryptoServiceProvider.Create().ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        private static void WriteCookie(string respone_text)
        {
            String[] split = Regex.Split("\r\n", Regex.Split("\r\n\r\n", respone_text)[0]);
            String _sCookie = "";
            for (Int32 i = 0; i < split.Length; i++)
            {
                if (split[i].Contains("Set-Cookie")) _sCookie += split[i].Substring(split[i].IndexOf(':') + 1, split[i].IndexOf(';') - 12);               
            }
        }

        private static String HTML(string text)
        {
            String[] split = Regex.Split("\r\n\r\n", text);
            return split[1];
        }


        public void Login(string username, string password)
        {
            RequestBuilder _req = new RequestBuilder(doLogin);
            _req.By = RequestBuilder.Method.POST;
            _req.Referer = "http://forum.tirkx.com/main/index.php";
            _req.Origin = "http://forum.tirkx.com";
            _req.ContentType = "application/x-www-form-urlencoded";
            _req.PostValue.Add("vb_login_username", username);
            _req.PostValue.Add("vb_login_password", "");
            _req.PostValue.Add("vb_login_password_hint", "Password");
            _req.PostValue.Add("cookieuser", "1");
            _req.PostValue.Add("securitytoken", "guest");
            _req.PostValue.Add("do", "login");
            _req.PostValue.Add("vb_login_md5password", Tirkx.MD5(password));
            _req.PostValue.Add("vb_login_md5password_utf", Tirkx.MD5(password));

            //string respone = ConnectTirkx(doLogin, _req.ToString());
        }

        public static List<NewFile> GetOngoing()
        {
            List<NewFile> _list = new List<NewFile>();

            return _list;

        }
    }

}
