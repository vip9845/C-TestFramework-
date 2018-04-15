using System;
using System.Collections.Generic;
using Eurofins.Selenium.Extension.Other;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Selenium.Internal.SeleniumEmulation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Eurofins.Testing.Other;

namespace Eurofins.ECOM.Selenium.Extension.Control
{
    public class ControlBase : Control
    {
        #region Property

        private By _selector;
        private IWebElement _webElement;

        public IWebElement WrappedElement
        {
            get
            {
                return _webElement;
            }
            set
            {
                _webElement = value;
            }
        }

        #endregion

        private void GetWrappedElement(By by)
        {
            try
            {
                WrappedElement = CurrentBrowser.WebDriver.FindElement(by);
            }
            
            catch(Exception ex)
            {
                WrappedElement = null;
            }
        }
               

        //public void InitSelectedElement()
        //{
        //    _selectElement = new SelectElement(_webElement);
        //}

        #region Constroctor

        public ControlBase()
        {

        }

        public ControlBase(By by)
        {
            _selector = by;
            GetWrappedElement(_selector);
        }

        public ControlBase(By by, params string[] objects)
        {
            _selector = by;
            GetWrappedElement(_selector);
        }

        public ControlBase(IWebElement element)
        {
            this.WrappedElement = element;
        }

        public ControlBase(params string[] objects)
        {

        }

        #endregion

        private SelectElement _selectElement;
        public SelectElement SelectElement
        {
            get
            {
                if(_selectElement==null)
                    _selectElement = new SelectElement(_webElement);
                return _selectElement;
            }           
        }

        #region Actions

        public virtual void Click(bool isClosing)
        {
            this.Click();
            if (isClosing)
                base.CurrentBrowser.WindowSelector.CloseCheck();
        }

        private enum ElementAction
        {
            Click,
            Clear
        }

        public virtual void TabOut()
        {
            try
            {
                this.WrappedElement.SendKeys(Keys.Tab);
            }
            catch (NullReferenceException ex)
            {
                //throw ex.InnerException; 
                throw new NullReferenceException("Script error while tab out. Javascript not being called");
            }
        }

        public virtual void Clear()
        {
            WaitForControlToExistAndDoAction(this.WrappedElement, ElementAction.Clear);
            //this.WrappedElement.Clear();
        }
        
        public virtual void Click()
        {
            WaitForControlToExistAndDoAction(this.WrappedElement, ElementAction.Click);
            //this.WrappedElement.Click();
        }

        private void WaitForControlToExistAndDoAction(IWebElement element,ElementAction elementAction)
        {
            bool elementExists = false;
            int i = 0;
            bool useJS = true;
            while (!elementExists)
            {
                try
                {
                    switch (elementAction)
                    {
                        case ElementAction.Clear :
                            element.Clear();
                            return;
                        case ElementAction.Click :
                            if (!useJS) //we try using normal method to click once and JS the next time. useJS bool value toggles (see catch block code)
                            {
                                element.Click();
                            }
                            else
                            {
                                IJavaScriptExecutor executor = (IJavaScriptExecutor)this.CurrentBrowser.WebDriver;
                                executor.ExecuteScript("arguments[0].click();", element);
                            }
                            return;
                    }
                }
                catch (NoSuchElementException ex)
                {
                    elementExists = false;
                    useJS = !useJS;
                    System.Threading.Thread.Sleep(500);
                    if (i > 20) //looks for 10 seconds
                    {
                        throw new NoSuchElementException();
                    }
                    i++;
                }
            }

        }

        public string Text
        {
            get
            {
                return this.WrappedElement.Text;
            }
        }

        #endregion

        #region Attribution

        public string ElementType
        {
            get
            {
                return GetAttribute("type");
            }
        }

        public string Class
        {
            get
            {
                return GetAttribute("class");
            }
        }

        /// <summary>
        /// Need to focus to textbox.
        /// </summary>  
        public string Value
        {
            get
            {
                return GetAttribute("value");
            }
        }

