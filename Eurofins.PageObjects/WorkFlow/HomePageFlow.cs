using Eurofins.Genomics.MIDI.PageObjects.Core;
using Eurofins.PageObjects.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eurofins.PageObjects.WorkFlow
{
    public class HomePageFlow : PageFlowBase
    {
        private readonly HomePage _pageInstance;

        public HomePageFlow()
        {

        }

        public HomePageFlow(HomePage page):base(page)
        {
            _pageInstance = page;
        }

        public HomePage HomePageInstance
        {
            get { return _pageInstance; }
        }

        public HomePageFlow EnterText(string text)
        {
            HomePageInstance.searchText.ContentText = text;
            return this;
        }

        public HomePageFlow ClickOnSerachButton()
        {
            HomePageInstance.searchButton.Click();
            return this;
        }

    }
}
