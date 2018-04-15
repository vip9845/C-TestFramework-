using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eurofins.ECOM.Selenium.Extension.Interface
{
    public interface IINavigator
    {
        TimeSpan PageLoadTimeout
        {
            get;
        }

        IBrowser CurrentBrowser
        {
            get;
        }

        TT FirstOpen<TT>() where TT : IPage, new();

        TT Navigate<TT>(Action action = null, string validatePageUrl = null) where TT : IPage, new();

        TT OpenPopup<TT>(Action action = null, string validatePageUrl = null) where TT : IPage, new();

        TResult WaitForCondition<TResult>(Func<IWebDriver, TResult> condition, int timeout = 5);
    }
}