        public int Index
        {
            get
            {
                return (int)CurrentBrowser.Actions.GetElementIndex(WrappedElement);
            }
        }

        public string GetAttribute(string attributeName)
        {
            if (WrappedElement == null)
                return "";
            return WrappedElement.GetAttribute(attributeName);
        }

        public bool IsVisible
        {
            get
            {
                try
                {
                    if (WrappedElement == null)
                        throw new NotFoundException("Elemen not found");

                    bool displayed = WrappedElement.Displayed;                  
                    return displayed;
                }
                catch(NotFoundException)
                {
                    return false;                
                }
                //return false;
            }
        }

        public bool IsPresent
        {
            get
            {
                return WrappedElement != null;
            }
        }

        public bool IsDisabled
        {
            get { return WrappedElement.GetAttribute("disabled") == "true"; }
        }

        #endregion

        protected IWebElement FindWebElementFromCurrentWebElement(string xpath, bool isSearchAllSubElement = false)
        {
            try
            {
                if (this.WrappedElement == null)
                    return this.CurrentBrowser.WebDriver.FindElement(By.XPath("/" + xpath));
                if (isSearchAllSubElement)
                {
                    try
                    { return this.WrappedElement.FindElement(By.XPath("./" + xpath)); }
                    catch(NotFoundException nx)
                    { return this.WrappedElement.FindElement(By.XPath("../" + xpath)); }

                }
                try
                { return this.WrappedElement.FindElement(By.XPath("." + xpath)); }
                catch (NotFoundException nx)
                { return this.WrappedElement.FindElement(By.XPath(".." + xpath)); }
            }
            catch (NotFoundException nx)
            {
                return null;
            }
        }

        protected IList<IWebElement> FindWebElementFromCurrentWebElements(string xpath, bool isSearchAllSubElement = false)
        {
            try
            {
                if (this.WrappedElement == null)
                    return this.CurrentBrowser.WebDriver.FindElements(By.XPath("/" + xpath));
                if (isSearchAllSubElement)
                {
                    try
                    { return this.WrappedElement.FindElements(By.XPath("./" + xpath)); }
                    catch (NotFoundException nx)
                    { return this.WrappedElement.FindElements(By.XPath("../" + xpath)); }
                    
                }
                return this.WrappedElement.FindElements(By.XPath("." + xpath));
            }
            catch (NotFoundException nx)
            {
                return null;
            }
        }

        #region Select Element

        public virtual TTControl Parent<TTControl>() where TTControl : ControlBase, new()
        {
            TTControl tt = new TTControl();          
            tt.WrappedElement = FindWebElementFromCurrentWebElement(ClassAttribute.Get(typeof(TTControl)).Replace("/", "/parent::"));
            return tt;
        }

        public virtual TTControl PrecedingSibling<TTControl>() where TTControl : ControlBase, new()
        {
            TTControl tt = new TTControl();
            tt.WrappedElement = FindWebElementFromCurrentWebElement("/preceding-sibling" + ClassAttribute.Get(typeof(TTControl)).Replace("/", "::") + "[1]");
            return tt;
        }

        /// <summary>
        /// position between two element(eg. tr[1]tr[2]tr[3]tr[4]tr[5], the different between tr[5] and tr[1] is 4)
        /// </summary>     
        public virtual TTControl PrecedingSibling<TTControl>(int position) where TTControl : ControlBase, new()
        {
            //e.g: "preceding-sibling::input[@type='radio'][1]"   
            TTControl tt = new TTControl();
            tt.WrappedElement = FindWebElementFromCurrentWebElement("/preceding-sibling" + ClassAttribute.Get(typeof(TTControl)).Replace("/", "::") + "[" + position + "]");
            return tt;
        }

