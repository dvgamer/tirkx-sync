using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace iHelpful_Tirkx.Collection
{
    class ResponseBuilder : StringBuilder
    {

        public String HTML()
        {
            String[] _data = Regex.Split("\r\n\r\n", this.ToString());
            if (_data.Length >= 2) return _data[2]; else return "";
        }

        public String SetCookie()
        {
            String[] _data = Regex.Split("\r\n", Regex.Split("\r\n\r\n", this.ToString())[0]);
            for (Int32 i = 0; i < _data.Length; i++)
            {

            }
            return "";
        }
    }
}
