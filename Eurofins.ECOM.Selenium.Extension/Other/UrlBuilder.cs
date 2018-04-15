using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Eurofins.ECOM.Selenium.Extension.Other;

namespace Eurofins.Selenium.Extension.Other
{
    public class UrlBuilder
    {
        string protocol;
        string serverUrl;
        string salesForceURL;
        string backOfficeURL;
        string hostName;
        string port;
        string path;
        string alternateHostName;

        public string AlternateHostName
        {
            get { return alternateHostName; }
        }

        public string ServerUrl
        {
            get { return serverUrl; }
        }

        public string SalesForceURL
        {
            get { return salesForceURL; }
        }
        public string BackOfficeURL
        {
            get { return backOfficeURL; }
        }
        public string HostName
        {
            get { return hostName; }
        }

        public string Path
        {
            get { return path; }
        }

        public UrlBuilder()
        {
            protocol = EnvironmentManager.GetSettingValue("Protocol");
            serverUrl = EnvironmentManager.GetSettingValue("ServerUrl");
            salesForceURL = EnvironmentManager.GetSettingValue("SalesForceURL");
            backOfficeURL = EnvironmentManager.GetSettingValue("BackOfficeURL");
            hostName = EnvironmentManager.GetSettingValue("HostName");
            hostName = EnvironmentManager.GetSettingValue("HostName");
            port = EnvironmentManager.GetSettingValue("Port");
            // TODO(andre.nogueira): Remove trailing / from folder
            //path = EnvironmentManager.GetSettingValue("Folder");
            //Use the first IPv4 address that we find
            //IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            //foreach (IPAddress ip in Dns.GetHostEntry(hostName).AddressList)
            //{
            //    if (ip.AddressFamily == AddressFamily.InterNetwork)
            //    {
            //        ipAddress = ip;
            //        break;
            //    }
            //}
            //alternateHostName = ipAddress.ToString();
            alternateHostName = hostName.ToString();
        }

        public string WhereIs()
        {
            // TODO(andre.nogueira): Is it a problem if folder==""?
            if (string.IsNullOrEmpty(path))
                return protocol + "://" + hostName + ":" + port + "/";// +page;
            return protocol + "://" + hostName + ":" + port + "/" + path + "/";// +page;
        }

        public string WhereElseIs(string page)
        {
            return protocol + "://" + alternateHostName + ":" + port + "/" + path + "/" + page;
        }

        public string WhereIsSecure(string page)
        {
            return protocol + "s://" + alternateHostName + ":" + port + "/" + path + "/" + page;
        }
    }
}