        public virtual TTControl FollowingSibling<TTControl>() where TTControl : ControlBase, new()
        {
            //e.g: "preceding-sibling::input[@type='radio'][1]"           
            TTControl tt = new TTControl();
            tt.WrappedElement = FindWebElementFromCurrentWebElement("/following-sibling" + ClassAttribute.Get(typeof(TTControl)).Replace("/", "::") + "[1]");
            return tt;
        }

        public virtual TTControl FollowingSibling<TTControl>(int position) where TTControl : ControlBase, new()
        {
            //e.g: "preceding-sibling::input[@type='radio'][1]"          
            TTControl tt = new TTControl();
            tt.WrappedElement = FindWebElementFromCurrentWebElement("/following-sibling" + ClassAttribute.Get(typeof(TTControl)).Replace("/", "::") + "[" + position + "]");
            return tt;
        }

        /// <summary>
        /// Select Cell by Attrubute (Some controls like row and table whose args of constructor is different from normal controls need to override this function)
        /// </summary>
        /// <typeparam name="TTControl">Sub Control Type</typeparam>
        /// <param name="name">Attribute Name</param>
        /// <param name="value">Attribute value</param>
        /// <returns></returns>
        public virtual TTControl ControlAttributeFuzzy<TTControl>(string name, string value) where TTControl : ControlBase, new()
        {
            if (typeof(TTControl) == this.GetType())
            {
                WrappedElement = FindWebElementFromCurrentWebElement(ClassAttribute.Get(typeof(TTControl)).Replace("/", "/parent::") + string.Format("[contains(@{0},'{1}')]", name, value));
                return this as TTControl;
            }
            else
            {
                TTControl tt = new TTControl();
                tt.WrappedElement = FindWebElementFromCurrentWebElement(ClassAttribute.Get(typeof(TTControl)) + string.Format("[contains(@{0},'{1}')]", name, value));
                return tt;
            }        
        }

        public virtual IList<TTControl> ControlAttributeStartWithCollection<TTControl>(string name, string value, bool isSearchAllSubElement = false) where TTControl : ControlBase, new()
        {
            IList<TTControl> controlCollection = new List<TTControl>();

            foreach (var item in FindWebElementFromCurrentWebElements(ClassAttribute.Get(typeof(TTControl)) + string.Format("[starts-with(@{0},'{1}')]", name, value), isSearchAllSubElement))
            {
                TTControl tt = new TTControl();
                tt.WrappedElement = item;
                controlCollection.Add(tt);
            }

            return controlCollection;
        }

        public virtual TTControl ControlAttribute<TTControl>(string name, string value) where TTControl : ControlBase, new()
        {
            TTControl tt = new TTControl();
            tt.WrappedElement = FindWebElementFromCurrentWebElement(ClassAttribute.Get(typeof(TTControl)) + string.Format("[@{0}='{1}']", name, value));
            return tt;
        }

        public virtual TTControl Control<TTControl>(bool isSearchAllSubElement = false) where TTControl : ControlBase, new()
        {
            TTControl tt = new TTControl();
            tt.WrappedElement = FindWebElementFromCurrentWebElement(ClassAttribute.Get(typeof(TTControl)), isSearchAllSubElement);
            return tt;
        }

        public virtual TTControl ControlTextFuzzy<TTControl>(string text, bool isSearchAllSubElement = false) where TTControl : ControlBase, new()
        {
            if (typeof(TTControl) == this.GetType())
            {
                WrappedElement = FindWebElementFromCurrentWebElement(ClassAttribute.Get(typeof(TTControl)).Replace("/", "/parent::") + string.Format("[contains(text(),'{0}')]", text), isSearchAllSubElement);
                return this as TTControl;
            }
            else
            {
                TTControl tt = new TTControl();
                tt.WrappedElement = FindWebElementFromCurrentWebElement(ClassAttribute.Get(typeof(TTControl)) + string.Format("[contains(text(),'{0}')]", text),isSearchAllSubElement);
                return tt;
            }
        }

