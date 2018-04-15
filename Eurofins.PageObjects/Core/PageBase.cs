using System;


using OpenQA.Selenium;
using Eurofins.ECOM.Selenium.Extension.Interface;
using Eurofins.ECOM.Selenium.Extension.Control;

namespace Eurofins.Genomics.MIDI.PageObjects
{
    /// <summary>
    /// Page Base Class
    /// </summary>
    public class PageBase : IPage
    {
        private string pageUrl;
        public string PageUrl
        {
            get
            {
                return pageUrl;
            }
        }

        public string PageFrameName
        {
            get
            {
                return null;
            }
        }

        public PageBase(string pageUrl)
        {
            this.pageUrl = pageUrl;
        }

        public TControl Get<TControl>(By by0, By by1, params string[] clauses) where TControl : ControlBase, new()
        {
            return Activator.CreateInstance(typeof(TControl), new object[] { by0, by1, clauses }) as TControl;
        }

        public TControl Get<TControl>(By by, params string[] clauses) where TControl : ControlBase, new()
        {
            return Activator.CreateInstance(typeof(TControl), new object[] { by, clauses }) as TControl;
        }

        public TControl Get<TControl>(By by) where TControl : ControlBase, new()
        {
            return Activator.CreateInstance(typeof(TControl), new object[] { by }) as TControl;
        }

        public TControl Get<TControl>(params string[] clauses) where TControl : ControlBase, new()
        {
            return Activator.CreateInstance(typeof(TControl), new object[] { clauses }) as TControl;
        }

        public LoadingPanel LoadingPanelWait
        {
            get
            {
                return Get<LoadingPanel>(By.XPath("//div[@id='progressWithoutCancel']"));
            }
        }
        public LoadingPanel LoadingPanelWaitForVanish
        {
            get
            {
                return Get<LoadingPanel>(By.XPath("//*[@id='progressWithoutCancel']"));
            }
        }

        #region FeedbackPopup
        public Div DivFeedBackPopUp
        {
            get
            {
                return Get<Div>(By.XPath("//div[@id='k_popup']"));
            }
        }

        public Button ButtonFeedBackPopUpNo
        {
            get
            {
                return Get<Button>(By.XPath("//div[@id='k_popup']//*[@id='k_pop_no_btn']"));
            }
        }
        #endregion
    }
}