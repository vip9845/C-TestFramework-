using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Eurofins.ECOM.Selenium.Extension.Control;

namespace Eurofins.ECOM.Selenium.Extension.Control
{
    public class SelectedControl : ControlBase
    {
        public SelectedControl() { }

        public SelectedControl(By by)
            : base(by)
        {
            //base.InitSelectedElement();
        }

        #region SelectElement

        public string SelectedText
        {
            set
            {
                //IeBrowser.Actions.Click();
                if (value.Length > 0)
                {
                    base.SelectElement.SelectByText(value);
                }
                //WrappedElement.Click();
            }
            get
            {
                return SelectElement.SelectedOption.Text;
            }
        }

        public string SelectedValue
        {
            set
            {
                //IeBrowser.Actions.Click();
                base.SelectElement.SelectByValue(value);
                //WrappedElement.Click();
            }
            get
            {
                return base.SelectElement.SelectedOption.GetAttribute("value");
            }
        }

        /// <summary>
        /// From 0
        /// </summary>
        /// <param name="index"></param>
        public void SelectByIndex(int index)
        {
            //base.Click();
            if (HasEmtpyString)
                base.SelectElement.SelectByIndex(index);
            else
                base.SelectElement.SelectByIndex(index - 1);
            //WrappedElement.Click();
        }

        public bool HasEmtpyString
        {
            get
            {
                foreach (var item in base.SelectElement.Options)
                {
                    var label = item.Text;
                    if (label == "")
                        return true;
                }
                return false;
            }
        }

        public IList<string> SelectedElementValues
        {
            get
            {
                var values = new List<string>();
                foreach (var item in base.SelectElement.Options)
                {
                    var value = item.GetAttribute("value");
                    if (value != "")
                        values.Add(value);
                }
                return values;
            }
        }

        public IList<string> SelectedElementTexts
        {
            get
            {
                var labels = new List<string>();
                int x = base.SelectElement.Options.Count;
                foreach (var item in base.SelectElement.Options)
                {
                    var label = item.Text;
                    if (label != "")
                        labels.Add(label);
                }
                return labels;
            }
        }

        public bool IsContainsText(params string[] optionsText)
        {
            bool IsContains = false;
            foreach (string optionText in optionsText)
            {
                IsContains = false;
                foreach (var selectedText in base.SelectElement.Options)
                {
                    if (optionText.ToString() == selectedText.Text.ToString())
                    {
                        IsContains = true;
                        break;
                    }
                }
                if (!IsContains)
                {
                    return IsContains;
                }

            }
            return IsContains;
        }

        #endregion
    }
}