using System;
using System.Data.SqlClient;
using Eurofins.Testing.Other;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using NLog;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using Eurofins.ECOM.Selenium.Extension.Other;
using Eurofins.ECOM.Selenium.Extension.Interface;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Reflection;

namespace Eurofins.Selenium.Extension.Other
{
    public class DriverTestFixtureBase
    {
        public string PageUrl { get; set; }
        protected string ServerUrl { get; private set; }
        protected string SalesForceURL { get; private set; }
        //public static Logger DDBSLogger = NLog.LogManager.GetLogger("DDBSLogger");
        public static Logger MIDILogger = NLog.LogManager.GetLogger("MIDILogger");
        //public static Logger ECOMLogger = NLog.LogManager.GetLogger("ECOMLogger");
        /// <summary>
        /// Helper class containing Gdi32 API functions
        /// </summary>
        private class GDI32
        {

            public const int SRCCOPY = 0x00CC0020; // BitBlt dwRop parameter
            [DllImport("gdi32.dll")]
            public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest,
                int nWidth, int nHeight, IntPtr hObjectSource,
                int nXSrc, int nYSrc, int dwRop);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth,
                int nHeight);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteObject(IntPtr hObject);
            [DllImport("gdi32.dll")]
            public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        }

        /// <summary>
        /// Helper class containing User32 API functions
        /// </summary>
        private class User32
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }
            [DllImport("user32.dll")]
            public static extern IntPtr GetDesktopWindow();
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowDC(IntPtr hWnd);
            [DllImport("user32.dll")]
            public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);
        }
        /// <summary>
        /// Creates an Image object containing a screen shot of a specific window
        /// </summary>
        /// <param name="handle">The handle to the window. (In windows forms, this is obtained by the Handle property)</param>
        /// <returns></returns>
        public Image CaptureWindow(IntPtr handle)
        {
            // get te hDC of the target window
            IntPtr hdcSrc = User32.GetWindowDC(handle);
            // get the size
            User32.RECT windowRect = new User32.RECT();
            User32.GetWindowRect(handle, ref windowRect);
            int width = windowRect.right - windowRect.left;
            int height = windowRect.bottom - windowRect.top;
            // create a device context we can copy to
            IntPtr hdcDest = GDI32.CreateCompatibleDC(hdcSrc);
            // create a bitmap we can copy it to,
            // using GetDeviceCaps to get the width/height
            IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc, width, height);
            // select the bitmap object
            IntPtr hOld = GDI32.SelectObject(hdcDest, hBitmap);
            // bitblt over
            GDI32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, GDI32.SRCCOPY);
            // restore selection
            GDI32.SelectObject(hdcDest, hOld);
            // clean up 
            GDI32.DeleteDC(hdcDest);
            User32.ReleaseDC(handle, hdcSrc);
            // get a .NET image object for it
            Image img = Image.FromHbitmap(hBitmap);
            // free up the Bitmap object
            GDI32.DeleteObject(hBitmap);
            return img;
        }

        public Image CaptureScreen()
        {
            return CaptureWindow(User32.GetDesktopWindow());
        }
        /// <summary>
        /// Captures a screen shot of the entire desktop, and saves it to a file
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="format"></param>
        public void CaptureScreenToFile(string filename)
        {
            Image img = CaptureScreen();
            img.Save(filename, ImageFormat.Gif);
        }
        public TT GetStart<TT>(string pageUrl) where TT : StartFlow, new()
        {
            string startPage;
            PageUrl = pageUrl;
            if (pageUrl == "")
                startPage = EnvironmentManager.Instance.UrlBuilder.ServerUrl;
            else
                startPage = pageUrl;
            return DoGetStart<TT>(startPage);
        }

        public TT GetStart<TT>(string pageUrl, string downloadFolderPath) where TT : StartFlow, new()
        {
            string startPage;
            PageUrl = pageUrl;
            if (pageUrl == "")
                startPage = EnvironmentManager.Instance.UrlBuilder.ServerUrl;
            else
                startPage = pageUrl;
            return DoGetStart<TT>(startPage, downloadFolderPath);
        }

        public TT GetStartSalesForce<TT>(string pageUrl) where TT : StartFlow, new()
        {
            PageUrl = pageUrl;
            var startPage = EnvironmentManager.Instance.UrlBuilder.SalesForceURL;
            return DoGetStart<TT>(startPage);
        }
        public TT GetStartbackOffice<TT>(string pageUrl) where TT : StartFlow, new()
        {
            PageUrl = pageUrl;
            var startPage = EnvironmentManager.Instance.UrlBuilder.BackOfficeURL;
            return DoGetStart<TT>(startPage);
        }
        public TT DoGetStart<TT>(string startPage, string folderName) where TT : StartFlow, new()
        {

            ChromeOptions options = new ChromeOptions();
            //options.AddArgument("-incognito");
            options.AddArgument("--disable-popup-blocking");
            //var prefs = new Dictionary<string, object> {{ "download.prompt_for_download", true }};
            //options.AddAdditionalCapability("chrome.prefs", prefs);

            options.AddUserProfilePreference("download.prompt_for_download", "false");
            options.AddUserProfilePreference("disable-popup-blocking", "true");
            options.AddUserProfilePreference("download.default_directory", folderName);

            ChromeDriver webDriver = new ChromeDriver(options);
            //webDriver = EnvironmentManager.Instance.GetCurrentDriver();
            IBrowser ieBrowser = GetBrowser(EnvironmentManager.Instance.BrowserType, webDriver);
            Navigator.Instance.InitInstance(ieBrowser, startPage);
            return Activator.CreateInstance(typeof(TT)) as TT;
        }

        public TT DoGetStart<TT>(string startPage) where TT : StartFlow, new()
        {
            ////////////////////////////////////////////////////////////
            //ChromeOptions options = new ChromeOptions();
            ////options.AddArgument("-incognito");
            //options.AddArgument("--disable-popup-blocking");
            ////var prefs = new Dictionary<string, object> {{ "download.prompt_for_download", true }};
            ////options.AddAdditionalCapability("chrome.prefs", prefs);

            //options.AddUserProfilePreference("download.prompt_for_download", "true");
            //options.AddUserProfilePreference("disable-popup-blocking", "true");
            ////options.AddUserProfilePreference("download.default_directory", folderName);
           
            //var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //var relativePath = @"..\..\..\Eurofins.ECOM.Selenium.Extension";
            //var chromeDriverPath = Path.GetFullPath(Path.Combine(outPutDirectory, relativePath));
            //options.BinaryLocation = chromeDriverPath;
            ////driver = new ChromeDriver(chromeDriverPath);


            //ChromeDriver webDriver = new ChromeDriver(chromeDriverPath);
            ////webDriver = EnvironmentManager.Instance.GetCurrentDriver();
            //IBrowser ieBrowser = GetBrowser(EnvironmentManager.Instance.BrowserType, webDriver);
            //Navigator.Instance.InitInstance(ieBrowser, startPage);
            //return Activator.CreateInstance(typeof(TT)) as TT;
            ////////////////////////////////////////////////////////////////////

            var webDriver = EnvironmentManager.Instance.GetCurrentDriver();

            IBrowser ieBrowser = GetBrowser(EnvironmentManager.Instance.BrowserType, webDriver);
            Navigator.Instance.InitInstance(ieBrowser, startPage);

            return Activator.CreateInstance(typeof(TT)) as TT;
        }


        private IBrowser GetBrowser(BrowserType browserType, IWebDriver webDriver)
        {
            IBrowser browser = null;
            switch (browserType)
            {
                case BrowserType.IE: browser = new IEBrowser(webDriver); break;
                case BrowserType.Chrome: browser = new ChromeBrowser(webDriver); break;
                case BrowserType.Firefox: browser = new FirefoxBrowser(webDriver); break;
            }
            return browser;
        }

        protected bool IsIeDriverTimedOutException(Exception e)
        {
            // The IE driver may throw a timed out exception
            return e.GetType().Name.Contains("TimedOutException");
        }

        public void DebugLogger(string msg)
        {
            System.Console.WriteLine(msg);
        }

        #region TestData

        //private DataPackager _dataPackage;
        //public DataPackager DataPackage
        //{
        //    get { return _dataPackage ?? (_dataPackage = new DataPackager()); }
        //}

        public void PrintTime(string keyword)
        {
            System.Console.WriteLine(keyword + "   CurrentTime: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
        }

        //private IeTING _eTingInstanceEurodat506;
        //public IeTING EtingInstanceEurodat506
        //{
        //    get
        //    {
        //        if (_eTingInstanceEurodat506 == null)
        //        {
        //            // Create eTing instance for Eurodat db
        //            var cnn = new SqlConnectionStringBuilder(Services.ConnectionString);
        //            _eTingInstanceEurodat506 = eTINGFactory.CreateInstance();
        //            _eTingInstanceEurodat506.Config.ServerName = cnn.DataSource;
        //            _eTingInstanceEurodat506.Config.DatabaseName = cnn.InitialCatalog;
        //            _eTingInstanceEurodat506.Config.DefaultSnapShotSaveDirectory = @"D:\";
        //            _eTingInstanceEurodat506.Config.Password = cnn.Password;
        //            _eTingInstanceEurodat506.Config.UserName = cnn.UserID;
        //            _eTingInstanceEurodat506.Config.OperatorName = "ROOT";
        //        }
        //        return _eTingInstanceEurodat506;
        //    }
        //}

        [TestInitialize]
        public virtual void TestSetup()
        {
            try
            {
                string aa = EnvironmentManager.GetSettingValue("DriverName");

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
            }
            catch { }
            ServerUrl = EnvironmentManager.Instance.UrlBuilder.ServerUrl;

            //if (RuntimePolicyHelper.LegacyV2RuntimeEnabledSuccessfully)
            //{
            //    //EtingInstanceEurodat506.StartSession(true);
            //}
            //else
            //{
            //    throw new Exception("Could not load SMO");
            //}

        }

        [TestCleanup]
        public virtual void RunAfterAnyTests()
        {
            //EtingInstanceEurodat506.Rollback(true);
            EnvironmentManager.Instance.CloseCurrentDriver();
        }

        #endregion
    }
}

