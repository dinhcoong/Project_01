using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace mvcweb.Common
{
    public  class FriendlyUrl
    {
        public static string Url(string value)
        {
            return Regex.Replace(value, @"[^A-Za-z0-9_\.~]+", "-");

        }
    }
}