        public virtual TTControl ControlText<TTControl>(string text, bool isSearchAllSubElement = false) where TTControl : ControlBase, new()
        {
            if (typeof(TTControl) == this.GetType())
            {
                WrappedElement = FindWebElementFromCurrentWebElement(string.Format("[text()='{0}']", text),  isSearchAllSubElement);
                return this as TTControl;
            }
            else
            {
                TTControl tt = new TTControl();
                tt.WrappedElement = FindWebElementFromCurrentWebElement(ClassAttribute.Get(typeof(TTControl)) + string.Format("[text()='{0}']", text), isSearchAllSubElement);
                return tt;
            }
        }

        public virtual IList<TTControl> ControlTextCollection<TTControl>(string text, bool isSearchAllSubElement = false) where TTControl : ControlBase, new()
        {
            IList<TTControl> controlCollection = new List<TTControl>();
            if (typeof(TTControl) == this.GetType())
            {
                foreach (var item in FindWebElementFromCurrentWebElements(string.Format("[text()='{0}']", text), isSearchAllSubElement))
                {
                    TTControl tt=new TTControl();
                    tt.WrappedElement=item;
                    controlCollection.Add(tt);
                }
            }
            else
            {
                foreach (var item in FindWebElementFromCurrentWebElements(ClassAttribute.Get(typeof(TTControl)) + string.Format("[text()='{0}']", text), isSearchAllSubElement))
                {
                    TTControl tt = new TTControl();
                    tt.WrappedElement = item;
                    controlCollection.Add(tt);
                }     
            }
            return controlCollection;
        }

        public virtual IList<TTControl> ControlTextFuzzyCollection<TTControl>(string text, bool isSearchAllSubElement = false) where TTControl : ControlBase, new()
        {
            IList<TTControl> controlCollection = new List<TTControl>();
            if (typeof(TTControl) == this.GetType())
            {
                foreach (var item in FindWebElementFromCurrentWebElements(string.Format("[contains(text(),'{0}')]", text), isSearchAllSubElement))
                {
                    TTControl tt = new TTControl();
                    tt.WrappedElement = item;
                    controlCollection.Add(tt);
                }
            }
            else
            {
                foreach (var item in FindWebElementFromCurrentWebElements(ClassAttribute.Get(typeof(TTControl)) + string.Format("[contains(text(),'{0}')]", text),isSearchAllSubElement))
                {
                    TTControl tt = new TTControl();
                    tt.WrappedElement = item;
                    controlCollection.Add(tt);
                }
            }
            return controlCollection;
        }

        #endregion

        /// <summary>
        /// For getting the elements if the xpath can get 2 more elements
        /// </summary>
        //public IList<IWebElement> Collection
        //{
        //    get
        //    {
        //        return FindWebElementFromCurrentWebElement(ClassAttribute.Get(typeof(TTControl)).Replace("/", "/parent::"));                
        //    }
        //}

        public virtual IList<TTControl> GetControlCollection<TTControl>() where TTControl : ControlBase, new()
        {
            IList<TTControl> controlCollection = new List<TTControl>();

            try
            {
                foreach (var item in FindWebElementFromCurrentWebElements(ClassAttribute.Get(typeof(TTControl)).Replace("/", "/parent::")))
                {
                    TTControl tt = new TTControl();
                    tt.WrappedElement = item;
                    controlCollection.Add(tt);
                }
            }
            catch(NullReferenceException ex)
            {
                throw ex;
            }
            catch (NotFoundException)
            {
                Assert.Fail("No Control Found");
            }
            
            return controlCollection;
        }

        public virtual IList<IWebElement> GetElementCollection<TTControl>() where TTControl : ControlBase, new()
        {
            try
            {
                return FindWebElementFromCurrentWebElements(ClassAttribute.Get(typeof(TTControl)).Replace("/", "/parent::"));
            }
            catch (NotFoundException)
            {
                Assert.Fail("No Control Found");
            }
            return null;
        }
    }
}