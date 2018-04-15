using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eurofins.ECOM.Selenium.Extension.Other
{
    public class FrameSelector
    {
        private readonly IWebDriver _webDriver;
        private string _currentFrameName;

        public FrameSelector(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public string CurrentFrameName
        {
            get { return _currentFrameName; }
            set { _currentFrameName = value; }
        }

        public void SwitchToDefaultContent()
        {
            _webDriver.SwitchTo().DefaultContent();
        }

        public void SwitchToFrame(string frameName)
        {
            try
            {
                _webDriver.SwitchTo().DefaultContent();
                System.Threading.Thread.Sleep(10);
                _webDriver.SwitchTo().Frame(frameName);
            }
            catch (NoSuchFrameException)
            {
                
                throw new NoSuchFrameException("There is no "+ frameName+" in current page!");
            }
        }

        public void SwitchToFrame(IWebElement element)
        {
            try
            {
                _webDriver.SwitchTo().DefaultContent();
                System.Threading.Thread.Sleep(100);
                _webDriver.SwitchTo().Frame(element);
            }
            catch (NoSuchFrameException)
            {
                throw new NoSuchFrameException("There is no" + element + "in current page!");
            }
        }

        public void SwitchToFrame(int frameIndex)
        {
            try
            {
                _webDriver.SwitchTo().DefaultContent();
                System.Threading.Thread.Sleep(100);
                _webDriver.SwitchTo().Frame(frameIndex);
            }
            catch (NoSuchFrameException)
            {

                throw new NoSuchFrameException("There is no such frame in current page!");
            }
        }

        public void SwitchToFrameWithoutSwitchingToDefaultContent(int frameIndex)
        {
            try
            {
                //_webDriver.SwitchTo().DefaultContent();
                System.Threading.Thread.Sleep(100);
                _webDriver.SwitchTo().Frame(frameIndex);
            }
            catch (NoSuchFrameException)
            {

                throw new NoSuchFrameException("There is no such frame in current page!");
            }
        }

        public List<IWebElement> IFrames
        {
            get
            {
                try
                {
                    return _webDriver.FindElements(By.TagName("iframe")).ToList();
                }
                catch
                { }
                return new List<IWebElement>();
            }
        }

        public List<IWebElement> Frames
        {
            get
            {
                try
                {
                    return _webDriver.FindElements(By.TagName("frame")).ToList();
                }
                catch
                {
                }
                return new List<IWebElement>();
            }
        }

        public void ReFrame(string frameName = null)
        {
            if (frameName == null)// || frameName == CurrentFrameName)
                return;
            if (frameName == "Null")
            {
                CurrentFrameName = frameName;
                return;
            }

            bool flag = false;
            int timeOut = 0;
            while (flag == false && timeOut < 3)
            {
                try
                {
                    if (frameName.Contains("PLUS")) //This coded is added for handling pages which have frames inside a frame. For e.g, TestsOverviewDotNetPage
                    {
                        string parentFrame = frameName.Substring(0, frameName.IndexOf("PLUS"));
                        SwitchToFrame(parentFrame);
                        SwitchToFrameWithoutSwitchingToDefaultContent(0);
                    }
                    else
                    {
                        SwitchToFrame(frameName);
                    }
                    CurrentFrameName = frameName;
                    flag = true;
                }
                catch (Exception ex)
                {
                    //System.Console.WriteLine(ex.Message.ToString());
                }
                System.Threading.Thread.Sleep(1000);
                timeOut++;
            }
        }
    }
}

