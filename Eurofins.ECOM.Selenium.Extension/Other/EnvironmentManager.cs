using OpenQA.Selenium;
using System;
using System.Reflection;
using OpenQA.Selenium.Chrome;
using System.IO;
using Eurofins.Selenium.Extension.Other;

namespace Eurofins.ECOM.Selenium.Extension.Other
{
    public class EnvironmentManager
    {
        readonly Type _driverType;
        static readonly EnvironmentManager instance = new EnvironmentManager();
        private readonly BrowserType _browserType;
        IWebDriver _driver;
        private UrlBuilder _urlBuilder;
        private readonly string _remoteCapabilities;

        public EnvironmentManager()
        {
            var driverClassName = GetSettingValue("Driver");
            var assemblyName = GetSettingValue("Assembly");
            var assembly = Assembly.Load(assemblyName);
            _driverType = assembly.GetType(driverClassName);
            _browserType=(BrowserType)Enum.Parse(typeof(BrowserType),GetSettingValue("DriverName"));
            _remoteCapabilities = GetSettingValue("RemoteCapabilities");
        }

        public static TimeSpan PageLoadTimeOut
        {
            get
            {
                try
                {
                    return TimeSpan.FromSeconds(Convert.ToDouble(GetSettingValue("TimeOut")));
                }
                catch (Exception ex)
                {
                    return TimeSpan.FromSeconds(40);
                }       
            }
        }

        ~EnvironmentManager()
        {
            if (_driver != null)
            {
                _driver.Quit();
            }
        }

        public static string GetSettingValue(string key)
        {
            var strings=System.Configuration.ConfigurationManager.AppSettings.GetValues(key);
            if(strings!=null)
                return strings[0];
            else
            {
                throw new NotSupportedException("We currently cannot find your right key setting!");
            }
        }

        public BrowserType BrowserType
        {
            get { return _browserType; }
        }

        public string RemoteCapabilities
        {
            get { return _remoteCapabilities; }
        }

        public IWebDriver GetCurrentDriver()
        {
            return _driver ?? CreateFreshDriver();
        }

        public IWebDriver CreateSecondDriver()
        {
            return (IWebDriver)Activator.CreateInstance(_driverType);
        }

        public IWebDriver CreateFreshDriver()
        {
            CloseCurrentDriver();
            if (_driverType.Name.Equals("ChromeDriver"))
            {
                //var options = new ChromeOptions();
                //options.AddArgument("--start-maximized");
                //var path = Path.GetFullPath(new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath);
                var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);//Changes 3 lines made by VIP 
                var relativePath = @"..\..\..\Eurofins.ECOM.Selenium.Extension";
                var path = Path.GetFullPath(Path.Combine(outPutDirectory, relativePath));
                _driver = (IWebDriver)Activator.CreateInstance(_driverType, path);
            }
            else if (_driverType.Name.Equals("FirefoxDriver"))
            {
                var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);//Changes 3 lines made by VIP 
                var relativePath = @"..\..\..\Eurofins.ECOM.Selenium.Extension";
                var path = Path.GetFullPath(Path.Combine(outPutDirectory, relativePath));
                _driver = (IWebDriver)Activator.CreateInstance(_driverType,path);
            }
            else
            {
                var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);//Changes 3 lines made by VIP 
                var relativePath = @"..\..\..\Eurofins.ECOM.Selenium.Extension";
                var path = Path.GetFullPath(Path.Combine(outPutDirectory, relativePath));
                _driver = (IWebDriver)Activator.CreateInstance(_driverType,path);                               
            }
            _driver.Manage().Cookies.DeleteAllCookies();
            return _driver;
        }

        public void CloseCurrentDriver()
        {
            if (_driver != null)
            {
                //_driver.Close();
                try
                {
                    //_driver.Quit();
                    if (EnvironmentManager.GetSettingValue("DriverName").Equals("Firefox"))
                    {
                        foreach (System.Diagnostics.Process proc in System.Diagnostics.Process.GetProcessesByName("firefox"))
                        {
                            proc.Kill();
                        }
                    }
                    else if (EnvironmentManager.GetSettingValue("DriverName").Equals("Chrome"))
                    {
                        foreach (System.Diagnostics.Process proc in System.Diagnostics.Process.GetProcessesByName("chrome"))
                        {
                            proc.Kill();
                        }
                        foreach (System.Diagnostics.Process proc in System.Diagnostics.Process.GetProcessesByName("chromedriver"))
                        {
                            proc.Kill();
                        }
                    }
                    else
                    {
                        foreach (System.Diagnostics.Process proc in System.Diagnostics.Process.GetProcessesByName("IEDriverServer"))
                        {
                            proc.Kill();
                        }
                        foreach (System.Diagnostics.Process proc in System.Diagnostics.Process.GetProcessesByName("iexplore"))
                        {
                            proc.Kill();
                        }
                        foreach (System.Diagnostics.Process proc in System.Diagnostics.Process.GetProcessesByName("WerFault"))
                        {
                            proc.Kill();
                        }
                    }
                    _driver.Quit();
                }
                catch (DriverServiceNotFoundException ex)
                {
                    throw ex;
                }
                //driver.Dispose();                
            }

            _driver = null;
        }

        public static EnvironmentManager Instance
        {
            get
            {
                return instance;
            }
        }

        public UrlBuilder UrlBuilder
        {
            get
            {
                if (_urlBuilder == null)
                    _urlBuilder = new UrlBuilder();
                return _urlBuilder;
            }
        }
    }
}
