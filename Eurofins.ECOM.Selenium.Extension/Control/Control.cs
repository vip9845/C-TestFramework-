using Eurofins.ECOM.Selenium.Extension.Interface;
using Eurofins.ECOM.Selenium.Extension.Other;
using System;

namespace Eurofins.ECOM.Selenium.Extension.Control
{
    public class Control
    {
        public Control()
        {

        }

        public IBrowser CurrentBrowser
        {
            get
            {
                return Navigator.Instance.CurrentBrowser;
            }
        }

        public void DebuggingInformation(string msg)
        {
            System.Console.WriteLine(DateTime.Now.ToString()+" "+msg);
        }
    }
}
