using System.Threading;
using OpenQA.Selenium;
using Eurofins.Selenium.Extension.Other;

namespace Eurofins.ECOM.Selenium.Extension.Control
{
    public class LoadingPanel : ControlBase
    {
        private By _by;
        //public WaitingPanel(IWebDriver driver,string id)
        //    : base(driver)
        //{
        //    this._id = Utility.CutBy(id,"=");
        //}

        public LoadingPanel(By by)
            : base(by)
        {

        }

        public LoadingPanel()
        {
        }

        //private bool BuildSelecotr(string id)
        //{
        //    BuildSelectorById(id);

        //    if (WrappedElement != null && WrappedElement.Displayed == true)
        //        return true;
        //    else
        //        return false;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="navigator"></param>
        /// <param name="waitTimeout">Time wait for panel display(Not always the waiting panel will display, so need to wait for it display fisrt. if not, go ahead)</param>
        //public void WaitLoading(INavigator navigator, int waitTimeout = 5)
        //{
        //    int timeOut = 0;
        //    do
        //    {
        //        if (this._id == null || this._id == "" || timeOut > waitTimeout)
        //            break;

        //        if (BuildSelecotr(this._id) == true)
        //        {
        //            //Wait for panel hide
        //            navigator.WaitForCondition(driver => WrappedElement.Displayed == false, 20);
        //            timeOut = waitTimeout;
        //        }
        //        else
        //            Thread.Sleep(1000);
        //        timeOut++;

        //    } while (true);
        //}

        public void WaitLoadingPresent(int waitForDispresentTimeOut = 60)
        {
            int timeOutFlag = 0;
            bool waitPresentFlag = false;
            int waitforPresentTimeOut = 2;
            do
            {
                //FALSE is waiting for loading panel displayed, true is waiting for loading panel hide.
                if (timeOutFlag > waitforPresentTimeOut || timeOutFlag > waitForDispresentTimeOut)
                    break;     
            
                if (!waitPresentFlag)
                {
                    if (IsPresent)
                    {
                        //Loading panel displayed, reset the timeout time.
                        timeOutFlag = 0;
                        waitPresentFlag = true;
                        waitforPresentTimeOut = waitForDispresentTimeOut;
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    if (IsPresent)
                        Thread.Sleep(1000);
                    else
                        break;
                }
                timeOutFlag++;
            } while (true);
        }

        public void WaitLoadingVisible(int waitForDispresentTimeOut = 30)
        {
            int timeOutFlag = 0;
            bool waitPresentFlag = false;
            int waitforPresentTimeOut = 2;
            do
            {
                //FALSE is waiting for loading panel displayed, true is waiting for loading panel hide.
                if (timeOutFlag > waitforPresentTimeOut || timeOutFlag > waitForDispresentTimeOut)
                    break;               

                if (!waitPresentFlag)
                {
                    if (IsPresent == false || !IsVisible)
                        Thread.Sleep(1000);
                    else
                    {
                        timeOutFlag = 0;
                        waitPresentFlag = true;
                        waitforPresentTimeOut = waitForDispresentTimeOut;
                    }                  
                }
                else
                {
                    if (IsPresent == false)
                        break;
                    if (IsVisible)
                        Thread.Sleep(1000);
                    else
                    {
                        Thread.Sleep(500);
                        break;
                    }
                }
                timeOutFlag++;

            } while (true);

            Thread.Sleep(1000);
        }
    }
}
