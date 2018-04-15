using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eurofins.ECOM.Selenium.Extension.Other
{
    public class Utility
    {
        public static class StringUtility
        {
            public static string getRandomString(int numberOfChars = 8)
            {
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789 ";
                var random = new Random();
                var result = new string(
                    Enumerable.Repeat(chars, numberOfChars)
                              .Select(s => s[random.Next(s.Length)])
                              .ToArray());
                return result;
            }
        }
    }

    
    public enum BrowserType
    {
        All,
        HtmlUnit,
        IE,
        Firefox,
        Safari,
        Chrome,
        ChromeNonWindows,
        Remote,
        IPhone
    }
}
