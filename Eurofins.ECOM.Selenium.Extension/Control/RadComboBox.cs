using System.Threading;
using Eurofins.Selenium.Extension.Other;
using OpenQA.Selenium;
using System;
using Eurofins.ECOM.Selenium.Extension.Control;

namespace Eurofins.ECOM.Selenium.Extension.Control
{
    /// <summary>
    /// 
    /// </summary>
    public class RadComboBox : ControlBase
    {
        //The dropdownListId is the main id of the dropdownList. 
        //When you click the arrow in the dropdownlist, the items will be displayed. 

        private string _radComboboxBaseId;
        private string _loadingPanelId;

        public RadComboBox() { }

        /// <summary>
        /// Common Type is: If the id is 'xxxx', the link id should be'xxxx_Arrow', the combobox id should be 'xxxx_Input',
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="radCombobox"></param>
        /// <param name="linkArrow"></param>
        /// <param name="loadingPanel"></param>
        public RadComboBox(string radComboboxBaseId, string loadingPanelId)
            : base(By.Id(radComboboxBaseId + "_Input"))
        {
            this._radComboboxBaseId = radComboboxBaseId;
            _loadingPanelId = loadingPanelId;
        }

        /// <summary>
        /// Common Type is: If the id is 'xxxx', the link id should be'xxxx_Arrow', the combobox id should be 'xxxx_Input',
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="radCombobox"></param>
        /// <param name="linkArrow"></param>
        /// <param name="loadingPanel"></param>
        public RadComboBox(params string[] radComboxIds)
            : base(By.Id(radComboxIds[0] + "_Input"))
        {
            this._radComboboxBaseId = radComboxIds[0];
            _loadingPanelId = radComboxIds[1];
        }

        private LinkField _linkArrow;
        public LinkField LinkArrow
        {
            get
            {
                if (_linkArrow == null)
                    _linkArrow = new LinkField(By.Id(this._radComboboxBaseId + "_Arrow"));
                return _linkArrow;
            }
        }

        public RadComboBoxItem RadComboBoxItem
        {
            get
            {
                LinkArrow.Click();
                Thread.Sleep(1000);
                if (_loadingPanelId == null || _loadingPanelId == "")
                    return new RadComboBoxItem(By.Id(this._radComboboxBaseId + "_DropDown"), null);
                else
                    return new RadComboBoxItem(By.Id(this._radComboboxBaseId + "_DropDown"), By.Id(_loadingPanelId));

            }
        }

        /// <summary>
        /// Return the first item in combox. It is used for checking whether the combox have been loaded/
        /// </summary>
        public bool IsBinded
        {
            get
            {
                if (base.GetAttribute("value").ToString() == "")
                    return false;
                else
                    return true;
            }
        }

        /// <summary>
        /// selected item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool HasSelected(string item)
        {
            try
            {
                if (GetAttribute("value").ToString().Contains(item))
                    return true;
                else
                    return false;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool IsReady
        {
            get
            {
                return LinkArrow.IsPresent;
            }
        }

        public void UnselectItem()
        {
            LinkArrow.Click();
            Thread.Sleep(1000);
        }
    }
}