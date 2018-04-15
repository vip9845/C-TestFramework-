using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;

namespace Eurofins.Selenium.Extension.Other
{
    public class ScreenShotUtility : RemoteWebDriver, ITakesScreenshot, ICapabilities
    {
        private readonly Uri HubAdress;

        public ScreenShotUtility(Uri RemoteAdress, ICapabilities capabilities)
            : base(RemoteAdress, capabilities)
        {
            this.HubAdress = RemoteAdress;
        }

        public Screenshot GetScreenshot()
        {
            Response screenshotResponse = this.Execute(DriverCommand.Screenshot, null);
            string base64 = screenshotResponse.Value.ToString();
            return new Screenshot(base64);
        }

        public string GetGridNodeHost()
        {
            var uri = new Uri(string.Format("http://{0}:{1}/grid/api/testsession?session={2}", HubAdress.Host, HubAdress.Port, SessionId));
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "POST";
            request.ContentType = "application/json";
            using (var httpResponse = (HttpWebResponse)request.GetResponse())
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var response = JObject.Parse(streamReader.ReadToEnd());
                return response.SelectToken("proxyId").ToString();
            }

        }

        public static void killNodeHost(string hubIP)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(hubIP + "/selenium-server/driver/?cmd=shutDownSeleniumServer");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        }

        public string BrowserName
        {
            get { throw new NotImplementedException(); }
        }

        public object GetCapability(string capability)
        {
            throw new NotImplementedException();
        }

        public bool HasCapability(string capability)
        {
            throw new NotImplementedException();
        }

        public bool IsJavaScriptEnabled
        {
            get { throw new NotImplementedException(); }
        }

        public Platform Platform
        {
            get { throw new NotImplementedException(); }
        }

        public string Version
        {
            get { throw new NotImplementedException(); }
        }

        public void applicationName()
        { }
    }

}
