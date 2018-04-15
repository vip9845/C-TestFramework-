using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Eurofins.Selenium.Extension.Other
{
    public class WindowSelector
    {
        private readonly IWebDriver _webDriver;

        private PopupWindowFinder _popupWindowFinder;
       
        public WindowSelector(IWebDriver driver)
        {
            _webDriver = driver;          
        }

        private PopupWindowFinder PopupWindowFinder
        {
            get
            {
                if(_popupWindowFinder==null)
                    _popupWindowFinder=new PopupWindowFinder(_webDriver);
                return _popupWindowFinder;
            }
        }

        public void SwitchToPopup(Action action)
        {        
            SwitchToWindowByWindowHandle(PopupWindowFinder.Invoke(action));
            SetCurrentWindowHandel();
        }

        public void SetCurrentWindowHandel()
        {
            //first time, init handle
            if (string.IsNullOrEmpty(CurrentHandleByManual))
            {
                AddWindowHandle(CurrentWindowHandle, "");
                CurrentHandleByManual = CurrentWindowHandle;
            }                

            if (CurrentHandleByManual != CurrentWindowHandle)
            {
                AddWindowHandle(CurrentWindowHandle, CurrentHandleByManual);
                CurrentHandleByManual = CurrentWindowHandle;
            }
        }

        private string _currentHandleByManual="";
        public string CurrentHandleByManual
        {
            get { return _currentHandleByManual; }
            set { _currentHandleByManual = value; }
        }

        private string CurrentWindowHandle
        {
            get
            {
                return _webDriver.CurrentWindowHandle;
            }
        }

        private int WindowsCount
        {
            get
            {
                return _webDriver.WindowHandles.Count;
            }
        }

        public List<string> WindowHandles
        {
            get
            {
                return _webDriver.WindowHandles.ToList<string>();
            }
        }

        private Dictionary<string, string> _dicWindowHandlePool = new Dictionary<string, string>();
        public Dictionary<string, string> WindowHandlePool
        {
            get
            {
                return _dicWindowHandlePool;
            }
        }

        public void AddWindowHandle(string childWindowHandel, string parentWindowHandle)
        {
            if (_dicWindowHandlePool.ContainsKey(childWindowHandel))
                _dicWindowHandlePool[childWindowHandel] = parentWindowHandle;
            else
                _dicWindowHandlePool.Add(childWindowHandel, parentWindowHandle);
        }

        public void RemoveWindowHandle()
        {
            WindowHandlePool.Remove(CurrentHandleByManual);
            CurrentHandleByManual = CurrentWindowHandle;
        }

        public void CloseWindowAndCheck()
        {
            _webDriver.Close();
            CloseCheck();
        }

        public void CloseCheck()
        {
            bool flag;
            do
            {
                flag = false;
                foreach (var handle in WindowHandles)
                {
                    if (handle == CurrentHandleByManual)
                        flag = true;
                }
                System.Threading.Thread.Sleep(200);
            } while (flag);        
   
            SwitchToWindowByWindowHandle(WindowHandlePool[CurrentHandleByManual]);
            RemoveWindowHandle();
        }

        public bool CompareCurrentPageUrlToTarget(Func<string, bool> func, string targetPageUrl)
        {
            //if (WindowHandles.Count > 1)
            //{
            //    foreach (var item in WindowHandles)
            //    {
            //        SwitchToWindowByWindowHandle(item);
            //        if (func(targetPageUrl))
            //            return true;
            //    }
            //    SwitchToOriginalWindow();
            //    return false;
            //}

            if (targetPageUrl.Split('|').Length > 1)
            {
                foreach (var item in targetPageUrl.Split('|'))
                {
                    if (func(item))
                        return true;
                }
                return false;
            }
            return func(targetPageUrl);
        }

        /// <summary>
        /// Select a window
        /// </summary>
        /// <param name="windowId">title='' or name=''</param>
        public void SwitchToWindowByWindowHandle(string windowId)
        {
            _webDriver.SwitchTo().Window(windowId);
        }

        public void SwitchToOriginalWindow()
        {
            _webDriver.SwitchTo().Window(CurrentHandleByManual);
        }
    }
}
